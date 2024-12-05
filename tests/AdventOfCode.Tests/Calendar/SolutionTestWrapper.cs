using AdventOfCode.Calendar;
using AdventOfCode.Common;
using NSubstitute;
using NSubstitute.Extensions;

namespace AdventOfCode.Tests.Calendar;

/// <summary>
/// Wrapper around a specific year-day Solution class that mocks the return for
/// the <see cref="BaseSolution.ReadInput"/> method.
/// </summary>
/// <typeparam name="TSolution">Any year-day Solution class.</typeparam>
public class SolutionTestWrapper<TSolution>
    where TSolution : BaseSolution
{
    private readonly TSolution _theSolution;

    public SolutionTestWrapper(string[] inputLines)
    {
        // Use real instance for all purposes except to mock the ReadInput
        // method.
        _theSolution = Substitute.ForPartsOf<TSolution>();
        _theSolution.Configure().ReadInput().Returns(inputLines);
    }

    public object Run(RunMode runMode) => _theSolution.Run(runMode);
}
