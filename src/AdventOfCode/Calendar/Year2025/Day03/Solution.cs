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
            RunMode.PartOne => TotalOutputJoltage(banks, 2),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static double TotalOutputJoltage(string[] banks, int batteriesPerBank)
    {
        double sum = 0;

        foreach (var bank in banks)
        {
            var placeValueMultiplier = Math.Pow(10, batteriesPerBank - 1);
            var lastBatteryPos = -1;

            for (var currentBattery = 0; currentBattery < batteriesPerBank; currentBattery++)
            {
                var batteryIndex = -1;

                // Use ASCII values to reduce conversions since we're working
                // with chars. Omitting zero, it doesn't appear in the input.
                // ASCII 57 = number 9
                // ASCII 49 = number 1
                for (var j = 57; j >= 49; j--)
                {
                    batteryIndex = bank.IndexOf((char)j, lastBatteryPos + 1);

                    if (batteryIndex == -1)
                    {
                        continue;
                    }

                    // Don't count it if it's found in the last position when
                    // looking at the first battery. Minimum of two batteries
                    // required.
                    if (currentBattery == 0 && batteryIndex == bank.Length - 1)
                    {
                        continue;
                    }

                    break;
                }

                if (batteryIndex == -1)
                {
                    throw new ArithmeticException(
                        $"Battery {currentBattery + 1} wasn't found in bank {bank}. This should never happen. Current joltage: {sum}");
                }

                sum += char.GetNumericValue(bank[batteryIndex]) * placeValueMultiplier;

                placeValueMultiplier /= 10;
                lastBatteryPos = batteryIndex;
            }
        }

        return sum;
    }
}
