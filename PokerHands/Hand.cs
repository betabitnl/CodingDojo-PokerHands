namespace PokerHands;
public class Hand
{
    public ICollection<Card> Cards { get; init; } = new List<Card>(5);
    public HandType HandType => GetHandType();
    public IList<Card> Sorted => Cards.OrderBy(c => c.Rank).ToList();

    private HandType GetHandType()
    {
        var groupByRank = Cards.GroupBy(c => c.Rank);
        var groupByColor = Cards.GroupBy(c => c.Color);
        switch (groupByRank.Count())
        {
            case 5 when groupByColor.Count() == 1:
                {
                    if (IsStraight)
                    {
                        return HandType.StraightFlush;
                    }
                    return HandType.Flush;
                }
            case 5 when IsStraight:
                return HandType.Straight;
            case 5:
                return HandType.HighCard;
            case 4:
                return HandType.OnePair;
            case 3 when groupByRank.Any(g => g.Count() == 3):
                return HandType.ThreeOfAKind;
            case 3:
                return HandType.TwoPair;
            case 2 when groupByRank.Any(g => g.Count() == 4):
                return HandType.FourOfAKind;
            case 2:
                return HandType.FullHouse;
            default:
                throw new NotImplementedException("Hand type not implemented yet.");
        }
    }

    private bool IsStraight =>
         (Sorted.Last().Rank - Sorted.First().Rank == 4) ||
         (Sorted[0].Rank == Rank.Two &&
          Sorted[1].Rank == Rank.Three &&
          Sorted[2].Rank == Rank.Four &&
          Sorted[3].Rank == Rank.Five &&
          Sorted[4].Rank == Rank.Ace);

    public override string ToString()
    {
        var groupByRank = Cards.GroupBy(c => c.Rank);
        switch (HandType)
        {
            case HandType.HighCard:
                return $"High Card {Sorted.Last().Rank}";
            case HandType.OnePair:
                return $"One Pair {groupByRank.First(x => x.Count() == 2).First().Rank}";
            case HandType.TwoPair:
                var lists = groupByRank.Where(x => x.Count() == 2).OrderByDescending(s => s.First().Rank).ToList();
                return $"Two Pair {lists.First().First().Rank} and {lists.Last().First().Rank}";
            case HandType.ThreeOfAKind:
                return $"Three Of A Kind {groupByRank.First(x => x.Count() == 3).First().Rank}";
            case HandType.Straight:
                return Sorted.First().Rank switch
                {
                    Rank.Two when Sorted.Last().Rank == Rank.Ace =>
                        $"Straight {Sorted[3].Rank} high",
                    _ => $"Straight {Sorted.Last().Rank} high"
                };
            case HandType.Flush:
                return $"Flush {Cards.First().Color}";
            case HandType.FullHouse:
                return $"Full House {groupByRank.First(x => x.Count() == 3).First().Rank} over {groupByRank.First(x => x.Count() == 2).First().Rank}";
            case HandType.FourOfAKind:
                return $"Four Of A Kind {groupByRank.First(x => x.Count() == 4).First().Rank}";
            case HandType.StraightFlush:
                return Sorted.First().Rank switch
                {
                    Rank.Two when Sorted.Last().Rank == Rank.Ace =>
                        $"Straight Flush {Cards.First().Color} {Sorted[3].Rank} high",
                    _ => $"Straight Flush {Cards.First().Color} {Sorted.Last().Rank} high"
                };
        }
        return string.Empty;
    }
}