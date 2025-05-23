
namespace PokerHands;

public class HandComparer : IComparer<Hand>
{
    public int Compare(Hand? x, Hand? y)
    {
        if (x.HandType != y.HandType)
        {
            return x.HandType > y.HandType ? 1 : -1;
        }
        
        switch (x.HandType)
        {
            case HandType.ThreeOfAKind:
            case HandType.FullHouse:
            case HandType.FourOfAKind:
                return CompareMiddleCard(x, y);
            default:
                return CompareHighCard(x, y);
        }    
    }

    private int CompareHighCard(Hand x, Hand y)
    {
        var xCards = x.Cards.OrderByDescending(c => c.Rank);
        var yCards = y.Cards.OrderByDescending(c => c.Rank);

        for (int i = 0; i < xCards.Count(); i++)
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

    private int CompareMiddleCard(Hand x, Hand y)
    {
        return x.Sorted[2].Rank.CompareTo(y.Sorted[2].Rank);
    }
}