using System.Reflection;

namespace AdventOfCode.Calendar.Year2023.Day02;

public record SetOfCubes
{
    public int Red { get; init; }
    public int Green { get; init; }
    public int Blue { get; init; }

#pragma warning disable S1144 // Unused private types or members should be removed
    public int this[string colorName]
#pragma warning restore S1144 // Unused private types or members should be removed
    {
        set
        {
            GetType()
                .GetProperty(colorName, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public)
                !.SetValue(this, value, null);
        }
    }
}
