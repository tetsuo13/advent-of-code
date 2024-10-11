using System.Diagnostics;
using AdventOfCode.Calendar;
using AdventOfCode.Common;
using AdventOfCode.Runner.Utils;

namespace AdventOfCode.Runner;

public class SolutionRunner
{
    private readonly IEnumerable<BaseSolution> _solutionsToRun = SolutionFinder.FindSolutionsWithInputs();

    public async Task RunAsync()
    {
        foreach (var solution in _solutionsToRun)
        {
            Console.WriteLine("--- {0} ---", solution);

            await RunFor(solution, RunMode.PartOne);
            await RunFor(solution, RunMode.PartTwo);
            Console.WriteLine();
        }
    }

    private static async Task RunFor(BaseSolution solution, RunMode mode)
    {
        var stopWatch = Stopwatch.StartNew();
        Console.WriteLine("Part {0}: {1} ({2} ms)",
            (int)mode, await solution.Run(mode), stopWatch.ElapsedMilliseconds);
    }
}
