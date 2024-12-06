using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day02;

/// <summary>
/// There's opportunity to consolidate the two different methods into one
/// using recursion. When the Problem Dampener is enabled for part two, it
/// incurs a heavy penalty having to allocate a new list for every level
/// iteration.
/// </summary>
[PuzzleInfo(2024, 2, "Red-Nosed Reports")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => NumSafeReports(inputLines, false),
            RunMode.PartTwo => NumSafeReports(inputLines, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int NumSafeReports(string[] inputLines, bool enableProblemDampener)
    {
        var numSafeReports = 0;

        foreach (var report in inputLines)
        {
            var levels = report.Split(' ')
                .Select(int.Parse)
                .ToList();

            if (IsSafeReport(levels) ||
                (enableProblemDampener && IsSafeReportWithProblemDampener(levels)))
            {
                numSafeReports++;
            }
        }

        return numSafeReports;
    }

    /// <summary>
    /// The report for <paramref name="levels"/> isn't safe. Try removing at
    /// most one level at a time and rerun <see cref="IsSafeReport"/> against
    /// that new report to see if it's safe.
    /// </summary>
    private static bool IsSafeReportWithProblemDampener(List<int> levels)
    {
        for (var i = 0; i < levels.Count; i++)
        {
            var newList = new List<int>(levels);
            newList.RemoveAt(i);

            if (IsSafeReport(newList))
            {
                return true;
            }
        }

        return false;
    }

    private static bool IsSafeReport(List<int> levels)
    {
        // Determine if the levels should be increasing or decreasing based on
        // the first two.
        var expectedGrowth = levels[1] >= levels[0] ? 1 : -1;

        for (var i = 1; i < levels.Count; i++)
        {
            // Multiplying the delta by the expected growth ensures that if an
            // increasing report changes to decreasing the delta will be
            // negative and immediately be marked unsafe by the range check,
            // likewise for decreasing reports; if the delta continues
            // increasing/decreasing as expected, the multiplier won't affect
            // anything.
            var delta = (levels[i] - levels[i - 1]) * expectedGrowth;

            // Two adjacent levels should differ by at least one and at most 3.
            if (delta is < 1 or > 3)
            {
                return false;
            }
        }

        return true;
    }
}
