using AdventOfCode.Calendar.Year2023.Day24;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2023;

public class Day24Tests
{
    [Fact]
    public void Example()
    {
        string[] lines =
            [
                "19, 13, 30 @ -2,  1, -2",
                "18, 19, 22 @ -1, -1, -2",
                "20, 25, 34 @ -2, -2, -4",
                "12, 31, 28 @ -1, -2, -1",
                "20, 19, 15 @  1, -5, -3"
            ];

        var solution = new SolutionTestWrapper<Solution>(lines);
        Assert.Equal(2, solution.Run(RunMode.PartOne));
    }
}
