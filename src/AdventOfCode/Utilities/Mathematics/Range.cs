namespace AdventOfCode.Utilities.Mathematics;

/// <summary>
/// Represents a range that has a minimum and maximum indexes.
/// </summary>
/// <param name="Minimum">The inclusive minimum index of the range.</param>
/// <param name="Maximum">The inclusive maximum index of the range.</param>
/// <typeparam name="T"></typeparam>
public readonly record struct Range<T>(T Minimum, T Maximum)
    where T : IComparable<T>
{
    /// <summary>
    /// Determines if the provided value is inside the range, inclusive.
    /// </summary>
    /// <param name="value">The value to test.</param>
    /// <returns>Whether the value is within range or not.</returns>
    public bool ContainsValue(T value) =>
        Minimum.CompareTo(value) <= 0 && value.CompareTo(Maximum) <= 0;
}
