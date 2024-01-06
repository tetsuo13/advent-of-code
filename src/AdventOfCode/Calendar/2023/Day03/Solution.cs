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
    private const char PeriodSymbol = '.';
    private const char GearSymbol = '*';

    private string[] _lines = [];

    public override async Task<int> Run(RunMode runMode)
    {
        _lines = await ReadInput();

        return runMode switch
        {
            RunMode.PartOne => SumPartNumbers(),
            RunMode.PartTwo => SumGearRatios(),
            _ => throw new ArgumentOutOfRangeException(nameof(runMode))
        };
    }

    private int SumPartNumbers()
    {
        var sum = 0;

        for (var lineNumber = 0; lineNumber < _lines.Length; lineNumber++)
        {
            // Draw a bounding box around every number in the row to look
            // for a symbol.
            foreach (var match in NumberRegex().Matches(_lines[lineNumber]).Cast<Match>())
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

    private int SumGearRatios()
    {
        var sum = 0;

        for (var lineNumber = 0; lineNumber < _lines.Length; lineNumber++)
        {
            // Keep track of adjacent part numbers on the line to avoid
            // duplicates.
            var seen = new List<KeyValuePair<string, string>>();

            foreach (var match in NumberRegex().Matches(_lines[lineNumber]).Cast<Match>())
            {
                var boundingBox = GetBoundingBox(lineNumber, match.Index, match.Value);

                // Gear to the left, first character to the left of that isn't
                // a period, and this number wasn't previously seen.
                if (boundingBox[(int)Direction.Left].Contains(GearSymbol) &&
                    _lines[lineNumber][match.Index - 2] != PeriodSymbol &&
                    !seen.Exists(x => x.Value == match.Value))
                {
                    var rl = GearRatioRightLeftRegex();
                    var rlMatches = rl.Match(_lines[lineNumber], match.Index);

                    if (rlMatches.Success)
                    {
                        sum += int.Parse(match.Value) * int.Parse(rlMatches.Value);
                        seen.Add(new(match.Value, rlMatches.Value));
                    }
                }

                // Gear to the right and the first character next to that
                // isn't a period.
                if (boundingBox[(int)Direction.Right].Contains(GearSymbol) &&
                    _lines[lineNumber][match.Index + match.Value.Length + 1] != PeriodSymbol)
                {
                    var lr = GearRatioLeftRightRegex();
                    var rlMatches = lr.Match(_lines[lineNumber], match.Index + match.Value.Length);

                    if (rlMatches.Success)
                    {
                        sum += int.Parse(match.Value) * int.Parse(rlMatches.Value);
                        seen.Add(new(match.Value, rlMatches.Value));
                    }
                }

                var gearPosition = boundingBox[(int)Direction.Bottom].IndexOf(GearSymbol);

                if (gearPosition != -1 &&
                    lineNumber + 2 < _lines.Length &&
                    match.Index + gearPosition < _lines[lineNumber + 2].Length &&
                    _lines[lineNumber + 2][match.Index + gearPosition] != PeriodSymbol)
                {
                    // Starting at the position of the gear, find all numbers
                    // to the right and left of it then concatenate them to
                    // get the part number.

                    var lr = GearRatioLeftRightRegex();
                    var rl = GearRatioRightLeftRegex();
                    var lrMatches = lr.Match(_lines[lineNumber + 2], match.Index + gearPosition);
                    var rlMatches = rl.Match(_lines[lineNumber + 2], match.Index + gearPosition);

                    var partNumber = (rlMatches.Success ? rlMatches.Value : string.Empty)
                        + (lrMatches.Success ? lrMatches.Value : string.Empty);

                    // Found a gear
                    if (partNumber != string.Empty)
                    {
                        var gearRatio = int.Parse(match.Value) * int.Parse(partNumber);
                        sum += gearRatio;
                    }
                }
            }
        }

        return sum;
    }

    enum Direction
    {
        Left = 0,
        Right,
        Top,
        Bottom
    }

    /// <summary>
    /// Capture all of the characters around a substring on the line by
    /// drawing a bounding box.
    /// </summary>
    /// <returns>
    /// Collection of characters around the target number with elements: left
    /// (one character), right (one character), top, and bottom. It's possible
    /// for up to two of the elements to be <see cref="string.Empty"/> in cases
    /// where the number is adjacent to an edge or in a corner.
    /// </returns>
    private string[] GetBoundingBox(int currentLine, int matchIndex, string number)
    {
        var boundingBox = Enumerable.Repeat(string.Empty, 4).ToArray();
        var isFlushLeft = matchIndex == 0;

        // Add left
        if (!isFlushLeft)
        {
            boundingBox[(int)Direction.Left] = _lines[currentLine][matchIndex - 1].ToString();
        }

        // Add right
        if (matchIndex + number.Length < _lines[currentLine].Length)
        {
            boundingBox[(int)Direction.Right] = _lines[currentLine][matchIndex + number.Length].ToString();
        }

        var startingPos = isFlushLeft ? 0 : matchIndex - 1;
        int endPadding;

        if (isFlushLeft)
        {
            endPadding = startingPos + number.Length + 1 > _lines[currentLine].Length ? 0 : 1;
        }
        else
        {
            endPadding = startingPos + number.Length + 2 > _lines[currentLine].Length ? 1 : 2;
        }

        // Add top
        if (currentLine > 0)
        {
            boundingBox[(int)Direction.Top] = _lines[currentLine - 1]
                .Substring(startingPos, number.Length + endPadding);
        }

        // Add bottom
        if (currentLine < _lines.Length - 1)
        {
            boundingBox[(int)Direction.Bottom] = _lines[currentLine + 1]
                .Substring(startingPos, number.Length + endPadding);
        }

        return boundingBox;
    }

    [GeneratedRegex(@"\d+")]
    private static partial Regex NumberRegex();

    [GeneratedRegex(@"\d+")]
    private static partial Regex GearRatioLeftRightRegex();

    [GeneratedRegex(@"\d+", RegexOptions.RightToLeft)]
    private static partial Regex GearRatioRightLeftRegex();
}
