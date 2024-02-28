namespace AdventOfCode.Calendar.Year2023.Day04;

public sealed class Scratchcard
{
    public IEnumerable<int> WinningNumbers { get; set; } = Enumerable.Empty<int>();
    public IEnumerable<int> MyNumbers { get; init; } = Enumerable.Empty<int>();
    public int Worth => (int)Math.Pow(2, WinningNumbers.Count() - 1);
}
