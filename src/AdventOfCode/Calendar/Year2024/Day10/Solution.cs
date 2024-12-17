using AdventOfCode.Common;
using AdventOfCode.Utilities;

namespace AdventOfCode.Calendar.Year2024.Day10;

[PuzzleInfo(2024, 10, "Hoof It")]
public class Solution : BaseSolution
{
    private const int Trailhead = 0;
    private const int Trailend = 9;

    private int[,] _map;

    private enum Measure { Score, Rating }

    public override object Run(RunMode runMode)
    {
        InitializeMap(ReadInput());

        return runMode switch
        {
            RunMode.PartOne => SumTrailheads(Measure.Score),
            RunMode.PartTwo => SumTrailheads(Measure.Rating),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private void InitializeMap(string[] inputLines)
    {
        _map = new int[inputLines.Length, inputLines.Length];

        for (var i = 0; i < inputLines.Length; i++)
        {
            for (var j = 0; j < inputLines[i].Length; j++)
            {
                _map[i, j] = inputLines[i][j] switch
                {
                    '0' => 0,
                    '1' => 1,
                    '2' => 2,
                    '3' => 3,
                    '4' => 4,
                    '5' => 5,
                    '6' => 6,
                    '7' => 7,
                    '8' => 8,
                    '9' => 9,
                    _ => -1 // Doesn't occur for puzzle input, only unit tests
                };
            }
        }
    }

    private int SumTrailheads(Measure measure)
    {
        var sum = 0;

        for (var row = 0; row < _map.GetLength(0); row++)
        {
            for (var column = 0; column < _map.GetLength(1); column++)
            {
                if (_map[row, column] != Trailhead)
                {
                    continue;
                }

                List<Point> trailends = [];
                DoHikingTrail(row, column, trailends, measure);
                sum += trailends.Count;
            }
        }

        return sum;
    }

    private void DoHikingTrail(int row, int column, List<Point> trailends, Measure measure)
    {
        var height = _map[row, column];

        if (height == Trailend)
        {
            var trailend = new Point(row, column);

            switch (measure)
            {
                case Measure.Score when !trailends.Contains(trailend): // *Unique* trailends!
                case Measure.Rating:
                    trailends.Add(trailend);
                    break;
            }

            return;
        }

        if (row > 0 && _map[row - 1, column] == height + 1)
        {
            DoHikingTrail(row - 1, column, trailends, measure);
        }

        if (row < _map.GetLength(0) - 1 && _map[row + 1, column] == height + 1)
        {
            DoHikingTrail(row + 1, column, trailends, measure);
        }

        if (column > 0 && _map[row, column - 1] == height + 1)
        {
            DoHikingTrail(row, column - 1, trailends, measure);
        }

        if (column < _map.GetLength(1) - 1 && _map[row, column + 1] == height + 1)
        {
            DoHikingTrail(row, column + 1, trailends, measure);
        }
    }
}
