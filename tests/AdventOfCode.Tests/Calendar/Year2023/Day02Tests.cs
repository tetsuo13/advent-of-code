using AdventOfCode.Calendar.Year2023.Day02;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day02Tests
{
    private readonly string[] _input =
        [
            "Game 1: 3 blue, 4 red; 1 red, 2 green, 6 blue; 2 green",
            "Game 2: 1 blue, 2 green; 3 green, 4 blue, 1 red; 1 green, 1 blue",
            "Game 3: 8 green, 6 blue, 20 red; 5 blue, 4 red, 13 green; 5 green, 1 red",
            "Game 4: 1 green, 3 red, 6 blue; 3 green, 6 red; 3 green, 15 blue, 14 red",
            "Game 5: 6 red, 1 blue, 3 green; 2 blue, 1 red, 2 green"
        ];

    [Theory]
    [InlineData(RunMode.PartOne, 8)]
    [InlineData(RunMode.PartTwo, 2286)]
    public void SampleRecord(RunMode runMode, int expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
