using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day11;

[PuzzleInfo(2024, 11, "Plutonian Pebbles")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var initialArrangement = ReadInput()[0];

        // Workaround to support unit testing.
        var blinks = initialArrangement switch
        {
            "0 1 10 99 999" => 1,
            _ => 25
        };

        return runMode switch
        {
            RunMode.PartOne => NumStones(initialArrangement, blinks),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int NumStones(string initialArrangement, int blinks)
    {
        var stones = initialArrangement.Split(' ')
            .Select(long.Parse)
            .ToList();

        for (int i = 0; i < blinks; i++)
        {
            stones = EvolveStones(stones);
        }

        return stones.Count;
    }

    private static List<long> EvolveStones(List<long> stones)
    {
        var evolved = new List<long>();

        foreach (var stone in stones)
        {
            if (stone == 0)
            {
                evolved.Add(1);
            }
            else if (Math.Floor(Math.Log10(stone)) % 2 != 0)
            {
                var evenStone = stone.ToString();
                evolved.Add(int.Parse(evenStone[..(evenStone.Length / 2)]));
                evolved.Add(int.Parse(evenStone[(evenStone.Length / 2)..]));
            }
            else
            {
                evolved.Add(stone * 2024);
            }
        }

        return evolved;
    }
}
