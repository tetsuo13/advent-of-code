using AdventOfCode.Calendar.Year2025.Day01;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2025;

public class Day01Tests
{
    private readonly string[] _input =
    [
        "L68",
        "L30",
        "R48",
        "L5",
        "R60",
        "L55",
        "L1",
        "L99",
        "R14",
        "L82"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 3)]
    [InlineData(RunMode.PartTwo, 6)]
    public void Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
