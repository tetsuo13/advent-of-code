using AdventOfCode.Calendar.Year2024.Day10;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day10Tests
{
    [Fact]
    public void Part_Example1()
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
    public void Part_Example2()
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
    public void Part_Example3()
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
        Assert.Equal(4, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part_Example4()
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
    public void Part_Example5()
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
        Assert.Equal(36, solution.Run(RunMode.PartOne));
    }
}
