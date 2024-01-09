using AdventOfCode.Calendar._2023.Day06;
using Xunit;

namespace AdventOfCode.Tests.Calendar._2023.Day06;

public class SolutionTests
{
    [Theory]
    [InlineData(RunMode.PartOne, 288)]
    [InlineData(RunMode.PartTwo, 71503)]
    public async Task Example(RunMode runMode, int expected)
    {
        string[] input =
            [
                "Time:      7  15   30",
                "Distance:  9  40  200"
            ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
