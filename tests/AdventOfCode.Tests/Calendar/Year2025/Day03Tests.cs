using AdventOfCode.Calendar.Year2025.Day03;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2025;

public class Day03Tests
{
    private readonly string[] _input =
    [
        "987654321111111",
        "811111111111119",
        "234234234234278",
        "818181911112111"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 357)]
    public void Example(RunMode runMode, double expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
