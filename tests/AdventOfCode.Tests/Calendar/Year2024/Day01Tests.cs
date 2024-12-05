using AdventOfCode.Calendar.Year2024.Day01;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day01Tests
{
    private readonly string[] _input =
    [
        "3   4",
        "4   3",
        "2   5",
        "1   3",
        "3   9",
        "3   3"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 11)]
    [InlineData(RunMode.PartTwo, 31)]
    public void Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
