using AdventOfCode.Calendar.Year2023.Day06;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day06Tests
{
    private readonly string[] _input =
    [
        "Time:      7  15   30",
        "Distance:  9  40  200"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 288)]
    [InlineData(RunMode.PartTwo, 71503)]
    public async Task Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
