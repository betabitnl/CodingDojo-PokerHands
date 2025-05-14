namespace PokerHands;

public static class HandHelper
{
    /// <summary>
    /// Hand constructor that takes a string representation of the hand.
    /// e.g. AS is Ace of Spades, 2H is Two of Hearts, etc.
    /// </summary>
    /// <param name="handString"></param>

    public static Hand ToHand(this string handString)
    {
        var cards = new List<Card>(5);

        var cardStrings = handString.Split(' ');

        cards.AddRange(cardStrings.Select(cardString => cardString.ToCard()));

        return new Hand { Cards = cards };
    }
}