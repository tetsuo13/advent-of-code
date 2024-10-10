using System.Reflection;
using AdventOfCode.Calendar;
using AdventOfCode.Calendar.Year2023.Day01;
using AdventOfCode.Common;

namespace AdventOfCode.Runner.Utils;

public static class SolutionFinder
{
    public const string InputFileName = "input.txt";

    /// <summary>
    /// Gets the absolute path to the assembly file. This may not always be
    /// current directory. When running benchmarks, this will be an entirely
    /// new subdirectory from the assembly file as it will generate new
    /// projects.
    /// </summary>
    private static string CurrentPath => Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) ??
                                         throw new InvalidOperationException();

    /// <summary>
    /// Given the starting directory of the assembly, recursively search for
    /// input files (<see cref="InputFileName"/>) and return a collection of
    /// <see cref="BaseSolution"/> classes in the same directory as those
    /// input files.
    /// </summary>
    /// <returns></returns>
    /// <exception cref="Exception"></exception>
    public static List<BaseSolution> FindSolutionsWithInputs()
    {
        var solutions = new List<BaseSolution>();
        var inputFiles = Directory.GetFiles(CurrentPath, InputFileName, SearchOption.AllDirectories)
            .OrderBy(x => x);

        foreach (var input in inputFiles)
        {
            var parts = input.Split(Path.DirectorySeparatorChar);
            var year = parts[^3];
            var day = parts[^2];
            var solutionType = TryFindSolutionType(year, day);

            if (solutionType is null)
            {
                throw new Exception($"Error: no solution found in same directory for {input}");
            }

            if (Activator.CreateInstance(solutionType) is not BaseSolution solution)
            {
                throw new Exception($"Error: unexpected error instantiating solution for {year} {day}");
            }

            // TODO: Validate each solution is unique for the year and day.
            // Ensure each solution is marked.
            var puzzleInfo = solution.GetType().GetPuzzleInfoAttribute();

            if (puzzleInfo is null)
            {
                Console.WriteLine("Error: missing {0} attribute on solution for {1} {2}",
                    nameof(PuzzleInfoAttribute), year, day);
                continue;
            }

            solutions.Add(solution);
        }

        return solutions;
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
    internal static Type? TryFindSolutionType(string year, string day)
    {
        var ns = new List<string>
        {
            nameof(AdventOfCode),
            nameof(Calendar),
            year,
            day,

            // A direct reference to a solution, any year and day will do.
            // We're looking for a class named `Solution` specifically, so
            // `BaseSolution` won't work here.
            nameof(Solution)
        };

        var className = string.Join(".", ns);
        return Type.GetType(className);
    }
}
