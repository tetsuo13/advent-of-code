﻿using AdventOfCode.Calendar.Year2023.Day18;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day18Tests
{
    [Fact]
    public async Task Example()
    {
        string[] lines =
            [
                "R 6 (#70c710)",
                "D 5 (#0dc571)",
                "L 2 (#5713f0)",
                "D 2 (#d2c081)",
                "R 2 (#59c680)",
                "D 2 (#411b91)",
                "L 5 (#8ceee2)",
                "U 2 (#caa173)",
                "L 1 (#1b58a2)",
                "U 2 (#caa171)",
                "R 2 (#7807d2)",
                "U 3 (#a77fa3)",
                "L 2 (#015232)",
                "U 2 (#7a21e3)"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equivalent(62, await solution.Run(RunMode.PartOne));
    }
}
