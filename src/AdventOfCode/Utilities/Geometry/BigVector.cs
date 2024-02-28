using System.Numerics;

namespace AdventOfCode.Utilities.Geometry;

/// <summary>
/// Represents a vector with two <see cref="BigInteger"/> values. Exists
/// because <see cref="System.Numerics.Vector2" /> uses floating-point values
/// that lose too much precision during operations.
/// </summary>
/// <param name="X">The X component of the vector.</param>
/// <param name="Y">The Y component of the vector.</param>
public readonly record struct BigVector(BigInteger X, BigInteger Y)
{
    /// <summary>
    /// Creates a new LongVector object from the first two elements in the
    /// collection.
    /// </summary>
    /// <param name="inputs">The collection of elements.</param>
    public BigVector(IReadOnlyList<BigInteger> inputs) : this(inputs[0], inputs[1])
    {
    }

    /// <summary>
    /// Adds two vectors together.
    /// </summary>
    /// <param name="lhs">The first vector to add.</param>
    /// <param name="rhs">The second vector to add.</param>
    /// <returns>The summed vector.</returns>
    public static BigVector operator +(BigVector lhs, BigVector rhs) =>
        new(lhs.X + rhs.X, lhs.Y + rhs.Y);
}
