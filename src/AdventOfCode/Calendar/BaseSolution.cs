using System.Reflection;

namespace AdventOfCode.Calendar;

public abstract class BaseSolution
{
    private const string InputFilename = "input.txt";

    public abstract Task<int> Run(RunMode runMode);

    /// <summary>
    /// <para>
    /// Input file will reside in a subdirectory of the executable. That
    /// path will follow the same as the namespace mostly.
    /// </para>
    /// <para>
    /// Using <see langword="virtual"/> keyword so unit tests can replace
    /// the method to mock the return.
    /// </para>
    /// </summary>
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
