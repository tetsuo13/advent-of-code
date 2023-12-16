using System.Diagnostics.CodeAnalysis;

namespace AdventOfCode.Calendar._2023.Day01;

internal class Solution : BaseSolution
{
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

    public async Task<int> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await SumCalibrationValues(),
            RunMode.PartTwo => await SumCalibrationValuesPartTwo(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    /// <summary>
    /// Return a two-digit number. The first number encountered in the line will
    /// represent the tens place. The last number will be the ones place.
    /// </summary>
    private async Task<int> SumCalibrationValues()
    {
        var inputLines = await ReadInput();
        return inputLines.Sum(line =>
        {
            return 10 * AsNumber(line.First(char.IsDigit)) +
                AsNumber(line.Last(char.IsDigit));
        });
    }

    /// <summary>
    /// Make two passes against the line: forward from beginning to end looking
    /// for either a number or word and again backwards from end to beginning
    /// looking for the same thing. The number found from the first pass will
    /// represent the tens place, the second pass represent the ones place.
    /// </summary>
    [SuppressMessage("Major Code Smell", "S907:\"goto\" statement should not be used")]
    private async Task<int> SumCalibrationValuesPartTwo()
    {
        var calibrationValue = 0;

        foreach (var line in await ReadInput())
        {
            for (var i = 0; i < line.Length; i++)
            {
                if (char.IsDigit(line[i]))
                {
                    calibrationValue += 10 * AsNumber(line[i]);
                    break;
                }

                foreach (var (numberWord, numberValue) in NumberWords)
                {
                    if (i + numberWord.Length < line.Length &&
                        line.Substring(i, numberWord.Length) == numberWord)
                    {
                        calibrationValue += 10 * numberValue;
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
            ;
        }

        return calibrationValue;
    }

    private static int AsNumber(char c) => c - '0';
}
