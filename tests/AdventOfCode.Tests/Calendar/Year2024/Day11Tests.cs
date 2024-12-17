using AdventOfCode.Calendar.Year2024.Day11;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day11Tests
{
    [Fact]
    public void Example1()
    {
        string[] input = ["0 1 10 99 999"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(7, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Example2()
    {
        string[] input = ["125 17"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(55312, solution.Run(RunMode.PartOne));
    }
}
