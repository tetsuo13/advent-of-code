namespace AdventOfCode.Utilities.Geometry;

public static class EuclideanUtilities
{
    /// <summary>
    /// Finds the intersection of a line and a line on a two-dimensional plane
    /// based on a point and velocity of each line.
    /// </summary>
    /// <param name="positionA"></param>
    /// <param name="velocityA"></param>
    /// <param name="positionB"></param>
    /// <param name="velocityB"></param>
    /// <returns>
    /// The point of intersection or <see langword="null"/> when the two lines
    /// are parallel or coincident.
    /// </returns>
    /// <seealso href="https://en.wikipedia.org/wiki/Line%E2%80%93line_intersection"/>
    public static BigVector? FindIntersection2D(BigVector positionA, BigVector velocityA,
        BigVector positionB, BigVector velocityB)
    {
        // Assumes intersection of rays, not segments.

        var positionA2 = positionA + velocityA;
        var positionB2 = positionB + velocityB;

        var a1 = positionA.X * positionA2.Y - positionA.Y * positionA2.X;
        var a2 = positionA.X - positionA2.X;
        var a3 = positionB.X - positionB2.X;
        var a4 = positionB.X * positionB2.Y - positionB.Y * positionB2.X;
        var a5 = positionB.Y - positionB2.Y;
        var a6 = positionA.Y - positionA2.Y;

        var pxD = a2 * a5 - a6 * a3;
        var pyD = a2 * a5 - a6 * a3;

        if (pxD == 0 || pyD == 0)
        {
            return null;
        }

        var pxN = a1 * a3 - a2 * a4;
        var pyN = a1 * a5 - a6 * a4;

        var px = pxN / pxD;
        var py = pyN / pyD;

        return new BigVector(px, py);
    }
}
