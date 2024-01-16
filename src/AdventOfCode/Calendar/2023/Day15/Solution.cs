namespace AdventOfCode.Calendar._2023.Day15;

public class Solution : BaseSolution
{
    public override async Task<object> Run(RunMode runMode)
    {
        var initializationSequence = await ReadInput();
        var steps = initializationSequence.Single().Split(',');

        return runMode switch
        {
            RunMode.PartOne => SumHashSteps(steps),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private static int SumHashSteps(string[] steps) =>
        steps.Select(CalculateHashAlgorithm).Sum();

    private static int CalculateHashAlgorithm(string s) =>
        s.Aggregate(0, (currentValue, c) => (currentValue + c) * 17 % 256);
}
