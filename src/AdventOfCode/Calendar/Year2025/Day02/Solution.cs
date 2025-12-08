using System.Text;
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
            RunMode.PartOne => SumInvalidIds(rotations[0], false),
            RunMode.PartTwo => SumInvalidIds(rotations[0], true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static long SumInvalidIds(string idRanges, bool useNewRules)
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

                if (numDigits % 2 == 0)
                {
                    // Treating the number as a string, compare the first half
                    // with the last half for equality.
                    var id = i.ToString();

                    if (id[..(id.Length / 2)] == id[(id.Length / 2)..])
                    {
                        sum += i;

                        // Don't run through the new rule process as well,
                        // otherwise it'll get counted twice.
                        if (useNewRules)
                        {
                            continue;
                        }
                    }
                }

                if (!useNewRules)
                {
                    continue;
                }

                // Brute force approach to try every combination.
                for (var y = 1; y <= numDigits / 2; y++)
                {
                    if (numDigits % y != 0)
                    {
                        continue;
                    }

                    var id = i.ToString();
                    var substring = id[..y];
                    var repeats = numDigits / y;
                    var repeated = new StringBuilder(substring.Length * repeats)
                        .Insert(0, substring, repeats)
                        .ToString();

                    if (repeated == id)
                    {
                        sum += i;
                    }
                }
            }
        }

        return sum;
    }
}
