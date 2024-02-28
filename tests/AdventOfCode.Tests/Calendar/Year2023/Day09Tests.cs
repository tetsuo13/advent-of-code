using AdventOfCode.Calendar.Year2023.Day09;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day09Tests
{
    [Theory]
    [InlineData(RunMode.PartOne, 114)]
    [InlineData(RunMode.PartTwo, 2)]
    public async Task Example(RunMode runMode, int expected)
    {
        string[] input =
            [
                "0 3 6 9 12 15",
                "1 3 6 10 15 21",
                "10 13 16 21 30 45"
            ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
