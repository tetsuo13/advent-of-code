using System.Numerics;
using AdventOfCode.Utilities.Geometry;
using AdventOfCode.Utilities.Mathematics;

namespace AdventOfCode.Calendar._2023.Day24;

public class Solution : BaseSolution
{
    private Range<BigInteger> _testArea;

    public override async Task<object> Run(RunMode runMode)
    {
        var lines = await ReadInput();
        var hailstones = ParseHailstones(lines);

        // Change test area for unit tests.
        if (hailstones[0].Position.X > 100)
        {
            _testArea = new Range<BigInteger>(200_000_000_000_000, 400_000_000_000_000);
        }
        else
        {
            _testArea = new Range<BigInteger>(7, 27);
        }

        return runMode switch
        {
            RunMode.PartOne => IntersectionsWithinTestArea(hailstones),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int IntersectionsWithinTestArea(IReadOnlyList<Hailstone> hailstones)
    {
        var count = 0;

        // Compare each hailstone to every other hailstone.
        for (var i = 0; i < hailstones.Count - 1; i++)
        {
            for (var j = i + 1; j < hailstones.Count; j++)
            {
                var intersection = EuclideanUtilities.FindIntersection2D(hailstones[i].Position, hailstones[i].Velocity,
                    hailstones[j].Position, hailstones[j].Velocity);

                // Two lines are parallel or coincident.
                if (intersection is null)
                {
                    continue;
                }

                if (hailstones[i].IntersectsInPast(intersection.Value) ||
                    hailstones[j].IntersectsInPast(intersection.Value))
                {
                    continue;
                }

                if (_testArea.ContainsValue(intersection.Value.X) &&
                    _testArea.ContainsValue(intersection.Value.Y))
                {
                    count++;
                }
            }
        }

        return count;
    }

    private static IReadOnlyList<Hailstone> ParseHailstones(IEnumerable<string> lines)
    {
        var hailstones = new List<Hailstone>();

        foreach (var line in lines)
        {
            var atPos = line.IndexOf('@');
            var position = line[..atPos].Split(',', StringSplitOptions.TrimEntries).Select(BigInteger.Parse).ToList();
            var velocity = line[(atPos + 1)..].Split(',', StringSplitOptions.TrimEntries).Select(BigInteger.Parse).ToList();

            // Throw away z-values in both vectors.
            hailstones.Add(new Hailstone(new BigVector(position), new BigVector(velocity)));
        }

        return hailstones;
    }
}
