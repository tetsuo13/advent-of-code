using AdventOfCode.Utilities.Mathematics;
using Xunit;

namespace AdventOfCode.Tests.Utilities.Mathematics;

public class NumericUtilitiesTests
{
    [Theory]
    [InlineData(12, new ulong[] { 4, 6 })]
    [InlineData(84, new ulong[] { 4, 7, 12, 21, 42 })]
    public void LeastCommonMultipleTests(ulong expected, ulong[] numbers)
    {
        Assert.Equal(expected, NumericUtilities.LeastCommonMultiple(numbers));
    }
}
