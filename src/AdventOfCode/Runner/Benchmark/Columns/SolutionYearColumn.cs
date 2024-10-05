using AdventOfCode.Calendar;
using AdventOfCode.Common;
using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Reports;
using BenchmarkDotNet.Running;

namespace AdventOfCode.Runner.Benchmark.Columns;

public class SolutionYearColumn : IColumn
{
    public string GetValue(Summary summary, BenchmarkCase benchmarkCase, SummaryStyle style)
    {
        var solution = (BaseSolution)benchmarkCase.Parameters.Items.Single(x => x.Name == "Solution").Value;
        var puzzleInfo = solution.GetType().GetPuzzleInfoAttribute();
        return puzzleInfo?.Year.ToString() ?? "Unknown";
    }

    public string GetValue(Summary summary, BenchmarkCase benchmarkCase) => GetValue(summary, benchmarkCase, SummaryStyle.Default);
    public bool IsDefault(Summary summary, BenchmarkCase benchmarkCase) => false;
    public bool IsAvailable(Summary summary) => true;
    public string Id => nameof(SolutionYearColumn);
    public string ColumnName => "Year";
    public bool AlwaysShow => true;
    public ColumnCategory Category => ColumnCategory.Job;
    public int PriorityInCategory => 0;
    public bool IsNumeric => false;
    public UnitType UnitType => UnitType.Dimensionless;
    public string Legend => "The year";
}
