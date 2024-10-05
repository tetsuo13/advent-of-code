using AdventOfCode.Calendar;
using AdventOfCode.Common;
using AdventOfCode.Runner.Benchmark.Configs;
using AdventOfCode.Runner.Utils;
using BenchmarkDotNet.Attributes;

// TODO: Have Benchmark write the value to a file if it doesn't exist (output_YYYYmmdd.txt). Column class just reads this value.

namespace AdventOfCode.Runner;

[MemoryDiagnoser]
[Config(typeof(SolutionConfig))]
public class BenchmarkSolutionRunner
{
    [ParamsSource(nameof(SolutionsToRun))]
    public BaseSolution Solution;

    public IEnumerable<BaseSolution> SolutionsToRun { get; init; }

    [ParamsAllValues]
    public RunMode PartToRun;

    public BenchmarkSolutionRunner()
    {
        SolutionsToRun = SolutionFinder.FindSolutionsWithInputs();
    }

    [Benchmark]
    public async Task<object> Benchmark()
    {
        return await Solution.Run(PartToRun);

        // TODO: Write a file in the
    }
}
