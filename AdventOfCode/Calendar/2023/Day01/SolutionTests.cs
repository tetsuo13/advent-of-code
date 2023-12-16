using Xunit;

namespace AdventOfCode.Calendar._2023.Day01;

public class SolutionTests
{
    private class TestSolution(string[] inputLines) : Solution
    {
        public override async Task<string[]> ReadInput() => inputLines;
    }

    [Fact]
    public async Task PartOne()
    {
        string[] lines =
            [
                "1abc2",
                "pqr3stu8vwx",
                "a1b2c3d4e5f",
                "treb7uchet"
            ];

        var solution = new TestSolution(lines);
        Assert.Equal(142, await solution.Run(RunMode.PartOne));
    }

    [Fact]
    public async Task PartTwo()
    {
        string[] lines =
            [
                "two1nine",
                "eightwothree",
                "abcone2threexyz",
                "xtwone3four",
                "4nineeightseven2",
                "zoneight234",
                "7pqrstsixteen"
            ];

        var solution = new TestSolution(lines);
        Assert.Equal(281, await solution.Run(RunMode.PartTwo));
    }
}
