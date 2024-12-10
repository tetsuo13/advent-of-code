using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day05;

/// <summary>
/// This could have been more elegant using a custom IComparer implementation.
/// </summary>
[PuzzleInfo(2024, 5, "Print Queue")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();
        var (pageOrderingRules, sectionTwoLine) = ParsePageOrderingRules(inputLines);

        return runMode switch
        {
            RunMode.PartOne => SumMiddlePageNumbers(inputLines, pageOrderingRules, sectionTwoLine, false),
            RunMode.PartTwo => SumMiddlePageNumbers(inputLines, pageOrderingRules, sectionTwoLine, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumMiddlePageNumbers(string[] inputLines, Dictionary<int, List<int>> pageOrderingRules,
        int sectionTwoLine, bool onlyIncorrectlyOrdered)
    {
        var sum = 0;

        for (var i = sectionTwoLine; i < inputLines.Length; i++)
        {
            var pageNumbers = inputLines[i].Split(',')
                .Select(int.Parse)
                .ToList();

            if (!IsCorrectOrder(pageOrderingRules, pageNumbers))
            {
                if (onlyIncorrectlyOrdered)
                {
                    pageNumbers = CorrectOrder(pageOrderingRules, pageNumbers);
                    sum += pageNumbers[pageNumbers.Count / 2];
                }
                continue;
            }

            if (!onlyIncorrectlyOrdered)
            {
                sum += pageNumbers[pageNumbers.Count / 2];
            }
        }

        return sum;
    }

    private static List<int> CorrectOrder(Dictionary<int, List<int>> pageOrderingRules, List<int> pageNumbers)
    {
        var errors = false;

        // Keep making corrections until there aren't anymore to be made.
        do
        {
            errors = false;

            for (var i = 0; i < pageNumbers.Count - 1; i++)
            {
                if (!pageOrderingRules.TryGetValue(pageNumbers[i], out List<int>? value) ||
                    !value.Contains(pageNumbers[i + 1]))
                {
                    // Swap places.
                    (pageNumbers[i], pageNumbers[i + 1]) = (pageNumbers[i + 1], pageNumbers[i]);
                    errors = true;
                }
            }
        } while (errors);

        return pageNumbers;
    }

    private static bool IsCorrectOrder(Dictionary<int, List<int>> pageOrderingRules, List<int> pageNumbers)
    {
        var firstPass = pageNumbers[1..].All(pageOrderingRules[pageNumbers[0]].Contains);

        if (!firstPass)
        {
            return false;
        }

        for (var j = 0; j < pageNumbers.Count - 1; j++)
        {
            if (!pageOrderingRules.TryGetValue(pageNumbers[j], out List<int>? value) ||
                !value.Contains(pageNumbers[j + 1]))
            {
                return false;
            }
        }

        return true;
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputLines"></param>
    /// <returns>
    /// Tuple of page order rules and index in <paramref name="inputLines"/>
    /// where second section starts.
    /// </returns>
    private static (Dictionary<int, List<int>>, int) ParsePageOrderingRules(string[] inputLines)
    {
        var pageOrderingRules = new Dictionary<int, List<int>>();
        var i = 0;

        for (; i < inputLines.Length; i++)
        {
            if (string.IsNullOrEmpty(inputLines[i]))
            {
                break;
            }

            var pageX = int.Parse(inputLines[i][..2]);
            var pageY = int.Parse(inputLines[i][3..]);

            if (pageOrderingRules.TryGetValue(pageX, out List<int>? value))
            {
                value.Add(pageY);
            }
            else
            {
                pageOrderingRules.Add(pageX, [pageY]);
            }
        }

        // Index is the section break, a blank line.
        return (pageOrderingRules, i + 1);
    }
}
