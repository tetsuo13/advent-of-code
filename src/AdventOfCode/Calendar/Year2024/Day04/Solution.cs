using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day04;

[PuzzleInfo(2024, 4, "Ceres Search")]
public class Solution : BaseSolution
{
    private enum Direction
    {
        Up,
        Down,
        Left,
        Right,
        DiagonalUpRight,
        DiagonalDownRight,
        DiagonalUpLeft,
        DiagonalDownLeft,
    }

    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => NumOccurrencesOfWord(inputLines, "XMAS"),
            RunMode.PartTwo => NumOccurrencesOfXmas(inputLines),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int NumOccurrencesOfXmas(string[] inputLines)
    {
        var sum = 0;

        for (var row = 0; row < inputLines.Length; row++)
        {
            for (var col = 0; col < inputLines[row].Length; col++)
            {
                // Found a middle A, ensure potential surrounding candidate X
                // is within bounds.
                if (inputLines[row][col] != 'A' ||
                    row - 1 < 0 || row + 1 >= inputLines.Length ||
                    col - 1 < 0 || col + 1 >= inputLines[row].Length)
                {
                    continue;
                }

                // Create a four-character string containing the bounding
                // characters.
                var candidate = new string([
                    inputLines[row - 1][col - 1], // top-left
                    inputLines[row - 1][col + 1], // top-right
                    inputLines[row + 1][col - 1], // bottom-left
                    inputLines[row + 1][col + 1]  // bottom-right
                ]);

                // There are four possible ways that two "MAS"es can exist
                sum += candidate switch
                {
                    "MSMS" or "SSMM" or "MMSS" or "SMSM" => 1,
                    _ => 0
                };
            }
        }

        return sum;
    }

    /// <summary>
    /// Uses depth-first algorithm.
    /// </summary>
    private static int NumOccurrencesOfWord(string[] inputLines, string word)
    {
        var sum = 0;

        for (var row = 0; row < inputLines.Length; row++)
        {
            for (var col = 0; col < inputLines[row].Length; col++)
            {
                if (inputLines[row][col] == word[0])
                {
                    sum += NumTimesFoundStartingAt(inputLines, row, col, word);
                }
            }
        }

        return sum;
    }

    // Assumes starting row/col matches first letter of word
    private static int NumTimesFoundStartingAt(string[] inputLines, int startingRow, int startingCol, string word)
    {
        var times = 0;

        // Need to look at every possible direction from starting point as
        // word may appear in multiple directions.
        foreach (var direction in Enum.GetValues<Direction>())
        {
            if (SearchDirection(inputLines, startingRow, startingCol, word, direction))
            {
                times++;
            }
        }

        return times;
    }

    private static bool SearchDirection(string[] inputLines, int startingRow, int startingCol, string word,
        Direction direction)
    {
        for (var i = 1; i < word.Length; i++)
        {
            var row = direction switch
            {
                Direction.Left or Direction.Right => startingRow,
                Direction.Up or Direction.DiagonalUpLeft or Direction.DiagonalUpRight => startingRow - i,
                Direction.Down or Direction.DiagonalDownLeft or Direction.DiagonalDownRight => startingRow + i,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };

            var col = direction switch
            {
                Direction.Left or Direction.DiagonalDownLeft or Direction.DiagonalUpLeft => startingCol - i,
                Direction.Right or Direction.DiagonalDownRight or Direction.DiagonalUpRight => startingCol + i,
                Direction.Up or Direction.Down => startingCol,
                _ => throw new ArgumentOutOfRangeException(nameof(direction))
            };

            if (row < 0 || row >= inputLines.Length ||
                col < 0 || col >= inputLines[row].Length ||
                inputLines[row][col] != word[i])
            {
                return false;
            }
        }

        return true;
    }
}
