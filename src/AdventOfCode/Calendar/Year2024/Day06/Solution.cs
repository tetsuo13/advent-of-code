using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day06;

[PuzzleInfo(2024, 6, "Guard Gallivant")]
public class Solution : BaseSolution
{
    private const char Obstruction = '#';

    private record struct Position(int Row, int Column);

    /// <summary>
    /// The current direction that the guard is facing from the
    /// perspective of the map.
    /// </summary>
    private enum Direction { Up, Down, Left, Right }

    public override object Run(RunMode runMode)
    {
        var map = ReadInput();
        var initialPosition = FindGuardPosition(map);

        return runMode switch
        {
            RunMode.PartOne => NumPositions(map, initialPosition),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int NumPositions(string[] map, Position position)
    {
        var stepDirection = Direction.Up;

        // Use a grid the same size as the map to record the unique positions
        // visited. Initialize the grid with zeroes, mark visited positions
        // with a non-zero value.
        var visited = new int[map[0].Length, map.Length];

        // The x and y offset for any given direction.
        var stepOffset = new Dictionary<Direction, (int RowOffset, int ColumnOffset)>
        {
            { Direction.Up, (-1, 0) },
            { Direction.Down, (1, 0) },
            { Direction.Right, (0, 1) },
            { Direction.Left, (0, -1) }
        };

        // While not out of bounds, bounce the guard all over the damn place
        do
        {
            visited[position.Row, position.Column] = 1;

            // Examine the next step the guard would make.
            var rowOffset = position.Row + stepOffset[stepDirection].RowOffset;
            var columnOffset = position.Column + stepOffset[stepDirection].ColumnOffset;

            // If the next step causes the guard to leave the area then we're done
            if (rowOffset < 0 || rowOffset >= map.Length ||
                columnOffset < 0 || columnOffset >= map[0].Length)
            {
                break;
            }

            if (map[rowOffset][columnOffset] != Obstruction)
            {
                position.Row += stepOffset[stepDirection].RowOffset;
                position.Column += stepOffset[stepDirection].ColumnOffset;
                continue;
            }

            stepDirection = TurnRight90Degrees(stepDirection);
        } while (true);

        return visited.Cast<int>().Count(x => x == 1);

        Direction TurnRight90Degrees(Direction currentDirection)
        {
            return currentDirection switch
            {
                Direction.Up => Direction.Right,
                Direction.Right => Direction.Down,
                Direction.Down => Direction.Left,
                Direction.Left => Direction.Up,
                _ => throw new InvalidOperationException(nameof(stepDirection))
            };
        }
    }

    private static Position FindGuardPosition(string[] map)
    {
        for (var i = 0; i < map.Length; i++)
        {
            for (var j = 0; j < map[i].Length; j++)
            {
                if (map[i][j] == '^')
                {
                    return new Position(i, j);
                }
            }
        }

        throw new OverflowException("Guard position couldn't be found");
    }
}
