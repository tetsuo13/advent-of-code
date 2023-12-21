namespace AdventOfCode.Calendar._2023.Day04;

internal class Solution : BaseSolution
{
    public override async Task<int> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await TotalPoints(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private class Scratchcard
    {
        public IEnumerable<int> WinningNumbers { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<int> MyNumbers { get; set; } = Enumerable.Empty<int>();
        public int Worth => (int)Math.Pow(2, WinningNumbers.Count() - 1);
    }

    private async Task<int> TotalPoints()
    {
        var cards = await ReadInput();
        var scratchcards = new List<Scratchcard>();

        foreach (var card in cards)
        {
            var cardIntroPos = card.IndexOf(':');
            var pipePos = card.IndexOf('|');
            var winningNumbersGroup = card.Substring(cardIntroPos + 2, pipePos - cardIntroPos - 3);
            var winningNumbers = ParseOutNumbers(winningNumbersGroup);

            var scratchcard = new Scratchcard
            {
                MyNumbers = ParseOutNumbers(card[(pipePos + 1)..])
            };

            scratchcard.WinningNumbers = winningNumbers.Intersect(scratchcard.MyNumbers);
            scratchcards.Add(scratchcard);
        }

        return scratchcards.Sum(x => x.Worth);

    }

    private static IEnumerable<int> ParseOutNumbers(string s) =>
        s.Split(' ', StringSplitOptions.RemoveEmptyEntries).Select(int.Parse);
}
