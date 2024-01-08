using AdventOfCode.Calendar._2023.Day01;
using Xunit;

namespace AdventOfCode.Tests.Calendar._2023.Day01;

public class SolutionTests
{
    [Fact]
    public async Task PartOne()
    {
        string[] lines =
            [
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(142, await solution.Run(RunMode.PartOne));
    }

    [Fact]
    public async Task PartTwo()
    {
        string[] lines =
            [
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(281, await solution.Run(RunMode.PartTwo));
    }
}
