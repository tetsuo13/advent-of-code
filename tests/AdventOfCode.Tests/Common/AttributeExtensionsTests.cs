using AdventOfCode.Calendar;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Common;

public class AttributeExtensionsTests
{
    [Fact]
    public void GetPuzzleInfoAttribute_ReturnsCorrectAttribute()
    {
        var actual = typeof(SolutionReturnsCorrectAttribute).GetPuzzleInfoAttribute();
        Assert.NotNull(actual);
    }

    [Fact]
    public void GetPuzzleInfoAttribute_MissingAttribute_ReturnsNull()
    {
        var actual = typeof(SolutionMissingAttribute).GetPuzzleInfoAttribute();
        Assert.Null(actual);
    }

    [Fact]
    public void BaseSolutionToString_ContainsPuzzleInfoAttribute()
    {
        var solution = new SolutionReturnsCorrectAttribute();
        var actual = solution.ToString();
        Assert.Equal("2024-12-08: Indie Pop Rocks!", actual);
    }

    [Fact]
    public void BaseSolutionToString_MissingAttribute_ReturnsErrorMessage()
    {
        var solution = new SolutionMissingAttribute();
        var actual = solution.ToString();
        Assert.Equal($"Missing {nameof(PuzzleInfoAttribute)} on class", actual);
    }

    [PuzzleInfo(2024, 8, "Indie Pop Rocks!")]
    private class SolutionReturnsCorrectAttribute : BaseSolution
    {
        public override Task<object> Run(RunMode runMode) => throw new NotImplementedException();
    }

    private class SolutionMissingAttribute : BaseSolution
    {
        public override Task<object> Run(RunMode runMode) => throw new NotImplementedException();
    }
}
