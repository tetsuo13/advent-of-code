using AdventOfCode.Calendar.Year2023.Day07;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day07Tests
{
    [Theory]
    [InlineData(RunMode.PartOne, 6440)]
    [InlineData(RunMode.PartTwo, 5905)]
    public void Example(RunMode runMode, int expected)
    {
        string[] lines =
            [
                "32T3K 765",
                "T55J5 684",
                "KK677 28",
                "KTJJT 220",
                "QQQJA 483"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Theory]
    [InlineData(RunMode.PartOne, 6592)]
    [InlineData(RunMode.PartTwo, 6839)]
    public void Example2(RunMode runMode, int expected)
    {
        string[] lines =
            [
                "2345A 1",
                "Q2KJJ 13",
                "Q2Q2Q 19",
                "T3T3J 17",
                "T3Q33 11",
                "2345J 3",
                "J345A 2",
                "32T3K 5",
                "T55J5 29",
                "KK677 7",
                "KTJJT 34",
                "QQQJA 31",
                "JJJJJ 37",
                "JAAAA 43",
                "AAAAJ 59",
                "AAAAA 61",
                "2AAAA 23",
                "2JJJJ 53",
                "JJJJ2 41"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Theory]
    [InlineData(RunMode.PartOne, 2237)]
    [InlineData(RunMode.PartTwo, 2297)]
    public void Example3(RunMode runMode, int expected)
    {
        string[] lines =
            [
                "23456 22",
                "56789 19",
                "KJJKK 2",
                "AAAAJ 3",
                "JJ243 7",
                "QJ256 6",
                "QQ562 5",
                "Q8Q24 4",
                "AAAAT 3",
                "TJJJJ 2",
                "6789T 18",
                "789TJ 17",
                "22345 13",
                "34567 21",
                "45678 20",
                "32245 12",
                "33245 11",
                "89TJQ 16",
                "9TJQK 15",
                "TJQKA 14",
                "3J245 10",
                "J3425 9",
                "J5432 8",
                "JJJJJ 1"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
