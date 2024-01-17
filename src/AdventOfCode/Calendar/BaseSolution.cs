using System.Reflection;

namespace AdventOfCode.Calendar;

public abstract class BaseSolution
{
    private const string InputFilename = "input.txt";

    // Returning an object as not all solutions fit in an int. Rather than
    // use long everywhere, this returns an object since the only place the
    // return value is used is to write it to the console. There's an argument
    // to be made for simply returning a string.
    public abstract Task<object> Run(RunMode runMode);

    /// <summary>
    /// Input file will reside in a subdirectory of the executable. That
    /// path will follow the same as the namespace mostly.
    /// </summary>
    /// <remarks>
    /// Using <see langword="virtual"/> keyword so unit tests can mock the
    /// method without having to resort to reading files.
    /// </remarks>
    public virtual async Task<string[]> ReadInput()
    {
        var solutionDirectory = GetType().Namespace!
            .Replace(nameof(AdventOfCode), string.Empty)
            // Years will have leading underscore
            .Replace("_", string.Empty)
            .Replace(Type.Delimiter, Path.DirectorySeparatorChar);

        // Remove leading separator left behind from first substitution
        solutionDirectory = solutionDirectory[1..];

        var inputFile = Path.Combine(Path.GetDirectoryName(Assembly.GetEntryAssembly()!.Location)!,
            solutionDirectory, InputFilename);

        if (!File.Exists(inputFile))
        {
            throw new FileNotFoundException($"Input file not found in {inputFile}");
        }

        return await File.ReadAllLinesAsync(inputFile);
    }
}
