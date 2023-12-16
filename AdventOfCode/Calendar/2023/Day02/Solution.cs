using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace AdventOfCode.Calendar._2023.Day02;

public class Solution : BaseSolution
{
    private readonly SetOfCubes Constraint = new()
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

        public int this[string colorName]
        {
            set
            {
                GetType()
                    .GetProperty(colorName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                    !.SetValue(this, value, null);
            }
        }
    }

    public async Task<int> Run(RunMode runMode)
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
            // Strip the leading "Game x:" information
            var line = lines[gameId][(lines[gameId].IndexOf(':') + 2)..];
            var sets = Tokenize(line);

            if (sets.TrueForAll(x => x.Red <= Constraint.Red &&
                x.Green <= Constraint.Green &&
                x.Blue <= Constraint.Blue))
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
            // Strip the leading "Game x:" information
            var line = lines[gameId][(lines[gameId].IndexOf(':') + 2)..];
            var sets = Tokenize(line);

            sum += sets.Max(x => x.Red) * sets.Max(x => x.Green) * sets.Max(x => x.Blue);
        }

        return sum;
    }

    private List<SetOfCubes> Tokenize(string line)
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
