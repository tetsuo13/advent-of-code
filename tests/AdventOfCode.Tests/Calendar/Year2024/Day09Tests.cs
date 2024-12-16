using AdventOfCode.Calendar.Year2024.Day09;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day09Tests
{
    [Theory]
    [InlineData(RunMode.PartOne, 1_928)]
    [InlineData(RunMode.PartTwo, 2_858)]
    public void Example(RunMode runMode, long expected)
    {
        string[] input = ["2333133121414131402"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, solution.Run(runMode));
    }

    [Theory]
    [InlineData("12345", 60)]
    [InlineData("233313312141413140256", 3_383)]
    public void Part1_Examples(string diskMap, long expected)
    {
        var solution = new SolutionTestWrapper<Solution>([diskMap]);
        Assert.Equal(expected, solution.Run(RunMode.PartOne));
    }
}
