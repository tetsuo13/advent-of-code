namespace AdventOfCode.Calendar._2023.Day19;

public readonly record struct Rule
{
    public char Category { get; init; }
    public char Op { get; init; }
    public int Rating { get; init; }
    public string Destination { get; init; }

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
