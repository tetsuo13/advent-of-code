namespace AdventOfCode.Calendar.Year2023.Day07;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await TotalWinningsByStrength(false),
            RunMode.PartTwo => await TotalWinningsByStrength(true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private async Task<int> TotalWinningsByStrength(bool withJokerRule) =>
        ParseInput(await ReadInput())
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
