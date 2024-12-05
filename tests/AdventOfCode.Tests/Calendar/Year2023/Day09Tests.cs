using AdventOfCode.Calendar.Year2023.Day09;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day09Tests
{
    private readonly string[] _input =
    [
        "0 3 6 9 12 15",
        "1 3 6 10 15 21",
        "10 13 16 21 30 45"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 114)]
    [InlineData(RunMode.PartTwo, 2)]
    public async Task Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
