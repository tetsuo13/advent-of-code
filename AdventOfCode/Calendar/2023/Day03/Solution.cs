using System.Text.RegularExpressions;

namespace AdventOfCode.Calendar._2023.Day03;

/// <summary>
/// Brute force approach. For each number found on a line, draw a bounding box
/// around it to look for a character other than period.
/// 
/// Plenty of room for improvement. Should use Span for character manipulation.
/// There's likely some 2D array operation libraries that should be used rather
/// than implementing algorithms from scratch [poorly] -- this really showed
/// for part 2.
/// </summary>
public partial class Solution : BaseSolution
{
    private const string GearSymbol = "*";
    private string[] lines = [];

    public override async Task<int> Run(RunMode runMode)
    {
        return runMode switch
        {
            RunMode.PartOne => await SumPartNumbers(),
            RunMode.PartTwo => await SumGearRatios(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private async Task<int> SumPartNumbers()
    {
        lines = await ReadInput();
        var sum = 0;

        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            // Draw a bounding box around every number in the row to look
            // for a symbol.
            foreach (var match in NumberRegex().Matches(lines[lineNumber]).Cast<Match>())
            {
                var boundingBox = GetBoundingBox(lineNumber, match.Index, match.Value);

                // Flatten the bounding box. The number is a "part number"
                // if there's more than just a period character around it.
                if (string.Join(string.Empty, boundingBox).Distinct().Count() > 1)
                {
                    sum += Convert.ToInt32(match.Value);
                }
            }
        }

        return sum;
    }

    private async Task<int> SumGearRatios()
    {
        lines = await ReadInput();
        var sum = 0;

        for (var lineNumber = 0; lineNumber < lines.Length; lineNumber++)
        {
            // Draw a bounding box around every gear symbol to look for
            // exactly two numbers around it.

            var gearIndex = 0;

            while ((gearIndex = lines[lineNumber].IndexOf(GearSymbol, gearIndex)) != -1)
            {
                var boundingBox = GetBoundingBox(lineNumber, gearIndex, lines[lineNumber].Substring(gearIndex, 1));

                Console.WriteLine($"{gearIndex} {string.Join("|", boundingBox)}");

                Console.WriteLine(boundingBox.Count(x => x.Any(char.IsDigit)));

                //Console.WriteLine(boundingBox.Select(char.IsDigit).Count());

                // Must have exactly two elements with numbers in it
                if (boundingBox.Count(x => x.Any(char.IsDigit)) == 2)
                {
                    //answer +=
                }

                gearIndex++;
            }
        }

        return sum;
    }

    /// <summary>
    /// Capture all of the characters around a substring on the line by
    /// drawing a bounding box.
    /// </summary>
    private List<string> GetBoundingBox(int currentLine, int matchIndex, string number)
    {
        var boundingBox = new List<string>();
        var isFlushLeft = matchIndex == 0;

        // Add left
        if (!isFlushLeft)
        {
            boundingBox.Add(lines[currentLine][matchIndex - 1].ToString());
        }

        // Add right
        if (matchIndex + number.Length < lines[currentLine].Length)
        {
            boundingBox.Add(lines[currentLine][matchIndex + number.Length].ToString());
        }

        var startingPos = isFlushLeft ? 0 : matchIndex - 1;
        int endPadding;

        if (isFlushLeft)
        {
            endPadding = startingPos + number.Length + 1 > lines[currentLine].Length ? 0 : 1;
        }
        else
        {
            endPadding = startingPos + number.Length + 2 > lines[currentLine].Length ? 1 : 2;
        }

        // Add top
        if (currentLine > 0)
        {
            boundingBox.Add(lines[currentLine - 1]
                .Substring(startingPos, number.Length + endPadding));
        }

        // Add bottom
        if (currentLine < lines.Length - 1)
        {
            boundingBox.Add(lines[currentLine + 1]
                .Substring(startingPos, number.Length + endPadding));
        }

        return boundingBox;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();
}
