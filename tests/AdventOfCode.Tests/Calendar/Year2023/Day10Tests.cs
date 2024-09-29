using AdventOfCode.Calendar.Year2023.Day10;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day10Tests
{
    [Fact(Skip = "WIP")]
    public async Task Example1Simple()
    {
        string[] lines =
            [
                ".....",
                ".S-7.",
                ".|.|.",
                ".L-J.",
                "....."
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equivalent(4, await solution.Run(RunMode.PartOne));
    }

    [Fact(Skip = "WIP")]
    public async Task Example1Complex()
    {
        string[] lines =
            [
                "-L|F7",
                "7S-7|",
                "L|7||",
                "-L-J|",
                "L|-JF"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equivalent(4, await solution.Run(RunMode.PartOne));
    }

    [Fact(Skip = "WIP")]
    public async Task Example2()
    {
        string[] lines =
            [
                "..F7.",
                ".FJ|.",
                "SJ.L7",
                "|F--J",
                "LJ..."
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equivalent(8, await solution.Run(RunMode.PartOne));
    }
}
