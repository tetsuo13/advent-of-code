using System.Collections.ObjectModel;
using AdventOfCode.Utilities.Mathematics;

namespace AdventOfCode.Calendar.Year2023.Day08;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var lines = await ReadInput();
        var instructions = ParseInstructions(lines);
        var network = ParseNetwork(lines);

        return runMode switch
        {
            RunMode.PartOne => RequiredSteps(instructions, network),
            RunMode.PartTwo => RequiredStepsSimultaneously(instructions, network),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static ulong RequiredStepsSimultaneously(IReadOnlyList<int> instructions,
        ReadOnlyDictionary<string, string[]> network)
    {
        var steps = network.Keys
            .Where(name => name.EndsWith('A'))
            .Select(name => TraverseNetwork(instructions, name, network, StopCondition))
            .ToList();

        return NumericUtilities.LeastCommonMultiple(steps);

        static bool StopCondition(string name) => name.EndsWith('Z');
    }

    private static ulong RequiredSteps(IReadOnlyList<int> instructions, IReadOnlyDictionary<string, string[]> network)
    {
        return TraverseNetwork(instructions, "AAA", network, StopCondition);
        static bool StopCondition(string name) => name == "ZZZ";
    }

    private static ulong TraverseNetwork(IReadOnlyList<int> instructions, string startingElement,
        IReadOnlyDictionary<string, string[]> network, Func<string, bool> stop)
    {
        ulong stepsTaken = 0;
        var currentInstruction = 0;
        var currentElement = startingElement;

        while (!stop(currentElement))
        {
            currentElement = network[currentElement][instructions[currentInstruction]];
            stepsTaken++;
            currentInstruction++;

            if (currentInstruction > instructions.Count - 1)
            {
                currentInstruction = 0;
            }
        }

        return stepsTaken;
    }

    private static List<int> ParseInstructions(IReadOnlyList<string> lines) =>
        lines[0].ToCharArray()
            .Select(x => x == 'L' ? 0 : 1)
            .ToList();

    private static ReadOnlyDictionary<string, string[]> ParseNetwork(IEnumerable<string> lines)
    {
        var nodes = new Dictionary<string, string[]>();

        // First line is left/right instruction set, second line is blank.
        foreach (var line in lines.Skip(2))
        {
            var element = line[..3];
            var nextNode = line[7..^1].Split(',', StringSplitOptions.TrimEntries);
            nodes.Add(element, nextNode);
        }

        return nodes.AsReadOnly();
    }
}
