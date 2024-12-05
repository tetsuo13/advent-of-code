using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day07;

[PuzzleInfo(2023, 7, "Camel Cards")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => TotalWinningsByStrength(false),
            RunMode.PartTwo => TotalWinningsByStrength(true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int TotalWinningsByStrength(bool withJokerRule) =>
        ParseInput(ReadInput())
            .Order(new CamelCardsComparer(withJokerRule))
            .Select((hand, rank) => hand.Bid * (rank + 1))
            .Sum();

    private static IEnumerable<Hand> ParseInput(IEnumerable<string> input) =>
        input.Select(line =>
        {
            var parts = line.Split(' ');
            return new Hand(parts[0], Convert.ToInt32(parts[1]));
        });
}
