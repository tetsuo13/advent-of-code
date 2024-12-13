using AdventOfCode.Calendar.Year2024.Day07;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day07Tests
{
    private readonly string[] _input =
    [
        "190: 10 19",
        "3267: 81 40 27",
        "83: 17 5",
        "156: 15 6",
        "7290: 6 8 6 15",
        "161011: 16 10 13",
        "192: 17 8 14",
        "21037: 9 7 18 13",
        "292: 11 6 16 20"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 3749)]
    [InlineData(RunMode.PartTwo, 11387)]
    public void Example(RunMode runMode, long expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
