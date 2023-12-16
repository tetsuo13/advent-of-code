namespace AdventOfCode.Calendar;

public abstract class BaseSolution
{
    public virtual async Task<string[]> ReadInput()
    {
        return await File.ReadAllLinesAsync("input.txt");
    }
}
