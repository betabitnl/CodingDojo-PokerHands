namespace PokerHands;
public class Hand
{
    public ICollection<Card> Cards { get; init; } = new List<Card>(5);
    public HandType HandType => HandType.HighCard;
}