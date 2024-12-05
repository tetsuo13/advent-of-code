using System.Diagnostics;
using AdventOfCode.Calendar;
using AdventOfCode.Common;
using AdventOfCode.Runner.Utils;

namespace AdventOfCode.Runner;

public class SolutionRunner
{
    private readonly IEnumerable<BaseSolution> _solutionsToRun = SolutionFinder.FindSolutionsWithInputs();

    public void Run()
    {
        foreach (var solution in _solutionsToRun)
        {
            Console.WriteLine("--- {0} ---", solution);

            RunFor(solution, RunMode.PartOne);
            RunFor(solution, RunMode.PartTwo);
            Console.WriteLine();
        }
    }

    private static void RunFor(BaseSolution solution, RunMode mode)
    {
        var stopWatch = Stopwatch.StartNew();
        Console.WriteLine("Part {0}: {1} ({2} ms)",
            (int)mode, solution.Run(mode), stopWatch.ElapsedMilliseconds);
    }
}
