using AdventOfCode.Runner.Utils;
using Xunit;

namespace AdventOfCode.Tests.Runner.Utils;

public class SolutionFinderTests
{
    [Theory]
    [InlineData("Year2023", "Day01")]
    [InlineData("Year2023", "Day15")]
    public void TryFindSolutionType_Exists(string year, string day)
    {
        var t = SolutionFinder.TryFindSolutionType(year, day);
        Assert.NotNull(t);
    }

    [Fact]
    public void TryFindSolutionType_YearMonthNotFound()
    {
        var t = SolutionFinder.TryFindSolutionType("Year1999", "Day04");
        Assert.Null(t);
    }
}
