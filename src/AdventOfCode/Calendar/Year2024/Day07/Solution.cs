using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day07;

/// <summary>
/// This is one of the slowest solutions yet, taking around 25 and 2500 ms for
/// parts one and two, respectively. It's a brute force algorithm using
/// recursion without any kind of caching. There's opportunity to change the
/// algorithm to process RTL instead of LTR.
/// </summary>
[PuzzleInfo(2024, 7, "Bridge Repair")]
public class Solution : BaseSolution
{
    private bool _useConcatOperator;

    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();
        var equations = ParseEquations(inputLines);

        return runMode switch
        {
            RunMode.PartOne => TotalCalibrationResult(equations, false),
            RunMode.PartTwo => TotalCalibrationResult(equations, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private long TotalCalibrationResult(Dictionary<long, long[]> equations, bool useConcatOperator)
    {
        var sum = 0L;
        _useConcatOperator = useConcatOperator;

         foreach ((long testValue, long[] numbers) in equations)
         {
             if (CanProduceTestValue(testValue, 0, numbers, 0))
             {
                 sum += testValue;
             }
         }

        return sum;
    }

    private bool CanProduceTestValue(long testValue, long remainingTestValue, long[] numbers, int index)
    {
        // Ran out of numbers to test
        if (index > numbers.Length - 1)
        {
            // A remainder of zero means a correct set of operators was found
            // in order to hit the test value exactly.
            return remainingTestValue == testValue;
        }

        if (remainingTestValue < 0)
        {
            return false;
        }

        var result = CanProduceTestValue(testValue, remainingTestValue * numbers[index], numbers, index + 1) ||
                     CanProduceTestValue(testValue, remainingTestValue + numbers[index], numbers, index + 1);

        if (_useConcatOperator)
        {
            result |= CanProduceTestValue(testValue, long.Parse($"{remainingTestValue}{numbers[index]}"),
                numbers, index + 1);
        }

        return result;
    }

    private static Dictionary<long, long[]> ParseEquations(string[] inputLines)
    {
        var equations = new Dictionary<long, long[]>();

        foreach (var line in inputLines)
        {
            var separator = line.IndexOf(':');
            var testValue = long.Parse(line[..separator]);
            var numbers = line[(separator + 2)..].Split(' ').Select(long.Parse).ToArray();
            equations.Add(testValue, numbers);
        }

        return equations;
    }
}
