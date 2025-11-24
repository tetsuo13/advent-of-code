using AdventOfCode.Calendar;
using AdventOfCode.Common;
using AdventOfCode.Runner.Benchmark.Columns;
using AdventOfCode.Runner.Benchmark.Configs;
using AdventOfCode.Runner.Benchmark.Utils;
using AdventOfCode.Runner.Utils;
using BenchmarkDotNet.Attributes;

namespace AdventOfCode.Runner;

/// <summary>
/// <para>
/// Run benchmarks for any solutions that have inputs. It will run both for
/// both parts.
/// </para>
/// <para>
/// A lot of effort is made to display the output of each part. There's an
/// open issue at https://github.com/dotnet/BenchmarkDotNet/issues/784 which
/// notes the ask of recording a benchmark target's return value in a column
/// and how difficult it can be. As a workaround, the return value is recorded
/// once per run using <see cref="SolutionRecorder"/> to manage for a custom
/// column, <see cref="AnswerColumn"/>, to show in the summary table.
/// </para>
/// </summary>
[MemoryDiagnoser]
[Config(typeof(SolutionConfig))]
public class BenchmarkSolutionRunner
{
    [ParamsSource(nameof(SolutionsToRun))]
    public required BaseSolution Solution { get; set; }

    public IEnumerable<BaseSolution> SolutionsToRun { get; init; } = SolutionFinder.FindSolutionsWithInputs();

    [ParamsAllValues]
    public required RunMode PartToRun { get; set; }

    /// <summary>
    /// Should be set during the benchmark run for use during the cleanup phase.
    /// </summary>
    private object? _solutionAnswer;

    [GlobalCleanup]
    public void GlobalCleanup()
    {
        SolutionRecorder.SaveAnswer(Solution, PartToRun,
            _solutionAnswer ?? throw new InvalidOperationException("Answer is null"));
    }

    [Benchmark]
    public object Benchmark()
    {
        var output = Solution.Run(PartToRun);
        _solutionAnswer = output;
        return output;
    }
}
