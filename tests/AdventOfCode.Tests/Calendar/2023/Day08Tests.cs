using AdventOfCode.Calendar._2023.Day08;
using Xunit;

namespace AdventOfCode.Tests.Calendar._2023;

public class Day08Tests
{
    [Fact]
    public async Task Example()
    {
        string[] input =
            [
                "RL",
                "",
                "AAA = (BBB, CCC)",
                "BBB = (DDD, EEE)",
                "CCC = (ZZZ, GGG)",
                "DDD = (DDD, DDD)",
                "EEE = (EEE, EEE)",
                "GGG = (GGG, GGG)",
                "ZZZ = (ZZZ, ZZZ)"
            ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal((ulong)2, await solution.Run(RunMode.PartOne));
    }

    [Fact]
    public async Task Example2()
    {
        string[] input =
            [
                "LLR",
                "",
                "AAA = (BBB, BBB)",
                "BBB = (AAA, ZZZ)",
                "ZZZ = (ZZZ, ZZZ)"
            ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal((ulong)6, await solution.Run(RunMode.PartOne));
    }

    [Fact]
    public async Task Example3()
    {
        string[] input =
            [
                "LR",
                "",
                "11A = (11B, XXX)",
                "11B = (XXX, 11Z)",
                "11Z = (11B, XXX)",
                "22A = (22B, XXX)",
                "22B = (22C, 22C)",
                "22C = (22Z, 22Z)",
                "22Z = (22B, 22B)",
                "XXX = (XXX, XXX)"
            ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal((ulong)6, await solution.Run(RunMode.PartTwo));
    }
}
