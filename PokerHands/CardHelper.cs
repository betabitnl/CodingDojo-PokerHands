namespace PokerHands;

public static class CardHelper
{
    public static Card ToCard(this string cardString)
    {
        if (cardString.Length != 2)
        {
            throw new ArgumentException($"Invalid card string: {cardString}");
        }
        var color = cardString[1] switch
        {
            'S' => Color.Spades,
            'H' => Color.Hearts,
            'D' => Color.Diamonds,
            'C' => Color.Clubs,
            _ => throw new ArgumentException($"Invalid color: {cardString[1]}")
        };
        var rank = cardString[0] switch
        {
            '2' => Rank.Two,
            '3' => Rank.Three,
            '4' => Rank.Four,
            '5' => Rank.Five,
            '6' => Rank.Six,
            '7' => Rank.Seven,
            '8' => Rank.Eight,
            '9' => Rank.Nine,
            'T' => Rank.Ten,
            'J' => Rank.Jack,
            'Q' => Rank.Queen,
            'K' => Rank.King,
            'A' => Rank.Ace,
            _ => throw new ArgumentException($"Invalid rank: {cardString[0]}")
        };
        return new Card(color, rank);
    }
}