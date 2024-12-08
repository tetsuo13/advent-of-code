using AdventOfCode.Calendar.Year2024.Day03;
using AdventOfCode.Common;
using Xunit;

namespace AdventOfCode.Tests.Calendar.Year2024;

public class Day03Tests
{
    [Fact]
    public void PartOne()
    {
        string[] input = ["xmul(2,4)%&mul[3,7]!@^do_not_mul(5,5)+mul(32,64]then(mul(11,8)mul(8,5))"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(161, solution.Run(RunMode.PartOne));
    }

    [Fact]
    public void PartTwo()
    {
        string[] input = ["xmul(2,4)&mul[3,7]!^don't()_mul(5,5)+mul(32,64](mul(11,8)undo()?mul(8,5))"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(48, solution.Run(RunMode.PartTwo));
    }

    [Fact]
    public void PartTwo_DontInstructionWithoutAnyFollowingDoInstructions()
    {
        string[] input = ["'{how()when()<@,}mul(512,183),']why()(who()&<when()~don't()who()~)-why(899,149)how()mul(931,445)how(852,127)"];
        var solution = new SolutionTestWrapper<Solution>(input);
        Assert.Equal(93_696, solution.Run(RunMode.PartTwo));
    }
}
