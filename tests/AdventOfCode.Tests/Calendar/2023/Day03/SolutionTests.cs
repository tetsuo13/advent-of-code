using AdventOfCode.Calendar._2023.Day03;
using Xunit;

namespace AdventOfCode.Tests.Calendar._2023.Day03;

public class SolutionTests
{
    private class TestSolution(string[] inputLines) : Solution
    {
        protected override Task<string[]> ReadInput() => Task.FromResult(inputLines);
    }

    [Theory]
    [InlineData(RunMode.PartOne, 4361)]
    [InlineData(RunMode.PartTwo, 467835)]
    public async Task ExampleEngineSchematic(RunMode runMode, int expected)
    {
        string[] input =
            [
                "467..114..",
                "...*......",
                "..35..633.",
                "......#...",
                "617*......",
                ".....+.58.",
                "..592.....",
                "......755.",
                "...$.*....",
                ".664.598.."
            ];

        var solution = new TestSolution(input);
        Assert.Equal(expected, await solution.Run(runMode));
    }

    [Fact]
    public async Task PartNumberNotDirectlyUnderGear()
    {
        string[] input =
            [
                "....513..........96.101..359.212.585...............",
                ".............802*...+............#..............804",
                ".707....522....................................*..."
            ];

        var solution = new TestSolution(input);
        Assert.Equal(0, await solution.Run(RunMode.PartTwo));
    }

    [Theory]
    [InlineData(RunMode.PartOne, 413)]
    [InlineData(RunMode.PartTwo, 6756, Skip = "Fails")]
    public async Task ExampleEngineSchematic2(RunMode runMode, int expected)
    {
        string[] input =
            [
                "12.......*..",
                "+.........34",
                ".......-12..",
                "..78........",
                "..*....60...",
                "78..........",
                ".......23...",
                "....90*12...",
                "............",
                "2.2......12.",
                ".*.........*",
                "1.1.......56"
            ];

        var solution = new TestSolution(input);
        Assert.Equal(expected, await solution.Run(runMode));
    }

    [Fact]
    public async Task ExampleEngineSchematic3()
    {
        string[] input =
            [
                ".....24.*23.",
                "..10........",
                "..397*.610..",
                ".......50...",
                "1*2..4......"
            ];

        var solution = new TestSolution(input);
        Assert.Equal(2, await solution.Run(RunMode.PartTwo));
    }

    [Fact]
    public async Task ExampleEngineSchematic4()
    {
        string[] input =
            [
                "........",
                ".24..4..",
                "......*."
            ];

        var solution = new TestSolution(input);
        Assert.Equal(0, await solution.Run(RunMode.PartTwo));
    }

    [Fact]
    public async Task ExampleEngineSchematic5()
    {
        string[] input =
            [
                ".2.",
                ".*.",
                "585"
            ];

        var solution = new TestSolution(input);
        Assert.Equal(1170, await solution.Run(RunMode.PartTwo));
    }

    [Fact(Skip = "Fails")]
    public async Task ExampleEngineSchematic6()
    {
        string[] input =
            [
                "333.3",
                "...*."
            ];

        var solution = new TestSolution(input);
        Assert.Equal(999, await solution.Run(RunMode.PartTwo));
    }
}
