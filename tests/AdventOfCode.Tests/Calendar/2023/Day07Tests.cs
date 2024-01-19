using AdventOfCode.Calendar._2023.Day07;
using Xunit;

namespace AdventOfCode.Tests.Calendar._2023;

public class Day07Tests
{
    [Fact]
    public async Task Example()
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
        Assert.Equal(6440, await solution.Run(RunMode.PartOne));
    }

    [Theory]
    [InlineData(RunMode.PartOne, 6592)]
    //[InlineData(RunMode.PartTwo, 6839)]
    public async Task Example2(RunMode runMode, int expected)
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
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
