using System.Collections.ObjectModel;

namespace AdventOfCode.Calendar._2023.Day06;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await MarginOfError(false),
            RunMode.PartTwo => await MarginOfError(true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private async Task<int> MarginOfError(bool ignoreSpaces)
    {
        var races = await ParseRaces(ignoreSpaces);

        // Number of ways you could beat the record in each race.
        var numWays = new List<long>();

        foreach (var (milliseconds, millimeters) in races)
        {
            numWays.Add(Enumerable.Range(1, (int)(milliseconds - 1))
                .Count(i => i * (milliseconds - i) > millimeters));
        }

        return (int)numWays.Aggregate(1, (long x, long y) => x * y);
    }

    private async Task<ReadOnlyDictionary<long, long>> ParseRaces(bool ignoreSpaces)
    {
        var lines = await ReadInput();
        var times = NumbersFromLine(lines[0], ignoreSpaces);
        var distances = NumbersFromLine(lines[1], ignoreSpaces);

        return times.Zip(distances, (long k, long v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v)
            .AsReadOnly();
    }

    private static IEnumerable<long> NumbersFromLine(string line, bool ignoreSpaces)
    {
        var numbers = line[(line.IndexOf(':') + 1)..];

        if (ignoreSpaces)
        {
            return new string[] { numbers.Replace(" ", string.Empty) }.Select(long.Parse);
        }

        return numbers.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
    }
}
