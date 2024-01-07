using System.ComponentModel.DataAnnotations;
using AdventOfCode.Calendar;
using Cocona;

namespace AdventOfCode.Runner;

public class SolutionRunner
{
    private enum ErrorExitCode
    {
        None,
        YearDayNotFound,
        ErrorInstantiatingSolution
    }

    public async Task<int> Run([Option(Description = "Four digit year")] int year,
        [Option(Description = "Day in month of December")][Range(1, 31)] int day)
    {
        var solutionType = TryFindSolutionType(year, day) ?? throw new CommandExitedException(
            $"Error: no solution by that year/day combo, check README.md for available solutions.",
            (int)ErrorExitCode.YearDayNotFound);

        var solution = Activator.CreateInstance(solutionType) as BaseSolution ?? throw new CommandExitedException(
            "Error: unexpected error instantiating solution",
            (int)ErrorExitCode.ErrorInstantiatingSolution);

        Console.WriteLine("Part {0}: {1}", (int)RunMode.PartOne, await solution.Run(RunMode.PartOne));
        Console.WriteLine("Part {0}: {1}", (int)RunMode.PartTwo, await solution.Run(RunMode.PartTwo));

        return (int)ErrorExitCode.None;
    }

    /// <summary>
    /// Find the full <see cref="Type"/> for the requested
    /// <paramref name="year"/> and <paramref name="day"/> solution. Every
    /// valid combination has a class named "Solution" under a unique
    /// namespace. This method derives that full namespace.
    /// </summary>
    /// <param name="year">The four digit year.</param>
    /// <param name="day">The day in the month of December.</param>
    /// <returns>
    /// The Type matching the requested year and day or <see langword="null"/>
    /// if there isn't one available.
    /// </returns>
    internal Type? TryFindSolutionType(int year, int day)
    {
        var ns = new List<string>
        {
            nameof(AdventOfCode),
            nameof(Calendar),
            $"_{year}",
            $"Day{day:D2}",

            // A direct reference to a solution, any year/month will do.
            nameof(Calendar._2023.Day01.Solution)
        };

        var className = string.Join(".", ns);
        return Type.GetType(className);
    }
}
