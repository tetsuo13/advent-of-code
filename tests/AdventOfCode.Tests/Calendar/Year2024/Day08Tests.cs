using AdventOfCode.Calendar.Year2024.Day08;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day08Tests
{
    [Fact]
    public void PartOne_Example1()
    {
        string[] input =
        [
            "..........",
            "..........",
            "..........",
            "....a.....",
            "..........",
            ".....a....",
            "..........",
            "..........",
            "..........",
            ".........."
        ];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(2, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void PartOne_Example2()
    {
        string[] input =
        [
            "..........",
            "..........",
            "..........",
            "....a.....",
            "........a.",
            ".....a....",
            "..........",
            "..........",
            "..........",
            ".........."
        ];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(4, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void PartOne_Example3()
    {
        string[] input =
        [
            "..........",
            "..........",
            "..........",
            "....a.....",
            "........a.",
            ".....a....",
            "..........",
            "......A...",
            "..........",
            ".........."
        ];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(4, solution.Run(RunMode.PartOne));
    }

    [Theory]
    [InlineData(RunMode.PartOne, 14)]
    [InlineData(RunMode.PartTwo, 34)]
    public void Example(RunMode runMode, int expected)
    {
        string[] input =
        [
            "............",
            "........0...",
            ".....0......",
            ".......0....",
            "....0.......",
            "......A.....",
            "............",
            "............",
            "........A...",
            ".........A..",
            "............",
            "............"
        ];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Fact]
    public void PartTwo_Example1()
    {
        string[] input =
        [
            "T.........",
            "...T......",
            ".T........",
            "..........",
            "..........",
            "..........",
            "..........",
            "..........",
            "..........",
            ".........."
        ];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(9, solution.Run(RunMode.PartTwo));
    }
}
