using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day11;

/// <summary>
/// <para>
/// This puzzle is deceptively simple as solving part 1 can be done with just
/// a List. Should complete quickly and with little resources. The only change
/// in requirements for part 2 is a bump in the number of blinks, but it
/// causes a dramatic increase in memory and execution time, so much so that
/// it's almost guaranteed to exhaust memory. The solution created for part 1
/// won't work for part 2.
/// </para>
/// <para>
/// The basic idea behind the rewrite is to not maintain the collection of
/// stones per blink because it reaches 6 figures for 25 blinks and
/// exponentially grows. Rather, treat each stone individually since it
/// doesn't affect its neighbor anyway and maintain the number of stones it
/// evolves into. For part 2 this will end up with a dictionary of just under
/// 4,000 keys since there aren't that many permutations.
/// </para>
/// </summary>
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
            RunMode.PartOne => EvolveStones(initialArrangement, blinks),
            RunMode.PartTwo => EvolveStones(initialArrangement, 75),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static long EvolveStones(string initialArrangement, int blinks)
    {
        // All unique stones and the total number of evolutions. Start with
        // counting each stone in the initial arrangement once.
        var stoneEvolutions = initialArrangement.Split(' ')
            .Select(long.Parse)
            .ToDictionary(stone => stone, _ => 1L);

        // For each blink, create list of modifications of the overall
        // evolutions and merge that back in.
        for (var i = 0; i < blinks; i++)
        {
            var evolutionStage = new Dictionary<long, long>();

            foreach (var stone in stoneEvolutions)
            {
                if (stone.Key == 0)
                {
                    AddToOrCreate(evolutionStage, 1, stoneEvolutions[stone.Key]);
                }
                else
                {
                    var numDigits = Math.Floor(Math.Log10(stone.Key)) + 1;

                    if (numDigits % 2 == 0)
                    {
                        var d = (int)Math.Pow(10, numDigits / 2);
                        AddToOrCreate(evolutionStage, stone.Key / d, stone.Value);
                        AddToOrCreate(evolutionStage, stone.Key % d, stone.Value);
                    }
                    else
                    {
                        AddToOrCreate(evolutionStage, stone.Key * 2024, stone.Value);
                    }
                }
            }

            stoneEvolutions = evolutionStage;
        }

        return stoneEvolutions.Values.Sum();

        void AddToOrCreate(Dictionary<long, long> evolutions, long key, long value)
        {
            if (!evolutions.TryAdd(key, value))
            {
                evolutions[key] += value;
            }
        }
    }
}
