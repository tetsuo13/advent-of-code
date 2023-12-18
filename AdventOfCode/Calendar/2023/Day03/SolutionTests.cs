using Xunit;

namespace AdventOfCode.Calendar._2023.Day03;

public class SolutionTests
{
    private class TestSolution(string[] inputLines) : Solution
    {
        public override Task<string[]> ReadInput() => Task.FromResult(inputLines);
    }

    private readonly string[] Input =
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

    [Theory]
    [InlineData(RunMode.PartOne, 4361)]
    public async Task Run(RunMode runMode, int expected)
    {
        var solution = new TestSolution(Input);
        Assert.Equal(expected, await solution.Run(runMode));
    }
}
