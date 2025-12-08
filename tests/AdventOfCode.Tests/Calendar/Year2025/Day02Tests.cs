using AdventOfCode.Calendar.Year2025.Day02;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2025;

public class Day02Tests
{
    private readonly string[] _input =
    [
        "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124"
    ];

    [Theory]
    [InlineData(RunMode.PartOne, 1_227_775_554)]
    [InlineData(RunMode.PartTwo, 4_174_379_265)]
    public void Example(RunMode runMode, long expected)
    {
        var solution = new SolutionTestWrapper<Solution>(_input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
