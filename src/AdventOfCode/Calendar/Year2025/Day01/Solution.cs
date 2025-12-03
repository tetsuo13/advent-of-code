using AdventOfCode.Common;

namespace AdventOfCode.Calendar.Year2025.Day01;

[PuzzleInfo(2025, 1, "Secret Entrance")]
public class Solution : BaseSolution
{
    public override object Run(RunMode runMode)
    {
        var rotations = ReadInput();

        return runMode switch
        {
            RunMode.PartOne => DialPointedToZero(rotations, false),
            RunMode.PartTwo => DialPointedToZero(rotations, true),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    /// <summary>
    /// </summary>
    /// <param name="rotations">
    /// The collection of rotations. Each element denotes the direction and
    /// distance of a single rotation. For example, "L68" means rotate left 68
    /// clicks.
    /// </param>
    /// <param name="utilizePasswordMethod0x434C49434B">
    /// When <see langword="true"/>, count every time the dial touches zero.
    /// For example, if the dial is at 50, a rotation of L68 will end up
    /// pointing to 82. During the rotation it passed through zero once. So
    /// it's counted once.
    /// <para/>
    /// When <see langword="false"/>, count every time the dial points to 0
    /// after a rotation. Using the same example as before, it's not counted.
    /// </param>
    private static int DialPointedToZero(string[] rotations, bool utilizePasswordMethod0x434C49434B)
    {
        var numZeroes = 0;
        var dial = 50;

        foreach (var rotation in rotations)
        {
            var previous = dial;
            var direction = rotation[0] switch
            {
                'L' => -1,
                'R' => 1,
                _ => throw new ArgumentOutOfRangeException($"Encountered unknown direction: {rotation[0]}")
            };

            dial += direction * int.Parse(rotation[1..]);

            if (utilizePasswordMethod0x434C49434B)
            {
                // After rotation. Check if the dial landed on zero or passed
                // through zero as indicated by a sign change of the dial: if
                // it was previously positive and now negative, or vice versa.
                if (dial == 0 || previous * dial < 0)
                {
                    numZeroes++;
                }

                // Treat 100 as zero
                numZeroes += Math.Abs(dial / 100);
                dial %= 100;
            }
            else
            {
                dial %= 100;

                if (dial == 0)
                {
                    numZeroes++;
                }
            }
        }

        return numZeroes;
    }
}
