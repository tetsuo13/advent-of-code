using BenchmarkDotNet.Columns;
using BenchmarkDotNet.Configs;

namespace AdventOfCode.Runner.Benchmark.Configs;

public class SolutionConfig : ManualConfig
{
    public SolutionConfig()
    {
        // AddColumn(new SolutionYearColumn(), new SolutionDayColumn());
        // HideColumns(Column.Method, Column.Error, Column.StdDev);
        HideColumns(Column.Error, Column.StdDev);

        // Normally truncates at 20 characters but want to output the
        // yyyy-MM-dd along with the solution name.
        SummaryStyle = DefaultConfig.Instance.SummaryStyle.WithMaxParameterColumnWidth(40);
    }
}
