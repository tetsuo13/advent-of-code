using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day01;

[PuzzleInfo(2024, 1, "Historian Hysteria")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => TotalDistance(ParseLists(inputLines, true)),
            RunMode.PartTwo => TotalSimilarityScore(ParseLists(inputLines, false)),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <remarks>
    /// This could've been done with <c>.Zip()</c> instead of list indexes but
    /// there doesn't appear to be any significant difference in speed/memory:
    /// <code>
    /// lists.left
    ///     .Zip(lists.right, (x, y) => Math.Abs(x - y))
    ///     .Sum();
    /// </code>
    /// </remarks>
    private static int TotalDistance((List<int> left, List<int> right) lists) =>
        lists.left
            .Select((x, i) => Math.Abs(x - lists.right[i]))
            .Sum();

    /// <summary>
    /// Use frequency map of the right-hand list then multiply each number in
    /// the left-hand list against the entry in the frequency map.
    /// </summary>
    private static int TotalSimilarityScore((List<int> left, List<int> right) lists)
    {
        var frequency = lists.right
            .GroupBy(x => x)
            .ToDictionary(x => x.Key, x => x.Count());

        // No need to cache lookups as they don't repeat, oddly enough.
        return lists.left
            .Select(x => x * frequency.GetValueOrDefault(x, 0))
            .Sum();
    }

    private static (List<int>, List<int>) ParseLists(string[] inputLines, bool sorted)
    {
        var leftList = new List<int>(inputLines.Length);
        var rightList = new List<int>(inputLines.Length);

        foreach (var line in inputLines)
        {
            // This can be optimized further by omitting the .Split() call and
            // using substring with a range:
            //
            // leftList.Add(int.Parse(line[..6]));
            // rightList.Add(int.Parse(line[6..]));
            //
            // However the range amount differs between unit tests (single
            // digits) and the real input (5 digits). No clear way of
            // dynamically setting a range amount depending on whether this is
            // being run in a unit test or not, so pragmatism wins.

            var parts = line.Split();
            leftList.Add(int.Parse(parts[0]));
            rightList.Add(int.Parse(parts[^1]));
        }

        if (!sorted)
        {
            return (leftList, rightList);
        }

        // Order in place, don't use LINQ's .Order() as it'll allocate a new collection.
        leftList.Sort();
        rightList.Sort();

        return (leftList, rightList);
    }
}
