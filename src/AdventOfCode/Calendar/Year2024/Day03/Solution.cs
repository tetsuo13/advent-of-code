using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2024.Day03;

/// <summary>
/// Avoiding regular expressions because that would be too easy.
/// </summary>
[PuzzleInfo(2024, 3, "Mull It Over")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var inputLines = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => SumMulInstructions(inputLines, false),
            RunMode.PartTwo => SumMulInstructions(inputLines, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumMulInstructions(string[] lines, bool useConditions)
    {
        var sum = 0;

        // mul instructions should be enabled at the start of the program.
        var enableMulInstructions = true;

        foreach (var line in lines)
        {
            var mul = line.Split("mul");

            foreach (var instruction in mul)
            {
                var mulInstructionResult = enableMulInstructions ? ParseMulInstruction(instruction) : null;

                if (mulInstructionResult is not null)
                {
                    sum += mulInstructionResult.Value;
                }

                if (!useConditions)
                {
                    continue;
                }

                if (instruction.LastIndexOf("don't()", StringComparison.InvariantCulture) != -1)
                {
                    enableMulInstructions = false;
                }
                else if (instruction.LastIndexOf("do()", StringComparison.InvariantCulture) != -1)
                {
                    enableMulInstructions = true;
                }
            }
        }

        return sum;
    }

    private static int? ParseMulInstruction(string instruction)
    {
        if (instruction[0] != '(')
        {
            return null;
        }

        var parts = instruction.Split(',');

        if (!int.TryParse(parts[0][1..], out var x))
        {
            return null;
        }

        var closingPos = parts[1].IndexOf(')');

        // No closing parenthesis or number is greater than 3 digits.
        if (closingPos is -1 or > 3)
        {
            return null;
        }

        if (!int.TryParse(parts[1][..closingPos], out var y))
        {
            return null;
        }

        return x * y;
    }
}
