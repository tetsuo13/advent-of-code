using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day09;

[PuzzleInfo(2023, 9, "Mirage Maintenance")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var oasisReport = ReadInput();
        var histories = oasisReport.Select(x => x.Split(' ').Select(int.Parse).ToList()).ToList();

        return runMode switch
        {
            RunMode.PartOne => SumExtrapolatedValues(histories, false),
            RunMode.PartTwo => SumExtrapolatedValues(histories, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumExtrapolatedValues(IEnumerable<List<int>> histories, bool backwards)
    {
        return histories
            .Select(sequence =>
            {
                return (backwards ? sequence.First() : sequence.Last()) +
                    GetNextInSequence(sequence, backwards);
            })
            .Sum();
    }

    private static int GetNextInSequence(IReadOnlyCollection<int> sequence, bool backwards)
    {
        if (sequence.All(x => x == 0))
        {
            return 0;
        }

        var nextSequence = sequence.Skip(1)
            .Zip(sequence, (curr, prev) => backwards ? prev - curr : curr - prev)
            .ToList();

        return (backwards ? nextSequence.First() : nextSequence.Last()) +
            GetNextInSequence(nextSequence, backwards);
    }
}
