using System.Reflection;
using AdventOfCode.Calendar;
using AdventOfCode.Calendar.Year2023.Day01;
using AdventOfCode.Common;

namespace AdventOfCode.Runner.Utils;

public static class SolutionFinder
{
    public const string InputFileName = "input.txt";

    public static List<BaseSolution> FindSolutionsWithInputs()
    {
        var solutions = new List<BaseSolution>();
        // TODO: Not current directory but assembly directory.
        var inputFiles = Directory.GetFiles(Directory.GetCurrentDirectory(), InputFileName, SearchOption.AllDirectories)
            .OrderBy(x => x);

        foreach (var input in inputFiles)
        {
            var parts = input.Split(Path.DirectorySeparatorChar);
            var year = parts[^3];
            var day = parts[^2];
            var solutionType = TryFindSolutionType(year, day);

            if (solutionType is null)
            {
                Console.WriteLine("Error: no solution found in same directory for {0}", input);
                throw new Exception();
                //return (int)ErrorExitCode.YearDayNotFound;
            }

            if (Activator.CreateInstance(solutionType) is not BaseSolution solution)
            {
                Console.WriteLine("Error: unexpected error instantiating solution for {0} {1}", year, day);
                throw new Exception();
                //return (int)ErrorExitCode.ErrorInstantiatingSolution;
            }

            // TODO: Validate each solution is unique for the year and day.
            // Ensure each solution is marked.
            var puzzleInfo = solution.GetType()
                .GetCustomAttributes(typeof(PuzzleInfoAttribute))
                .SingleOrDefault();

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

            // A direct reference to a solution, any year/month will do.
            nameof(Solution)
        };

        var className = string.Join(".", ns);
        return Type.GetType(className);
    }
}
