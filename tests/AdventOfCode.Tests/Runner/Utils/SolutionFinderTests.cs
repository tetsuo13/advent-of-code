using AdventOfCode.Calendar;
using AdventOfCode.Runner.Utils;
using Xunit;

namespace AdventOfCode.Tests.Runner.Utils;

public class SolutionFinderTests
{
    [Theory]
    [InlineData("Year2023", "Day01")]
    [InlineData("Year2023", "Day15")]
    public void TryFindSolutionType_Exists_ReturnsType(string year, string day)
    {
        var actual = SolutionFinder.TryFindSolutionType(year, day);
        Assert.NotNull(actual);
    }

    [Fact]
    public void TryFindSolutionType_YearMonthNotFound_ReturnsNull()
    {
        var actual = SolutionFinder.TryFindSolutionType("Year2038", "Day04");
        Assert.Null(actual);
    }
}
