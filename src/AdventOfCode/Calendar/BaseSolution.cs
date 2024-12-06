using System.Reflection;
using AdventOfCode.Common;
using AdventOfCode.Runner.Utils;

namespace AdventOfCode.Calendar;

public abstract class BaseSolution
{
    /// <summary>
    /// Returns the date and puzzle name of the solution.
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
        var puzzleInfo = GetType().GetPuzzleInfoAttribute();

        return puzzleInfo is null
            ? $"Missing {nameof(PuzzleInfoAttribute)} on class"
            : $"{puzzleInfo.Year}-12-{puzzleInfo.Day:D2}: {puzzleInfo.Name}";
    }

    // Returning an object as not all solutions fit in an int. Rather than
    // use long everywhere, this returns an object since the only place the
    // return value is used is to write it to the console. There's an argument
    // to be made for simply returning a string.
    // TODO: Just return a string. We don't like object.
    public abstract object Run(RunMode runMode);

    /// <summary>
    /// Input file will reside in a subdirectory of the executable. That
    /// path will follow the same as the namespace mostly.
    /// </summary>
    /// <remarks>
    /// Using <see langword="virtual"/> keyword so unit tests can mock the
    /// method without having to resort to reading files.
    /// </remarks>
    public virtual string[] ReadInput()
    {
        var solutionDirectory = GetType().Namespace!
            .Replace(nameof(AdventOfCode), string.Empty)
            .Replace(Type.Delimiter, Path.DirectorySeparatorChar);

        // Remove leading separator left behind from first substitution
        solutionDirectory = solutionDirectory[1..];

        var inputFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            solutionDirectory, SolutionFinder.InputFileName);

        if (!File.Exists(inputFile))
        {
            throw new FileNotFoundException($"Input file not found in {inputFile}");
        }

        // TODO: This is probably not the most efficient way of reading all lines from a file...
        return File.ReadAllLines(inputFile);
    }
}
