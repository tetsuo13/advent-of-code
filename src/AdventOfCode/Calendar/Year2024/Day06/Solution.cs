using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day06;

/// <summary>
/// Part 2 solution needs a lot of rework.
/// </summary>
[PuzzleInfo(2024, 6, "Guard Gallivant")]
public class Solution : BaseSolution
{
    private const char Obstruction = '#';

    private record struct Position(int Row, int Column);

    /// <summary>
    /// The current direction that the guard is facing from the
    /// perspective of the map.
    /// </summary>
    // TODO: Move this out under the Utilities namespace, consolidate other uses of the same enum
    private enum Direction { Up, Down, Left, Right }

    /// <summary>
    /// The x and y offset for any given direction.
    /// </summary>
    private readonly Dictionary<Direction, (int RowOffset, int ColumnOffset)> _stepOffset = new()
    {
        { Direction.Up, (-1, 0) },
        { Direction.Down, (1, 0) },
        { Direction.Right, (0, 1) },
        { Direction.Left, (0, -1) }
    };

    private bool[,]? _visitedPositions;

    public override object Run(RunMode runMode)
    {
        var map = ReadInput();
        _visitedPositions = new bool[map.Length, map[0].Length];
        var initialPosition = FindGuardPosition(map);

        return runMode switch
        {
            RunMode.PartOne => NumPositionsVisitedBeforeLeavingMap(map, initialPosition),
            RunMode.PartTwo => NumPositionsCausingLoop(map, initialPosition),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int NumPositionsCausingLoop(string[] map, Position initialPosition)
    {
        // Record happy path guard took.
        NumPositionsVisitedBeforeLeavingMap(map, initialPosition);

        var numPositionsCausingLoop = 0;

        // Place an obstruction for every position that was visited in order
        // for the guard to leave the map, run the map starting at the same
        // initial position, and see if it results in a loop.
        // TODO: This could be done in parallel
        for (var i = 0; i < _visitedPositions!.GetLength(0); i++)
        {
            for (var j = 0; j < _visitedPositions.GetLength(1); j++)
            {
                if (!_visitedPositions[i, j])
                {
                    continue;
                }

                var newObstruction = new Position(i, j);

                // Try running the map starting from the same initial position
                var enteredLoop = CausesGuardToEnterLoop(newObstruction,
                    new Position(initialPosition.Row, initialPosition.Column));

                if (enteredLoop)
                {
                    numPositionsCausingLoop++;
                }
            }
        }

        return numPositionsCausingLoop;

        bool CausesGuardToEnterLoop(Position potentialObstruction, Position position)
        {
            var stepDirection = Direction.Up;
            var visited = new Dictionary<Position, Direction>();

            do
            {
                // Examine the next step the guard would make.
                var next = new Position
                {
                    Row = position.Row + _stepOffset[stepDirection].RowOffset,
                    Column = position.Column + _stepOffset[stepDirection].ColumnOffset
                };

                // If the next step causes the guard to leave the area then we're done
                if (next.Row < 0 || next.Row >= map.Length ||
                    next.Column < 0 || next.Column >= map[0].Length)
                {
                    return false;
                }

                if (map[next.Row][next.Column] == Obstruction ||
                    next == potentialObstruction)
                {
                    // If guard has already hit this obstruction before travelling in
                    // the same direction then it means they've entered a loop.
                    if (visited.TryGetValue(position, out Direction value) && value == stepDirection)
                    {
                        return true;
                    }

                    visited.TryAdd(position, stepDirection);
                    stepDirection = TurnRight90Degrees(stepDirection);
                    continue;
                }

                visited.TryAdd(position, stepDirection);
                position.Row += _stepOffset[stepDirection].RowOffset;
                position.Column += _stepOffset[stepDirection].ColumnOffset;
            } while (true);
        }
    }

    private int NumPositionsVisitedBeforeLeavingMap(string[] map, Position position)
    {
        var stepDirection = Direction.Up;

        // Use a grid the same size as the map to record the unique positions
        // visited. Initialize the grid with false values, mark visited
        // positions by flipping bit.

        // While not out of bounds, bounce the guard all over the damn place.
        // Assumes that guard won't enter a loop scenario.
        do
        {
            _visitedPositions![position.Row, position.Column] = true;

            // Examine the next step the guard would make.
            var rowOffset = position.Row + _stepOffset[stepDirection].RowOffset;
            var columnOffset = position.Column + _stepOffset[stepDirection].ColumnOffset;

            // If the next step causes the guard to leave the area then we're done
            if (rowOffset < 0 || rowOffset >= map.Length ||
                columnOffset < 0 || columnOffset >= map[0].Length)
            {
                break;
            }

            if (map[rowOffset][columnOffset] != Obstruction)
            {
                position.Row += _stepOffset[stepDirection].RowOffset;
                position.Column += _stepOffset[stepDirection].ColumnOffset;
                continue;
            }

            stepDirection = TurnRight90Degrees(stepDirection);
        } while (true);

        return _visitedPositions.Cast<bool>().Count(b => b);
    }

    private static Direction TurnRight90Degrees(Direction currentDirection) => currentDirection switch
    {
        Direction.Up => Direction.Right,
        Direction.Right => Direction.Down,
        Direction.Down => Direction.Left,
        Direction.Left => Direction.Up,
        _ => throw new ArgumentOutOfRangeException(nameof(currentDirection))
    };

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
