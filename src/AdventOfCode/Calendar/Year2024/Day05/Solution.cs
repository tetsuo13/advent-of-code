using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day05;

[PuzzleInfo(2024, 5, "Print Queue")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();
        var (pageOrderingRules, sectionTwoLine) = ParsePageOrderingRules(inputLines);

        return runMode switch
        {
            RunMode.PartOne => SumMiddlePageNumberOfCorrectlyOrderedUpdates(inputLines, pageOrderingRules, sectionTwoLine),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumMiddlePageNumberOfCorrectlyOrderedUpdates(string[] inputLines,
        Dictionary<int, List<int>> pageOrderingRules, int sectionTwoLine)
    {
        var sum = 0;

        for (var i = sectionTwoLine; i < inputLines.Length; i++)
        {
            var pageNumbers = inputLines[i].Split(',').Select(int.Parse).ToList();

            if (!IsCorrectOrder(pageOrderingRules, pageNumbers))
            {
                continue;
            }

            sum += pageNumbers[pageNumbers.Count / 2];
        }

        return sum;
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

        return (pageOrderingRules, i + 1);
    }
}
