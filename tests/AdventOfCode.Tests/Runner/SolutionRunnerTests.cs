using AdventOfCode.Runner;
using Xunit;

namespace AdventOfCode.Tests.Runner;

public class SolutionRunnerTests
{
    [Theory]
    [InlineData("Year2023", "Day01")]
    [InlineData("Year2023", "Day15")]
    public void TryFindSolutionType_Exists(string year, string day)
    {
        var t = SolutionRunner.TryFindSolutionType(year, day);
        Assert.NotNull(t);
    }

    [Fact]
    public void TryFindSolutionType_YearMonthNotFound()
    {
        var t = SolutionRunner.TryFindSolutionType("Year1999", "Day04");
        Assert.Null(t);
    }
}
