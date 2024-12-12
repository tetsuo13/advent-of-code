using AdventOfCode.Calendar.Year2024.Day06;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day06Tests
{
    private readonly string[] _input =
    [
        "....#.....",
        ".........#",
        "..........",
        "..#.......",
        ".......#..",
        "..........",
        ".#..^.....",
        "........#.",
        "#.........",
        "......#..."
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 41)]
    [InlineData(RunMode.PartTwo, 6)]
    public void Example(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
