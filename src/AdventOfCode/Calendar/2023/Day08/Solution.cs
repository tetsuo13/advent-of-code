﻿using System.Collections.ObjectModel;

namespace AdventOfCode.Calendar._2023.Day08;

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

    private static ulong RequiredStepsSimultaneously(List<int> instructions,
        ReadOnlyDictionary<string, string[]> network)
    {
        static bool stopCondition(string name) => name.EndsWith('Z');

        var steps = network.Keys
            .Where(name => name.EndsWith('A'))
            .Select(name => TraverseNetwork(instructions, name, network, stopCondition))
            .ToList();

        return LeastCommonMultiple(steps);
    }

    private static ulong RequiredSteps(List<int> instructions, ReadOnlyDictionary<string, string[]> network)
    {
        static bool stopCondition(string name) => name == "ZZZ";
        return TraverseNetwork(instructions, "AAA", network, stopCondition);
    }

    private static ulong TraverseNetwork(List<int> instructions, string startingElement,
        ReadOnlyDictionary<string, string[]> network, Func<string, bool> stop)
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

    private static List<int> ParseInstructions(string[] lines) =>
        lines[0].ToCharArray()
            .Select(x => x == 'L' ? 0 : 1)
            .ToList();

    private static ReadOnlyDictionary<string, string[]> ParseNetwork(string[] lines)
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

    /// <summary>
    /// Calculate the least common multiple across a set of numbers.
    /// </summary>
    private static ulong LeastCommonMultiple(IEnumerable<ulong> numbers) =>
        numbers.Aggregate((a, b) => a * b / GreatestCommonDivisor(a, b));

    /// <summary>
    /// Calculate the greatest common divisor of two numbers.
    /// </summary>
    private static ulong GreatestCommonDivisor(ulong a, ulong b)
    {
        // Division-based version of the Euclidean algorithm.
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }

        return a;
    }
}
