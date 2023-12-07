using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode._2023;

internal sealed class Program
{
    private const RunMode RunModel = RunMode.PartTwo;

    private static readonly Dictionary<string, int> NumberWords = new()
    {
        { "one", 1 },
        { "two", 2 },
        { "three", 3 },
        { "four", 4 },
        { "five", 5 },
        { "six", 6 },
        { "seven", 7 },
        { "eight", 8 },
        { "nine", 9 },
    };

    private enum RunMode
    {
        PartOne,
        PartTwo
    }

    private static async Task Main()
    {
        var calibrationValues = await CalibrationValues(RunModel == RunMode.PartOne ? PartOne : PartTwo);

        Console.WriteLine("What is the sum of all of the calibration values? {0}", calibrationValues);
    }

    private static async Task<int> CalibrationValues(Func<string, int> part)
    {
        var calibrationValues = 0;

        foreach (var line in await File.ReadAllLinesAsync("input.txt"))
        {
            try
            {
                calibrationValues += part(line);
            }
            catch (Exception e)
            {
                Console.WriteLine($"Unexpected {e.GetType()} error processing '{line}': {e.Message}");
                throw;
            }
        }

        return calibrationValues;
    }

    /// <summary>
    /// Return a two-digit number. The first number encountered in the line will
    /// represent the tens place. The last number will be the ones place.
    /// </summary>
    private static int PartOne(string line) =>
        10 * AsNumber(line.First(char.IsDigit)) + AsNumber(line.Last(char.IsDigit));

    /// <summary>
    /// Make two passes against the line: forward from beginning to end looking
    /// for either a number or word and again backwards from end to beginning
    /// looking for the same thing. The number found from the first pass will
    /// represent the tens place, the second pass represent the ones place.
    /// </summary>
    [SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used")]
    private static int PartTwo(string line)
    {
        var calibrationValue = 0;

        for (var i = 0; i < line.Length; i++)
        {
            if (char.IsDigit(line[i]))
            {
                calibrationValue = 10 * AsNumber(line[i]);
                break;
            }

            foreach (var (numberWord, numberValue) in NumberWords)
            {
                if (i + numberWord.Length < line.Length &&
                    line.Substring(i, numberWord.Length) == numberWord)
                {
                    calibrationValue = 10 * numberValue;
                    goto StartReverse;
                }
            }
        }

        StartReverse:

        // Find the last number, keep as the last digit
        for (var i = line.Length - 1; i >= 0; i--)
        {
            if (char.IsDigit(line[i]))
            {
                calibrationValue += AsNumber(line[i]);
                break;
            }

            foreach (var (numberWord, numberValue) in NumberWords)
            {
                if (i + numberWord.Length <= line.Length &&
                    line.Substring(i, numberWord.Length).Contains(numberWord))
                {
                    calibrationValue += numberValue;
                    goto EndPasses;
                }
            }
        }

        EndPasses:

        return calibrationValue;
    }

    private static int AsNumber(char c) => c - '0';
}
