using AdventOfCode.Common;
using AdventOfCode.Utilities;

namespace AdventOfCode.Calendar.Year2024.Day08;

[PuzzleInfo(2024, 8, "Resonant Collinearity")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => LocationsWithAntinodes(inputLines, false),
            RunMode.PartTwo => LocationsWithAntinodes(inputLines, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    /// <summary>
    ///
    /// </summary>
    /// <param name="inputLines"></param>
    /// <param name="accountForResonantHarmonics">
    /// When disabled, will only one antinode from an antenna. When enabled,
    /// will create as many antinodes as will fit in the map.
    /// </param>
    /// <returns></returns>
    private static int LocationsWithAntinodes(string[] inputLines, bool accountForResonantHarmonics)
    {
        var antinodes = new List<Point>();
        var antennas = FindAllAntennasByFrequency(inputLines);

        // Arbitrary upper bound number used -- mostly randomly, chosen to be
        // large enough to accomodate the puzzle input map size.
        var distance = accountForResonantHarmonics ? 40 : 1;

        foreach ((char _, List<Point> positions) in antennas)
        {
            // Compare each antenna with every other antenna of the same
            // frequency.
            for (var i = 0; i < positions.Count; i++)
            {
                if (accountForResonantHarmonics)
                {
                    // The antenna itself is an antinode.
                    antinodes.Add(positions[i]);
                }

                for (var j = i + 1; j < positions.Count; j++)
                {
                    var rise = positions[j].Y - positions[i].Y;
                    var run = positions[j].X - positions[i].X;

                    var increasingAntinodes = new List<Point>();

                    for (var multiplier = 1; multiplier <= distance; multiplier++)
                    {
                        var antinode = new Point(positions[j].X + run * multiplier, positions[j].Y + rise * multiplier);

                        if (IsPositionWithinMap(inputLines, antinode))
                        {
                            increasingAntinodes.Add(antinode);
                        }
                    }

                    antinodes.AddRange(increasingAntinodes);

                    var decreasingAntinodes = new List<Point>();

                    for (var multiplier = 1; multiplier <= distance; multiplier++)
                    {
                        var antinode = new Point(positions[i].X - run * multiplier, positions[i].Y - rise * multiplier);

                        if (IsPositionWithinMap(inputLines, antinode))
                        {
                            decreasingAntinodes.Add(antinode);
                        }
                    }

                    antinodes.AddRange(decreasingAntinodes);
                }
            }
        }

        return antinodes.Distinct().Count();
    }

    private static bool IsPositionWithinMap(string[] inputLines, Point position) =>
        position.X >= 0 && position.X < inputLines[0].Length &&
        position.Y >= 0 && position.Y < inputLines.Length;

    private static Dictionary<char, List<Point>> FindAllAntennasByFrequency(string[] inputLines)
    {
        var antennas = new Dictionary<char, List<Point>>();

        for (var i = 0; i < inputLines.Length; i++)
        {
            for (var j = 0; j < inputLines[i].Length; j++)
            {
                // Only look at stations.
                if (inputLines[i][j] == '.')
                {
                    continue;
                }

                if (!antennas.TryGetValue(inputLines[i][j], out List<Point>? frequencyPositions))
                {
                    frequencyPositions = [];
                    antennas.Add(inputLines[i][j], frequencyPositions);
                }

                frequencyPositions.Add(new Point(j, i));
            }
        }

        return antennas;
    }
}
