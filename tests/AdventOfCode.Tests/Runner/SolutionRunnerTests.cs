using AdventOfCode.Runner;
using Xunit;

namespace AdventOfCode.Tests.Runner;

public class SolutionRunnerTests
{
    [Theory]
    [InlineData(2023, 1)]
    [InlineData(2023, 15)]
    public void TryFindSolutionType_Exists(int year, int day)
    {
        var t = SolutionRunner.TryFindSolutionType(year, day);
        Assert.NotNull(t);
    }

    [Fact]
    public void TryFindSolutionType_YearMonthNotFound()
    {
        var t = SolutionRunner.TryFindSolutionType(1999, 4);
        Assert.Null(t);
    }
}
