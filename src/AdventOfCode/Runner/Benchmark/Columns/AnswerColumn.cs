using AdventOfCode.Calendar;
using AdventOfCode.Common;
using AdventOfCode.Runner.Benchmark.Utils;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Runner.Benchmark.Columns;

/// <summary>
/// Uses <see cref="SolutionRecorder"/> for retrieving the saved answer during
/// the benchmarking process.
/// </summary>
public class AnswerColumn : IColumn
{
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
    {
        var solution = (BaseSolution)benchmarkCase.Parameters.Items
            .Single(x => x.Name == nameof(BenchmarkSolutionRunner.Solution)).Value;

        var runMode = (RunMode)benchmarkCase.Parameters.Items
            .Single(x => x.Name == nameof(BenchmarkSolutionRunner.PartToRun)).Value;

        return SolutionRecorder.ReadAnswer(solution, runMode);
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);
    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    public bool IsAvailable(Summary summary) => true;
    public string Id => nameof(AnswerColumn);
    public string ColumnName => "Answer";
    public bool AlwaysShow => true;
    public ColumnCategory Category => ColumnCategory.Params;
    public int PriorityInCategory => 0; // Show after solution name and part columns
    public bool IsNumeric => false;
    public UnitType UnitType => UnitType.Dimensionless;
    public string Legend => "The answer to the solution and part";
}
