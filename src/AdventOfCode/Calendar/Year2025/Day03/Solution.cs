using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2025.Day03;

[PuzzleInfo(2025, 3, "Lobby")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var banks = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => TotalOutputJoltage(banks),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static double TotalOutputJoltage(string[] banks)
    {
        double sum = 0;

        foreach (var bank in banks)
        {
            int firstBattery = -1;
            int secondBattery = -1;

            // Use ASCII codes to avoid extra conversions.
            for (var i = 57; i > 49; i--)
            {
                firstBattery = bank.IndexOf((char)i);

                if (firstBattery != -1 && firstBattery < bank.Length - 1)
                {
                    break;
                }
            }

            if (firstBattery == -1)
            {
                throw new ArithmeticException("First battery wasn't found. This should not happen.");
            }

            for (var i = 57; i > 49; i--)
            {
                secondBattery = bank.IndexOf((char)i, firstBattery + 1);

                if (secondBattery != -1)
                {
                    break;
                }
            }

            if (secondBattery == -1)
            {
                throw new ArithmeticException("Second battery wasn't found. This should not happen.");
            }

            sum += char.GetNumericValue(bank[firstBattery]) * 10 + char.GetNumericValue(bank[secondBattery]);
        }

        return sum;
    }
}
