namespace AdventOfCode.Calendar.Year2023.Day02;

public class Solution : BaseSolution
{
    private readonly SetOfCubes _constraint = new()
    {
        Red = 12,
        Green = 13,
        Blue = 14
    };

    public override async Task<object> Run(RunMode runMode)
    {
        var lines = await ReadInput();

        return runMode switch
        {
            RunMode.PartOne => SumGameIds(lines),
            RunMode.PartTwo => SumPowerSets(lines),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int SumGameIds(IReadOnlyList<string> lines)
    {
        var sum = 0;

        for (var gameId = 0; gameId < lines.Count; gameId++)
        {
            var sets = SetsInGame(lines[gameId]);

            if (sets.TrueForAll(x => x.Red <= _constraint.Red &&
                x.Green <= _constraint.Green &&
                x.Blue <= _constraint.Blue))
            {
                sum += gameId + 1;
            }
        }

        return sum;
    }

    private static int SumPowerSets(IEnumerable<string> lines)
    {
        var sum = 0;

        foreach (var line in lines)
        {
            var sets = SetsInGame(line);

            sum += sets.Max(x => x.Red) * sets.Max(x => x.Green) * sets.Max(x => x.Blue);
        }

        return sum;
    }

    private static List<SetOfCubes> SetsInGame(string line)
    {
        // Strip the leading "Game x:" information
        line = line[(line.IndexOf(':') + 2)..];
        return Tokenize(line);
    }

    private static List<SetOfCubes> Tokenize(string line)
    {
        var sets = new List<SetOfCubes>();

        foreach (var elements in line.Split(';', StringSplitOptions.TrimEntries))
        {
            var set = new SetOfCubes();
            var cubes = elements.Split(',', StringSplitOptions.TrimEntries);

            foreach (var cube in cubes)
            {
                (int number, string color) = cube.Split() switch { var x => (Convert.ToInt32(x[0]), x[1]) };
                set[color] = number;
            }

            sets.Add(set);
        }

        return sets;
    }
}
