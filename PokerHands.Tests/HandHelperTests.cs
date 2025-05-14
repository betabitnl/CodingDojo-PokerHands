using FluentAssertions;
using Xunit;

namespace PokerHands.Tests;

public class HandHelperTests
{
    [Fact]
    public void ToHand_ValidString_ShouldReturnHand()
    {
        // Arrange
        var handString = "AS 2H 3D 4C 5S";
        var expectedHand = new Hand
        {
            Cards = new List<Card>
            {
                new(Color.Spades, Rank.Ace),
                new(Color.Hearts, Rank.Two),
                new(Color.Diamonds, Rank.Three),
                new(Color.Clubs, Rank.Four),
                new(Color.Spades, Rank.Five)
            }
        };
        // Act
        var result = handString.ToHand();
            
        // Assert
        result.Cards.Count.Should().Be(expectedHand.Cards.Count);
    }
}