using AdventOfCode.Utilities.Geometry;

namespace AdventOfCode.Calendar._2023.Day24;

/// <summary>
/// Hail
/// </summary>
/// <param name="Position">The position right now (at time 0).</param>
/// <param name="Velocity">
/// Constant velocity which indicates how far the hailstone will move in one nanosecond.
/// </param>
public readonly record struct Hailstone(BigVector Position, BigVector Velocity)
{
    public bool IntersectsInPast(BigVector intersection) =>
        Velocity.X < 0 ? intersection.X > Position.X : intersection.X < Position.X;
}
