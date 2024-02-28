namespace AdventOfCode.Calendar.Year2023.Day19;

public class Solution : BaseSolution
{
    private const string StartingWorkflowName = "in";
    private const string RejectedRule = "R";
    private const string AcceptedRule = "A";

    public override async Task<object> Run(RunMode runMode)
    {
        var lines = await ReadInput();
        var separatorIndex = Array.IndexOf(lines, string.Empty);
        var workflows = ParseWorkflows(lines.Take(separatorIndex));
        var partRatings = ParsePartRatings(lines.Skip(separatorIndex + 1));

        return runMode switch
        {
            RunMode.PartOne => SumAcceptedRatings(workflows, partRatings),
            RunMode.PartTwo => 0,
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumAcceptedRatings(IReadOnlyDictionary<string, List<Rule>> workflows,
        List<Dictionary<char, int>> partRatings)
    {
        var sum = 0;

        foreach (var part in partRatings)
        {
            var currentWorkflow = StartingWorkflowName;

            do
            {
                currentWorkflow = EvaluateRules(part, workflows[currentWorkflow]);
            }
            while (currentWorkflow != RejectedRule && currentWorkflow != AcceptedRule);

            if (currentWorkflow == RejectedRule)
            {
                continue;
            }

            // Accepted
            sum += part.Sum(x => x.Value);
        }

        return sum;
    }

    private static string EvaluateRules(IReadOnlyDictionary<char, int> part, List<Rule> workflow)
    {
        foreach (var rule in workflow)
        {
            // No other rules match the part.
            if (rule.Category == default)
            {
                return rule.Destination;
            }

            var partRating = part[rule.Category];

            switch (rule.Op)
            {
                case '<' when partRating < rule.Rating:
                    return rule.Destination;

                case '>' when partRating > rule.Rating:
                    return rule.Destination;
            }
        }

        throw new ArithmeticException("No rules applied to part");
    }

    private static Dictionary<string, List<Rule>> ParseWorkflows(IEnumerable<string> lines)
    {
        var workflows = new Dictionary<string, List<Rule>>();

        // Each line looks like this: px{a<2006:qkq,m>2090:A,rfg}
        foreach (var line in lines)
        {
            var rules = new List<Rule>();
            var start = line.IndexOf('{');
            var name = line[..start];
            var ruleParts = line[(start + 1)..^1].Split(',');

            foreach (var part in ruleParts)
            {
                Rule rule;

                if (part.Contains(':'))
                {
                    var category = part[0];
                    var op = part[1];
                    var colon = part.IndexOf(':');
                    var rating = Convert.ToInt32(part[2..colon]);
                    var destination = part[(colon + 1)..];
                    rule = new Rule(category, op, rating, destination);
                }
                else
                {
                    rule = new Rule(part);
                }

                rules.Add(rule);
            }

            workflows.Add(name, rules);
        }

        return workflows;
    }

    private static List<Dictionary<char, int>> ParsePartRatings(IEnumerable<string> lines)
    {
        var partRatings = new List<Dictionary<char, int>>();

        // Each line looks like this: {x=787,m=2655,a=1222,s=2876}
        foreach (var line in lines)
        {
            partRatings.Add(line[1..^1]
                .Split(',')
                .Select(x => x.Split('='))
                .Select(x => new { Category = char.Parse(x[0]), Rating = Convert.ToInt32(x[1]) })
                .ToDictionary(x => x.Category, x => x.Rating));
        }

        return partRatings;
    }
}
