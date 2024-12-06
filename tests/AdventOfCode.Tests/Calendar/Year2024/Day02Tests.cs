using AdventOfCode.Calendar.Year2024.Day02;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day02Tests
{
    private readonly string[] _input =
    [
        "7 6 4 2 1",
        "1 2 7 8 9",
        "9 7 6 2 1",
        "1 3 2 4 5",
        "8 6 4 4 1",
        "1 3 6 7 9"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 2)]
    [InlineData(RunMode.PartTwo, 4)]
    public void Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Theory]
    [InlineData("48 46 47 49 51 54 56")]
    [InlineData("1 1 2 3 4 5")]
    [InlineData("1 2 3 4 5 5")]
    [InlineData("5 1 2 3 4 5")]
    [InlineData("1 4 3 2 1")]
    [InlineData("1 6 7 8 9")]
    [InlineData("1 2 3 4 3")]
    [InlineData("9 8 7 6 7")]
    [InlineData("7 10 8 10 11")]
    [InlineData("29 28 27 25 26 25 22 20")]
    public void PartTwo_EdgeCases_ShouldBeMarkedSafe(string report)
    {
        string[] input = [report];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(1, solution.Run(RunMode.PartTwo));
    }
}
