using AdventOfCode.Calendar;
using AdventOfCode.Common;

namespace AdventOfCode.Runner.Benchmark.Utils;

/// <summary>
/// Intermediary service used between running benchmarks and retrieving
/// solution answers for summary table display. Will use files in a temp
/// directory. Environment variables would be cleaner but benchmarks are
/// performed in separate processes so they would be lost between them.
/// </summary>
public static class SolutionRecorder
{
    private static readonly char[] _replaceChars = [..Path.GetInvalidFileNameChars(), ..new[] { ' ', ':' }];

    public static void SaveAnswer(BaseSolution solution, RunMode runMode, object answer) =>
        File.WriteAllText(FilePath(solution, runMode), answer.ToString());

    public static string ReadAnswer(BaseSolution solution, RunMode runMode) =>
        File.ReadAllText(FilePath(solution, runMode));

    private static string FilePath(BaseSolution solution, RunMode runMode)
    {
        var filename = $"{solution}_{runMode}_output.txt";
        var safeFilename = string.Join("_", filename.Split(_replaceChars));
        return Path.Combine(Path.GetTempPath(), $"{nameof(AdventOfCode)}_{safeFilename}");
    }
}
