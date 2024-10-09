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

            await RunFor(RunMode.PartOne);
            await RunFor(RunMode.PartTwo);
            Console.WriteLine();
            return;

            async Task RunFor(RunMode mode)
            {
                var stopWatch = Stopwatch.StartNew();
                Console.WriteLine("Part {0}: {1} ({2} ms)",
                    (int)mode, await solution.Run(mode), stopWatch.ElapsedMilliseconds);
            }
        }
    }
}
