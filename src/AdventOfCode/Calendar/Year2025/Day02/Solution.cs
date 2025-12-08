using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2025.Day02;

[PuzzleInfo(2025, 2, "Gift Shop")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var rotations = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => SumInvalidIds(rotations[0]),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static long SumInvalidIds(string idRanges)
    {
        long sum = 0;

        foreach (var range in idRanges.Split(','))
        {
            var firstId = long.Parse(range[..range.IndexOf('-')]);
            var lastId = long.Parse(range[(range.IndexOf('-') + 1)..]);

            // Brute force approach to look at every number.
            for (var i = firstId; i <= lastId; i++)
            {
                var numDigits = (int)(1 + Math.Log10(i));

                // Only look at numbers that contain an even number of digits.
                // Odd numbers can't be invalid.
                if (numDigits % 2 != 0)
                {
                    continue;
                }

                // Treating the number as a string, compare the first half
                // with the last half for equality.
                var id = i.ToString();

                if (id[..(id.Length / 2)] == id[(id.Length / 2)..])
                {
                    sum += i;
                }
            }
        }

        return sum;
    }
}
