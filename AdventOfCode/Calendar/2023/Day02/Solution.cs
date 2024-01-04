using System.Reflection;

namespace AdventOfCode.Calendar._2023.Day02;

internal class Solution : BaseSolution
{
    private readonly SetOfCubes _constraint = new()
    {
        Red = 12,
        Green = 13,
        Blue = 14
    };

    private sealed class SetOfCubes()
    {
        public int Red { get; set; }
        public int Green { get; set; }
        public int Blue { get; set; }

#pragma warning disable S1144 // Unused private types or members should be removed
        public int this[string colorName]
#pragma warning restore S1144 // Unused private types or members should be removed
        {
            set
            {
                GetType()
                    .GetProperty(colorName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                    !.SetValue(this, value, null);
            }
        }
    }

    public override async Task<int> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await SumGameIds(),
            RunMode.PartTwo => await SumPowerSets(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private async Task<int> SumGameIds()
    {
        var lines = await ReadInput();
        var sum = 0;

        for (var gameId = 0; gameId < lines.Length; gameId++)
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

    private async Task<int> SumPowerSets()
    {
        var lines = await ReadInput();
        var sum = 0;

        for (var gameId = 0; gameId < lines.Length; gameId++)
        {
            var sets = SetsInGame(lines[gameId]);

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
                var (number, color) = cube.Split() switch { var x => (Convert.ToInt32(x[0]), x[1]) };
                set[color] = number;
            }

            sets.Add(set);
        }

        return sets;
    }
}
