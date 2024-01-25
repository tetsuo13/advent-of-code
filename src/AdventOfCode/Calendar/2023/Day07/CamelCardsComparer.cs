using System.Collections.Immutable;

namespace AdventOfCode.Calendar._2023.Day07;

public class CamelCardsComparer : IComparer<Hand>
{
    private const char JokerCard = 'J';

    /// <summary>
    /// Whether to treat <c>J</c> cards as jokers -- wildcards that can act
    /// like whatever card would make the hand the strongest type possible.
    /// Otherwise <c>J</c> cards are jacks.
    /// </summary>
    private readonly bool _withJokerRule;

    /// <summary>
    /// Cached strengths of cards that were previously calculated. Avoids
    /// recalculating hands.
    /// </summary>
    private readonly Dictionary<string, int> _strengthCache = [];

    /// <summary>
    /// Map of each available label and its relative strength.
    /// </summary>
    private readonly IReadOnlyDictionary<char, int> _labelStrengthMap;

    public CamelCardsComparer(bool withJokerRule)
    {
        _withJokerRule = withJokerRule;
        _labelStrengthMap = new Dictionary<char, int>
        {
            { 'A', 14 },
            { 'K', 13 },
            { 'Q', 12 },
            // The key is either a Jack or a Joker.
            // Balance jokers by making them the weakest individually.
            { JokerCard, withJokerRule ? 1 : 11 },
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
    }

    public int Compare(Hand? x, Hand? y)
    {
        ArgumentNullException.ThrowIfNull(x);
        ArgumentNullException.ThrowIfNull(y);

        // Comparing two of the same hands. They're equal.
        if (x.Cards == y.Cards)
        {
            return 0;
        }

        var xStrength = Strength(x);
        var yStrength = Strength(y);

        if (xStrength > yStrength)
        {
            return 1;
        }
        else if (xStrength < yStrength)
        {
            return -1;
        }

        return CompareSameStrength(x, y);
    }

    /// <summary>
    /// Compare each card in hands to find first greater. Start at the
    /// left-most card of each hand and work to the right until one's greater.
    /// </summary>
    private int CompareSameStrength(Hand x, Hand y)
    {
        for (var i = 0; i < 5; i++)
        {
            var cardCompare = _labelStrengthMap[x.Cards[i]].CompareTo(_labelStrengthMap[y.Cards[i]]);

            // Cards are the same. Look at the next pair.
            if (cardCompare == 0)
            {
                continue;
            }

            return cardCompare;
        }

        // All five cards in both hands are identical.
        // Should never reach this point.
        throw new NotSupportedException($"Cards couldn't be compared ({x.Cards} vs {y.Cards})");
    }

    private int Strength(Hand hand)
    {
        if (_strengthCache.TryGetValue(hand.Cards, out int cachedStrength))
        {
            return cachedStrength;
        }

        var cardValues = Breakdown(hand);

        // There will always be five cards. List all possible combinations.
        // Return value itself isn't significant beyond being ordered
        // of strongest to weakest type, i.e. could be 7-1 or 42-36 ir doesn't
        // really matter.
        var strength = cardValues switch
        {
            [5] => 7, // Five of a kind
            [1, 4] => 6, // Four of a kind
            [2, 3] => 5, // Full house
            [1, 1, 3] => 4, // Three of a kind
            [1, 2, 2] => 3, // Two pair
            [1, 1, 1, 2] => 2, // One pair
            [1, 1, 1, 1, 1] => 1, // High card
            _ => throw new ArithmeticException($"Unrecognized hand strength ({hand.Cards})")
        };

        _strengthCache[hand.Cards] = strength;
        return strength;
    }

    private List<int> Breakdown(Hand hand)
    {
        var cards = hand.Cards.GroupBy(x => x)
            .OrderByDescending(x => x.Key)
            .ToImmutableDictionary(x => x.Key, x => x.Count());

        // Don't modify a hand of all Jokers.
        if (_withJokerRule &&
            cards.TryGetValue(JokerCard, out int numJokers) &&
            numJokers != 5)
        {
            // Increment the strongest label that's not a Joker by the amount
            // of Jokers in the hand then remove the Jokers.
            var strongestLabel = cards.Where(x => x.Key != JokerCard)
                .OrderByDescending(x => x.Value)
                .First()
                .Key;

            var copy = cards.ToDictionary();
            copy[strongestLabel] += copy[JokerCard];
            copy.Remove(JokerCard);
            return [.. copy.Values.Order()];
        }

        return [.. cards.Values.Order()];
    }
}
