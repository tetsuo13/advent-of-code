namespace AdventOfCode.Utilities.Mathematics;

/// <summary>
/// Utility methods for integers.
/// </summary>
public static class NumericUtilities
{
    /// <summary>
    /// Calculate the least common multiple across a set of numbers.
    /// </summary>
    public static ulong LeastCommonMultiple(IEnumerable<ulong> numbers) =>
        numbers.Aggregate((a, b) => a * b / GreatestCommonDivisor(a, b));

    /// <summary>
    /// Calculate the greatest common divisor of two numbers.
    /// </summary>
    private static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
        // Division-based version of the Euclidean algorithm.
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
