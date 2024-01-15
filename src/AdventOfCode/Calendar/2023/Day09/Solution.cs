namespace AdventOfCode.Calendar._2023.Day09;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var oasisReport = await ReadInput();
        var histories = oasisReport.Select(x => x.Split(' ').Select(int.Parse));

        return runMode switch
        {
            RunMode.PartOne => SumExtrapolatedValues(histories, false),
            RunMode.PartTwo => SumExtrapolatedValues(histories, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumExtrapolatedValues(IEnumerable<IEnumerable<int>> histories, bool backwards)
    {
        return histories
            .Select(sequence =>
            {
                return (backwards ? sequence.First() : sequence.Last()) +
                    GetNextInSequence(sequence, backwards);
            })
            .Sum();
    }

    private static int GetNextInSequence(IEnumerable<int> sequence, bool backwards)
    {
        if (sequence.All(x => x == 0))
        {
            return 0;
        }

        var nextSequence = sequence.Skip(1)
            .Zip(sequence, (curr, prev) => backwards ? prev - curr : curr - prev);

        return (backwards ? nextSequence.First() : nextSequence.Last()) +
            GetNextInSequence(nextSequence, backwards);
    }
}
