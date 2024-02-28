using AdventOfCode.Utilities.Mathematics;
using Xunit;

namespace AdventOfCode.Tests.Utilities.Mathematics;

public class RangeTests
{
    [Theory]
    [InlineData(9, false)]
    [InlineData(10, true)]
    [InlineData(11, true)]
    [InlineData(19, true)]
    [InlineData(20, true)]
    [InlineData(21, false)]
    public void ContainsTests(int value, bool expected)
    {
        var range = new Range<int>(10, 20);
        Assert.Equal(expected, range.ContainsValue(value));
    }
}
