namespace AdventOfCode.Common;

[AttributeUsage(AttributeTargets.Class)]
internal class PuzzleInfoAttribute(int year, int day, string name) : Attribute
{
    public int Year { get; } = year;
    public int Day { get; } = day;
    public string Name { get; } = name;
}
