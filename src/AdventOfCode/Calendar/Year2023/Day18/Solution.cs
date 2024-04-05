using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day18;

[PuzzleInfo(2023, 18, "Lavaduct Lagoon")]
public class Solution : BaseSolution
{
    private readonly record struct Point(long X, long Y);

    private enum Direction
    {
        Up,
        Down,
        Left,
        Right
    }

    public override async Task<object> Run(RunMode runMode)
    {
        var digPlan = await ReadInput();

        return runMode switch
        {
            RunMode.PartOne => CubicMetersOfLava(digPlan, false),
            RunMode.PartTwo => CubicMetersOfLava(digPlan, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static long CubicMetersOfLava(IEnumerable<string> digPlan, bool useHex)
    {
        // The dig plan starts from (0,0).
        var trench = new Point(0, 0);
        var trenches = new List<Point> { trench };
        var lagoonPerimeter = 0L;

        foreach (var dig in digPlan)
        {
            (Direction direction, int meters) = DiggerDirections(dig, useHex);

            trench = direction switch
            {
                Direction.Up => trench with { Y = trench.Y - meters },
                Direction.Down => trench with { Y = trench.Y + meters },
                Direction.Left => trench with { X = trench.X - meters },
                Direction.Right => trench with { X = trench.X + meters },
                _ => throw new IndexOutOfRangeException("Direction not yet implemented")
            };

            trenches.Add(trench);
            lagoonPerimeter += meters;
        }

        var area = CalculateArea(trenches);

        // Use Pick's theorem to find the interior area of the polygon:
        // A_i = A - \frac{P}{2} + 1
        var interior = area - lagoonPerimeter / 2 + 1;

        return interior + lagoonPerimeter;
    }

    /// <summary>
    /// Returns the direction and number of meters to dig.
    /// </summary>
    private static (Direction, int) DiggerDirections(string dig, bool useHex)
    {
        var parts = dig.Split(' ');

        if (!useHex)
        {
            return (
                MapToDirection(["U", "D", "L", "R"], parts[0]),
                Convert.ToInt32(parts[1])
            );
        }

        // Remove unnecessary parts from hexadecimal value:
        // (#a1b2c3) => a1b2c3
        var hexColorString = parts[2].Trim(['(', '#', ')']);

        return (
            // Translate last character into direction.
            MapToDirection(["3", "1", "2", "0"], hexColorString[^1..]),

            // Convert first 5 characters from hex to base 10.
            Convert.ToInt32(hexColorString[..5], 16)
        );
    }

    /// <summary>
    /// Maps any arbitrary value to <see cref="Direction"/> based on the
    /// provided <paramref name="map"/>.
    /// </summary>
    /// <param name="map">
    /// A collection of 4 elements corresponding to the values that represent
    /// up, down, left, and right.
    /// </param>
    /// <param name="direction">The value to map.</param>
    /// <returns></returns>
    /// <exception cref="ArgumentOutOfRangeException"></exception>
    private static Direction MapToDirection(IReadOnlyList<string> map, string direction)
    {
        if (map.Count != 4)
        {
            throw new ArgumentException("Map must have 4 elements", nameof(map));
        }
        return direction switch
        {
            _ when direction == map[0] => Direction.Up,
            _ when direction == map[1] => Direction.Down,
            _ when direction == map[2] => Direction.Left,
            _ when direction == map[3] => Direction.Right,
            _ => throw new ArgumentOutOfRangeException(nameof(direction))
        };
    }

    /// <summary>
    /// Use Shoelace formula:
    /// <code>A = \frac{1}{2} \sum_{i=1}^{n} v_{i} \wedge v_{i+1}</code>
    /// </summary>
    private static long CalculateArea(IReadOnlyList<Point> trenches)
    {
        var sum = 0L;

        for (var current = 0; current < trenches.Count; current++)
        {
            // Circular reference for the next trench.
            var next = current >= trenches.Count - 1 ? 0 : current + 1;

            sum += trenches[current].X * trenches[next].Y - trenches[current].Y * trenches[next].X;
        }

        return sum / 2;
    }
}
