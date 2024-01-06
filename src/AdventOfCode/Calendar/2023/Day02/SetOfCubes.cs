using System.Reflection;

namespace AdventOfCode.Calendar._2023.Day02;

public sealed class SetOfCubes()
{
    public int Red { get; set; }
    public int Green { get; set; }
    public int Blue { get; set; }

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
