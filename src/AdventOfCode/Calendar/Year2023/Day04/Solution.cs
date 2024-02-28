namespace AdventOfCode.Calendar.Year2023.Day04;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var scratchcards = await CountCards();

        return runMode switch
        {
            RunMode.PartOne => TotalPoints(scratchcards),
            RunMode.PartTwo => TotalScratchcards(scratchcards),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int TotalPoints(IEnumerable<Scratchcard> scratchcards) =>
        scratchcards.Sum(x => x.Worth);

    private static int TotalScratchcards(IList<Scratchcard> scratchcards)
    {
        var counts = Enumerable.Repeat(1, scratchcards.Count).ToArray();

        for (var i = 0; i < scratchcards.Count; i++)
        {
            for (var j = 1; j <= scratchcards[i].WinningNumbers.Count(); j++)
            {
                counts[i + j] += counts[i];
            }
        }

        return counts.Sum();
    }

    private async Task<List<Scratchcard>> CountCards()
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

        return scratchcards;
    }

    private static IEnumerable<int> ParseOutNumbers(string s)
    {
        return s.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse);
    }
}
