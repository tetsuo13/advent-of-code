using AdventOfCode.Calendar.Year2023.Day15;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day15Tests
{
    [Theory]
    [InlineData("HASH", RunMode.PartOne, 52)]
    [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", RunMode.PartOne, 1320)]
    [InlineData("rn=1,cm-,qp=3,cm=2,qp-,pc=4,ot=9,ab=5,pc-,pc=6,ot=7", RunMode.PartTwo, 145)]
    public void Example(string s, RunMode runMode, int expected)
    {
        string[] input = [s];

        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(expected, solution.Run(runMode));
    }
}
