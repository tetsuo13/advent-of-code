namespace AdventOfCode.Calendar._2023.Day07;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var hands = ParseInput(await ReadInput());

        return runMode switch
        {
            RunMode.PartOne => TotalWinningsByStrength(hands),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int TotalWinningsByStrength(IEnumerable<Hand> hands) =>
        hands.Order()
            .Select((hand, rank) => hand.Bid * (rank + 1))
            .Sum();

    private static IEnumerable<Hand> ParseInput(string[] input) =>
        input.Select(line =>
        {
            var parts = line.Split(' ');
            return new Hand(parts[0], Convert.ToInt32(parts[1]));
        });
}
