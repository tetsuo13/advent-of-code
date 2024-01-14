namespace AdventOfCode.Calendar._2023.Day09;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var oasisReport = await ReadInput();
        var histories = oasisReport.Select(x => x.Split(' ').Select(int.Parse));

        return runMode switch
        {
            RunMode.PartOne => SumExtrapolatedValues(histories),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumExtrapolatedValues(IEnumerable<IEnumerable<int>> histories)
    {
        return histories
            .Select(sequence => sequence.Last() + GetNextInSequence(sequence))
            .Sum();
    }

    private static int GetNextInSequence(IEnumerable<int> sequence)
    {
        if (sequence.All(x => x == 0))
        {
            return 0;
        }

        var nextSequence = sequence.Skip(1).Zip(sequence, (curr, prev) => curr - prev);
        return nextSequence.Last() + GetNextInSequence(nextSequence);
    }
}
