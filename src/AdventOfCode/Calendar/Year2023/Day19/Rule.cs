namespace AdventOfCode.Calendar.Year2023.Day19;

public readonly record struct Rule
{
    public char Category { get; }
    public char Op { get; }
    public int Rating { get; }
    public string Destination { get; }

    public Rule(char category, char op, int rating, string destination)
    {
        Category = category;
        Op = op;
        Rating = rating;
        Destination = destination;
    }

    public Rule(string destination)
    {
        Destination = destination;
    }
}
