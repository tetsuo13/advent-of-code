using AdventOfCode.Calendar.Year2024.Day10;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day10Tests
{
    [Theory]
    [InlineData(4, RunMode.PartOne)]
    [InlineData(13, RunMode.PartTwo)]
    public void Example1(int expected, RunMode runMode)
    {
        string[] input =
        [
            "..90..9",
            "...1.98",
            "...2..7",
            "6543456",
            "765.987",
            "876....",
            "987...."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Theory]
    [InlineData(36, RunMode.PartOne)]
    [InlineData(81, RunMode.PartTwo)]
    public void Example2(int expected, RunMode runMode)
    {
        string[] input =
        [
            "89010123",
            "78121874",
            "87430965",
            "96549874",
            "45678903",
            "32019012",
            "01329801",
            "10456732"
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Fact]
    public void Part1_Example1()
    {
        string[] input =
        [
            "0123",
            "1234",
            "8765",
            "9876"
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(1, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part1_Example2()
    {
        string[] input =
        [
            "...0...",
            "...1...",
            "...2...",
            "6543456",
            "7.....7",
            "8.....8",
            "9.....9"
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(2, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part1_Example3()
    {
        string[] input =
        [
            "10..9..",
            "2...8..",
            "3...7..",
            "4567654",
            "...8..3",
            "...9..2",
            ".....01"
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(3, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part2_Example1()
    {
        string[] input =
        [
            ".....0.",
            "..4321.",
            "..5..2.",
            "..6543.",
            "..7..4.",
            "..8765.",
            "..9...."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(3, solution.Run(RunMode.PartTwo));
    }

    [Fact]
    public void Part2_Example2()
    {
        string[] input =
        [
            "012345",
            "123456",
            "234567",
            "345678",
            "4.6789",
            "56789."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(227, solution.Run(RunMode.PartTwo));
    }
}
