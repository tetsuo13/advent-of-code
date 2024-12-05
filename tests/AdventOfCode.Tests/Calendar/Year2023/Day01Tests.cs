using AdventOfCode.Calendar.Year2023.Day01;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day01Tests
{
    [Fact]
    public void PartOne()
    {
        string[] lines =
            [
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(142, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void PartTwo()
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
        Assert.Equal(281, solution.Run(RunMode.PartTwo));
    }
}
