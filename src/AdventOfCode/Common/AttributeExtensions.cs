namespace AdventOfCode.Common;

public static class AttributeExtensions
{
    public static PuzzleInfoAttribute? GetPuzzleInfoAttribute(this Type type) =>
        type.GetCustomAttributes(typeof(PuzzleInfoAttribute), false)
            .SingleOrDefault() as PuzzleInfoAttribute;
}
