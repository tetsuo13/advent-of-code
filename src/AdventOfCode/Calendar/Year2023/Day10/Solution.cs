using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day10;

[PuzzleInfo(2023, 10, "Pipe Maze")]
public class Solution : BaseSolution
{
    private readonly record struct Point(long X, long Y);

    private const string StartingPosition = "S";

    public override async Task<object> Run(RunMode runMode)
    {
        var lines = await ReadInput();
        var field = CreateFieldGrid(lines);

        return runMode switch
        {
            RunMode.PartOne => FarthestFromStartingPosition(field),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int FarthestFromStartingPosition(string[,] field)
    {
        var startingPosition = FindStartingPosition(field);
        var steps = 0;

        var next = GetNextSignificantPosition(field, startingPosition);

        return steps;
    }

    private static Point GetNextSignificantPosition(string[,] field, Point currentPosition)
    {
        return new Point(1, 1);
    }

    private static Point FindStartingPosition(string[,] field)
    {
        for (var x = 0; x < field.GetLength(0); x++)
        {
            for (var y = 0; y < field.GetLength(1); y++)
            {
                if (field[x, y] == StartingPosition)
                {
                    return new Point(x, y);
                }
            }
        }

        throw new InvalidDataException("Field missing starting position");
    }

    private static string[,] CreateFieldGrid(IReadOnlyList<string> lines)
    {
        var width = lines[0].Length;
        var height = lines.Count;
        string[,] field = new string[width, height];

        for (var i = 0; i < height; i++)
        {
            for (var j = 0; j < width; j++)
            {
                field[i, j] = lines[i][j].ToString();
            }
        }

        return field;
    }
}
