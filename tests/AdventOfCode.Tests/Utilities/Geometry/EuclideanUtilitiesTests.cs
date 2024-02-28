using AdventOfCode.Utilities.Geometry;
using Xunit;

namespace AdventOfCode.Tests.Utilities.Geometry;

public class EuclideanUtilitiesTests
{
    [Fact]
    public void FindIntersection2D_ParallelLines_ReturnsNull()
    {
        var line1Position = new BigVector(18, 19);
        var line1Velocity = new BigVector(-1, -1);
        var line2Position = new BigVector(20, 25);
        var line2Velocity = new BigVector(-2, -2);

        var actual = EuclideanUtilities.FindIntersection2D(line1Position, line1Velocity, line2Position, line2Velocity);

        Assert.Null(actual);
    }
}
