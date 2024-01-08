using AdventOfCode.Calendar;
using NSubstitute;
using NSubstitute.Extensions;

namespace AdventOfCode.Tests.Calendar;

/// <summary>
/// Wrapper around a specific year-day Solution class that mcks the return for
/// the <see cref="BaseSolution.ReadInput"/> method.
/// </summary>
/// <typeparam name="T">Any year-day Solution class.</typeparam>
public class SolutionTestWrapper<T>
    where T : BaseSolution
{
    private readonly T _theSolution;

    public SolutionTestWrapper(string[] inputLines)
    {
        // Use real instance for all purposes except to mock the ReadInput
        // method.
        _theSolution = Substitute.ForPartsOf<T>();
        _theSolution.Configure().ReadInput().Returns(Task.FromResult(inputLines));
    }

    public async Task<int> Run(RunMode runMode) =>
        await _theSolution.Run(runMode);
}
