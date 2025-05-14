namespace PokerHands;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x.HandType != y.HandType)
        {
            return x.HandType.CompareTo(y.HandType);
        }
        
        var xCards = x.Sorted;
        var yCards = y.Sorted;

        for (int i = 0; i < xCards.Count; i++)
        {
            var card1 = xCards.ElementAt(i);
            var card2 = yCards.ElementAt(i);

            if (card1.Rank != card2.Rank)
            {
                return card1.Rank.CompareTo(card2.Rank);
            }
        }

        return 0;
    }
}