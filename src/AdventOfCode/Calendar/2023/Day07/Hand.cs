using System.Collections.Immutable;

namespace AdventOfCode.Calendar._2023.Day07;

public class Hand : IComparable<Hand>
{
    /// <summary>
    /// The cards in the hand in the original order.
    /// </summary>
    public string Cards { get; private set; }

    public int Bid { get; private set; }

    /// <summary>
    /// Breakdown of number of occurrences of each card in the hand.
    /// </summary>
    private readonly IReadOnlyDictionary<char, int> _cards;

    /// <summary>
    /// Map of each available label and its relative strength.
    /// </summary>
    private readonly IReadOnlyDictionary<char, int> _labelStrengthMap = new Dictionary<char, int>
    {
        { 'A', 14 },
        { 'K', 13 },
        { 'Q', 12 },
        { 'J', 11 },
        { 'T', 10 },
        { '9', 9 },
        { '8', 8 },
        { '7', 7 },
        { '6', 6 },
        { '5', 5 },
        { '4', 4 },
        { '3', 3 },
        { '2', 2 }
    };

    public Hand(string cards, int bid)
    {
        Bid = bid;
        Cards = cards;
        _cards = cards.GroupBy(x => x)
            .OrderByDescending(x => x.Key)
            .ToImmutableDictionary(x => x.Key, x => x.Count());
    }

    public int CompareTo(Hand? other)
    {
        ArgumentNullException.ThrowIfNull(other);

        // Comparing two of the same hands. They're equal.
        if (Cards == other.Cards)
        {
            return 0;
        }

        var instanceType = Type();
        var otherType = other.Type();

        if (instanceType > otherType)
        {
            return 1;
        }
        else if (instanceType < otherType)
        {
            return -1;
        }

        // Two hands are of same type.

        // Compare each card in hands to find first greater. Start at the
        // left-most card of each hand and work to the right until one's
        // greater.
        for (var i = 0; i < 5; i++)
        {
            var cardCompare = _labelStrengthMap[Cards[i]].CompareTo(_labelStrengthMap[other.Cards[i]]);

            // Cards are the same. Look at the next pair.
            if (cardCompare == 0)
            {
                continue;
            }

            return cardCompare;
        }

        // All five cards in both hands are identical.
        // Should never reach this point.
        throw new NotSupportedException($"Cards couldn't be compared ({Cards} vs {other.Cards})");
    }

    public int Type()
    {
        // There will always be five cards. List all possible combinations.
        // Return value itself isn't significant beyond being ordered
        // of strongest to weakest type.
        return _cards.Values.Order().ToList() switch
        {
            [5] => 7, // Five of a kind
            [1, 4] => 6, // Four of a kind
            [2, 3] => 5, // Full house
            [1, 1, 3] => 4, // Three of a kind
            [1, 2, 2] => 3, // Two pair
            [1, 1, 1, 2] => 2, // One pair
            [1, 1, 1, 1, 1] => 1, // High card
            _ => throw new ArithmeticException($"Unrecognized hand type ({Cards})")
        };
    }
}
