namespace PokerHands;
public class Hand
{
    public ICollection<Card> Cards { get; init; } = new List<Card>(5);
    public HandType HandType => EvaluateHandType();

    private HandType EvaluateHandType()
    {
        var cards = Cards.ToList();
        var isFlush = cards.All(c => c.Color == cards[0].Color);
        var orderedRanks = cards.Select(c => (int)c.Rank).OrderBy(x => x).ToList();
        var isStraight = orderedRanks.Zip(orderedRanks.Skip(1), (a, b) => b - a).All(x => x == 1)
            || (orderedRanks.SequenceEqual(new List<int> { 0, 1, 2, 3, 12 })); // Ace-low straight
        var groups = cards.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ToList();
        var counts = groups.Select(g => g.Count()).ToList();

        if (isFlush && isStraight) return HandType.StraightFlush;
        if (counts[0] == 4) return HandType.FourOfAKind;
        if (counts[0] == 3 && counts[1] == 2) return HandType.FullHouse;
        if (isFlush) return HandType.Flush;
        if (isStraight) return HandType.Straight;
        if (counts[0] == 3) return HandType.ThreeOfAKind;
        if (counts[0] == 2 && counts[1] == 2) return HandType.TwoPair;
        if (counts[0] == 2) return HandType.OnePair;
        return HandType.HighCard;
    }

    public override string ToString()
    {
        var cards = Cards.ToList();
        var groups = cards.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
        var flushColor = cards[0].Color;
        var orderedRanks = cards.Select(c => (int)c.Rank).OrderBy(x => x).ToList();
        var isFlush = cards.All(c => c.Color == flushColor);
        var isStraight = orderedRanks.Zip(orderedRanks.Skip(1), (a, b) => b - a).All(x => x == 1)
            || (orderedRanks.SequenceEqual(new List<int> { 0, 1, 2, 3, 12 }));

        switch (HandType)
        {
            case HandType.HighCard:
                return $"High Card {groups[0].Key}";
            case HandType.OnePair:
                return $"One Pair {groups[0].Key}";
            case HandType.TwoPair:
                return $"Two Pair {groups[0].Key} and {groups[1].Key}";
            case HandType.ThreeOfAKind:
                return $"Three Of A Kind {groups[0].Key}";
            case HandType.Straight:
                if (orderedRanks.SequenceEqual(new List<int> { 0, 1, 2, 3, 12 }))
                    return $"Straight Five high";
                return $"Straight {groups[0].Key} high";
            case HandType.Flush:
                return $"Flush {flushColor}";
            case HandType.FullHouse:
                return $"Full House {groups[0].Key} over {groups[1].Key}";
            case HandType.FourOfAKind:
                return $"Four Of A Kind {groups[0].Key}";
            case HandType.StraightFlush:
                if (orderedRanks.SequenceEqual(new List<int> { 0, 1, 2, 3, 12 }))
                    return $"Straight Flush {flushColor} Five high";
                return $"Straight Flush {flushColor} {groups[0].Key} high";
            default:
                return string.Empty;
        }
    }
}