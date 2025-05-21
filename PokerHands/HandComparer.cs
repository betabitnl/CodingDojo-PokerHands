namespace PokerHands;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x == null || y == null) return 0;
        if (x.HandType != y.HandType)
        {
            return x.HandType > y.HandType ? 1 : -1;
        }

        var xGroups = x.Cards.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
        var yGroups = y.Cards.GroupBy(c => c.Rank).OrderByDescending(g => g.Count()).ThenByDescending(g => g.Key).ToList();
        var xRanks = x.Cards.Select(c => (int)c.Rank).OrderByDescending(r => r).ToList();
        var yRanks = y.Cards.Select(c => (int)c.Rank).OrderByDescending(r => r).ToList();
        bool xIsAceLowStraight = xRanks.SequenceEqual(new List<int> { 12, 3, 2, 1, 0 }) || xRanks.SequenceEqual(new List<int> { 3, 2, 1, 0, 12 });
        bool yIsAceLowStraight = yRanks.SequenceEqual(new List<int> { 12, 3, 2, 1, 0 }) || yRanks.SequenceEqual(new List<int> { 3, 2, 1, 0, 12 });

        switch (x.HandType)
        {
            case HandType.StraightFlush:
            case HandType.Straight:
                if (xIsAceLowStraight && !yIsAceLowStraight) return 1;
                if (!xIsAceLowStraight && yIsAceLowStraight) return -1;
                return xRanks.Max().CompareTo(yRanks.Max());
            case HandType.FourOfAKind:
            case HandType.FullHouse:
            case HandType.ThreeOfAKind:
                for (int i = 0; i < xGroups.Count; i++)
                {
                    int cmp = xGroups[i].Key.CompareTo(yGroups[i].Key);
                    if (cmp != 0) return cmp;
                }
                break;
            case HandType.TwoPair:
                for (int i = 0; i < 2; i++)
                {
                    int cmp = xGroups[i].Key.CompareTo(yGroups[i].Key);
                    if (cmp != 0) return cmp;
                }
                // Compare kicker
                return xGroups[2].Key.CompareTo(yGroups[2].Key);
            case HandType.OnePair:
                int pairCmp = xGroups[0].Key.CompareTo(yGroups[0].Key);
                if (pairCmp != 0) return pairCmp;
                // Compare remaining cards
                var xKickers = xGroups.Skip(1).Select(g => g.Key).OrderByDescending(k => k).ToList();
                var yKickers = yGroups.Skip(1).Select(g => g.Key).OrderByDescending(k => k).ToList();
                for (int i = 0; i < xKickers.Count; i++)
                {
                    int cmp = xKickers[i].CompareTo(yKickers[i]);
                    if (cmp != 0) return cmp;
                }
                break;
            case HandType.Flush:
            case HandType.HighCard:
                for (int i = 0; i < xRanks.Count; i++)
                {
                    int cmp = xRanks[i].CompareTo(yRanks[i]);
                    if (cmp != 0) return cmp;
                }
                break;
        }
        return 0;
    }
}