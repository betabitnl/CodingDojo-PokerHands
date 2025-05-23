using FluentAssertions;
using Xunit;

namespace PokerHands.Tests;

public class HandTests
{
    [Theory]
    [InlineData("AS 5H 7D 9C TS", HandType.HighCard)]
    [InlineData("AS AH 3D 4C 5S", HandType.OnePair)]
    [InlineData("AS AH 3D 3C 5S", HandType.TwoPair)]
    [InlineData("AS AH AS 4C 5S", HandType.ThreeOfAKind)]
    [InlineData("AS 2H 3S 4C 5S", HandType.Straight)]
    [InlineData("2H 3S 4C 5S 6D", HandType.Straight)]
    [InlineData("TD JD QD KD AS", HandType.Straight)]
    [InlineData("AS 4S 6S 8S TS", HandType.Flush)]
    [InlineData("AS AH AS 5C 5S", HandType.FullHouse)]
    [InlineData("AS AH AS AC 5S", HandType.FourOfAKind)]
    [InlineData("AH 2H 3H 4H 5H", HandType.StraightFlush)]
    [InlineData("2C 3C 4C 5C 6C", HandType.StraightFlush)]
    [InlineData("TD JD QD KD AD", HandType.StraightFlush)]
    public void ToHand_ValidHandString_ShouldReturnHand(string handString, HandType expectedHandType)
    {
        // Arrange
        // Act
        var result = handString.ToHand();

        // Assert
        result.HandType.Should().Be(expectedHandType);
    }

    [Fact]
    public void ToHand_InvalidHandString_ShouldThrowException()
    {
        // Arrange
        var handString = "AS AS AS AS AS";
        var result = handString.ToHand();

        // Act
        HandType GetHandType() => result.HandType;
        Action act = () => GetHandType();

        // Assert
        act.Should().Throw<NotImplementedException>()
            .WithMessage("Hand type not implemented yet.");

    }
    [Theory]
    [InlineData("AS 5H 7D 9C TS", "High Card Ace")]
    [InlineData("AS AH 3D 4C 5S", "One Pair Ace")]
    [InlineData("AS AH 3D 3C 5S", "Two Pair Ace and Three")]
    [InlineData("AS AH AS 4C 5S", "Three Of A Kind Ace")]
    [InlineData("AS 2H 3S 4C 5S", "Straight Five high")]
    [InlineData("2H 3S 4C 5S 6D", "Straight Six high")]
    [InlineData("TD JD QD KD AS", "Straight Ace high")]
    [InlineData("AS 4S 6S 8S TS", "Flush Spades")]
    [InlineData("AS AH AS 5C 5S", "Full House Ace over Five")]
    [InlineData("AS AH AS AC 5S", "Four Of A Kind Ace")]
    [InlineData("AH 2H 3H 4H 5H", "Straight Flush Hearts Five high")]
    [InlineData("2C 3C 4C 5C 6C", "Straight Flush Clubs Six high")]
    [InlineData("TD JD QD KD AD", "Straight Flush Diamonds Ace high")]
    public void ToHand_ValidHandString_ShouldToString(string handString, string expectedToString)
    {
        // Arrange
        // Act
        var result = handString.ToHand();

        // Assert
        result.ToString().Should().Be(expectedToString);
    }
}

