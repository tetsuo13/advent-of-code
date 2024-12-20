﻿using System.Collections.ObjectModel;
using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2023.Day06;

[PuzzleInfo(2023, 6, "Wait For It")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => MarginOfError(false),
            RunMode.PartTwo => MarginOfError(true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int MarginOfError(bool ignoreSpaces)
    {
        var races = ParseRaces(ignoreSpaces);

        // Number of ways you could beat the record in each race.
        var numWays = new List<long>();

        foreach ((long milliseconds, long millimeters) in races)
        {
            numWays.Add(Enumerable.Range(1, (int)(milliseconds - 1))
                .Count(i => i * (milliseconds - i) > millimeters));
        }

        return (int)numWays.Aggregate(1, (long x, long y) => x * y);
    }

    private ReadOnlyDictionary<long, long> ParseRaces(bool ignoreSpaces)
    {
        var lines = ReadInput();
        var times = NumbersFromLine(lines[0], ignoreSpaces);
        var distances = NumbersFromLine(lines[1], ignoreSpaces);

        return times.Zip(distances, (k, v) => new { k, v })
            .ToDictionary(x => x.k, x => x.v)
            .AsReadOnly();
    }

    private static IEnumerable<long> NumbersFromLine(string line, bool ignoreSpaces)
    {
        var numbers = line[(line.IndexOf(':') + 1)..];

        if (ignoreSpaces)
        {
            return new[] { numbers.Replace(" ", string.Empty) }.Select(long.Parse);
        }

        return numbers.Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(long.Parse);
    }
}
