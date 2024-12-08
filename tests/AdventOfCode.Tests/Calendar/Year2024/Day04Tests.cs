using AdventOfCode.Calendar.Year2024.Day04;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day04Tests
{
    [Fact]
    public void Part1_Example1()
    {
        string[] input =
        [
            "..X...",
            ".SAMX.",
            ".A..A.",
            "XMAS.S",
            ".X...."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(4, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part1_Example2()
    {
        string[] input =
        [
            "MMMSXXMASM",
            "MSAMXMSMSA",
            "AMXSXMAAMM",
            "MSAMASMSMX",
            "XMASAMXAMM",
            "XXAMMXXAMA",
            "SMSMSASXSS",
            "SAXAMASAAA",
            "MAMMMXMMMM",
            "MXMXAXMASX"
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(18, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part1_WordExistsMultipleDirectionsStartingFromSameStart()
    {
        string[] input =
        [
            ".XMAS.",
            ".M....",
            ".A....",
            ".S...."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(2, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void Part2_Example()
    {
        string[] input =
        [
            ".M.S......",
            "..A..MSMS.",
            ".M.S.MAA..",
            "..A.ASMSM.",
            ".M.S.M....",
            "..........",
            "S.S.S.S.S.",
            ".A.A.A.A..",
            "M.M.M.M.M.",
            ".........."
        ];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(9, solution.Run(RunMode.PartTwo));
    }
}
