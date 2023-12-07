using System.Reflection;

namespace AdventOfCode._2023;

internal sealed class Program
{
    private const RunMode RunModel = RunMode.PartTwo;

    private enum RunMode
    {
        PartOne,
        PartTwo
    }

    private static readonly SetOfCubes Constraint = new()
    {
        Red = 12,
        Green = 13,
        Blue = 14
    };

    private static void Main()
    {
        var sum = 0;
        var lines = File.ReadAllLines("input.txt");

        for (var gameId = 0; gameId < lines.Length; gameId++)
        {
            // Strip the leading "Game x:" information
            var line = lines[gameId][(lines[gameId].IndexOf(':') + 2)..];
            var sets = Tokenize(line);

            switch (RunModel)
            {
                case RunMode.PartOne:
                    if (sets.TrueForAll(x => x.Red <= Constraint.Red &&
                        x.Green <= Constraint.Green &&
                        x.Blue <= Constraint.Blue))
                    {
                        sum += gameId + 1;
                    }
                    break;

                case RunMode.PartTwo:
                    sum += sets.Max(x => x.Red) * sets.Max(x => x.Green) * sets.Max(x => x.Blue);
                    break;
            }
        }

        if (RunModel == RunMode.PartOne)
        {
            Console.WriteLine($"What is the sum of the IDs of those games? {sum}");
        }
        else
        {
            Console.WriteLine($"What is the sum of the power of these sets? {sum}");
        }
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
}
