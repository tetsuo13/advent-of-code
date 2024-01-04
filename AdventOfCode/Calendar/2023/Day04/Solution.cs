﻿namespace AdventOfCode.Calendar._2023.Day04;

internal class Solution : BaseSolution
{
    private class Scratchcard
    {
        public IEnumerable<int> WinningNumbers { get; set; } = Enumerable.Empty<int>();
        public IEnumerable<int> MyNumbers { get; set; } = Enumerable.Empty<int>();
        public int Worth => (int)Math.Pow(2, WinningNumbers.Count() - 1);
    }

    public override async Task<int> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await TotalPoints(),
            RunMode.PartTwo => await TotalScratchcards(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private async Task<int> TotalPoints()
    {
        var scratchcards = await CountCards();
        return scratchcards.Sum(x => x.Worth);
    }

    private async Task<int> TotalScratchcards()
    {
        var scratchcards = await CountCards();
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
