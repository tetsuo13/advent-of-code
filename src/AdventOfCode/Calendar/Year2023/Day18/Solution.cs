using System.Numerics;
using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day18;

[PuzzleInfo(2023, 18, "Lavaduct Lagoon")]
public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var digPlan = await ReadInput();

        return runMode switch
        {
            RunMode.PartOne => CubicMetersOfLava(digPlan),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static float CubicMetersOfLava(IEnumerable<string> digPlan)
    {
        // The dig plan starts from (0,0).
        var trench = new Vector3(0, 0, 0);
        var trenches = new List<Vector3> { trench };
        var lagoonPerimeter = 0L;

        foreach (var dig in digPlan)
        {
            (string direction, int meters, string _) = dig.Split(' ') switch
            {
                var x => (x[0], Convert.ToInt32(x[1]), x[2])
            };

            trench = direction switch
            {
                "U"=> trench with { Y = trench.Y - meters },
                "D" => trench with { Y = trench.Y + meters },
                "L" => trench with { X = trench.X - meters },
                "R" => trench with { X = trench.X + meters },
                _ => trench
            };

            trenches.Add(trench);
            lagoonPerimeter += meters;
        }

        // Use Pick's theorem to find the interior area of the polygon:
        // A_i = A + \frac {P}{2} - 1
        var area = CalculateArea(trenches);
        var interior = area - lagoonPerimeter / 2F + 1;

        return interior + lagoonPerimeter;
    }

    /// <summary>
    /// Use Shoelace formula:
    /// <code>A = \frac {1}{2} \sum_{i=1}^{n} v_{i} \wedge v_{i+1}</code>
    /// </summary>
    private static float CalculateArea(IReadOnlyList<Vector3> trenches)
    {
        var sum = 0F;

        for (var current = 0; current < trenches.Count; current++)
        {
            // Circular reference for the next trench.
            var next = current >= trenches.Count - 1 ? 0 : current + 1;

            var result = Vector3.Cross(trenches[current], trenches[next]);
            sum += result.Z;
        }

        return sum / 2;
    }
}
