using System.Collections.Specialized;

namespace AdventOfCode.Calendar._2023.Day15;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var initializationSequence = await ReadInput();
        var steps = initializationSequence.Single().Split(',');

        return runMode switch
        {
            RunMode.PartOne => SumHashSteps(steps), // Elegant
            RunMode.PartTwo => TotalFocusingPower(steps), // Not as much
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int TotalFocusingPower(IEnumerable<string> steps)
    {
        // It's ordered dictionaries all the way down...
        // There's opportunity to use/create a better collection type --
        // something that provides generic support, access by both index and
        // key, and maintains order in which values were inserted.

        // 256 boxes of lens slots.
        var boxes = new OrderedDictionary(256);

        // The Holiday ASCII String Helper Manual Arrangement Procedure.
        foreach (var step in steps)
        {
            var label = new string(step.TakeWhile(char.IsLetter).ToArray());

            // Use string hash so that we're accessing OrderedDictionary by
            // key instead of index.
            var hash = CalculateHashAlgorithm(label).ToString();

            // The next character after the label indicates one of two possible
            // operations.
            switch (step[label.Length])
            {
                case '-':
                    if (boxes.Contains(hash))
                    {
                        ((OrderedDictionary)boxes[hash]!).Remove(label);
                    }
                    break;

                case '=':
                    if (!boxes.Contains(hash))
                    {
                        boxes.Add(hash, new OrderedDictionary());
                    }

                    var lenses = (OrderedDictionary)boxes[hash]!;
                    lenses[label] = Convert.ToInt32(step[(label.Length + 1)..]);
                    break;

                default:
                    throw new NotSupportedException($"Unrecognized operator in step '{step}'");
            }
        }

        var sum = 0;

        foreach (string boxIndex in boxes.Keys)
        {
            var box = (OrderedDictionary)boxes[boxIndex]!;

            for (int slot = 0; slot < box.Count; slot++)
            {
                sum += (Convert.ToInt32(boxIndex) + 1) * (slot + 1) * (int)box[slot]!;
            }
        }

        return sum;
    }

    private static int SumHashSteps(IEnumerable<string> steps) =>
        steps.Select(CalculateHashAlgorithm).Sum();

    private static int CalculateHashAlgorithm(string s) =>
        s.Aggregate(0, (currentValue, c) => (currentValue + c) * 17 % 256);
}
