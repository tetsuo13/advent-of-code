namespace AdventOfCode.Calendar;

public abstract class BaseSolution
{
    public virtual async Task<string[]> ReadInput() =>
        await File.ReadAllLinesAsync("input.txt");
}
