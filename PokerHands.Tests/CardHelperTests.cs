using FluentAssertions;
using Xunit;

namespace PokerHands.Tests
{
    public class CardHelperTests
    {
        [Theory]
        [InlineData("AS", Color.Spades, Rank.Ace)]
        [InlineData("2S", Color.Spades, Rank.Two)]
        [InlineData("3S", Color.Spades, Rank.Three)]
        [InlineData("4S", Color.Spades, Rank.Four)]
        [InlineData("5S", Color.Spades, Rank.Five)]
        [InlineData("6S", Color.Spades, Rank.Six)]
        [InlineData("7S", Color.Spades, Rank.Seven)]
        [InlineData("8S", Color.Spades, Rank.Eight)]
        [InlineData("9S", Color.Spades, Rank.Nine)]
        [InlineData("TS", Color.Spades, Rank.Ten)]
        [InlineData("JS", Color.Spades, Rank.Jack)]
        [InlineData("QS", Color.Spades, Rank.Queen)]
        [InlineData("KS", Color.Spades, Rank.King)]
        [InlineData("2H", Color.Hearts, Rank.Two)]
        [InlineData("2D", Color.Diamonds, Rank.Two)]
        [InlineData("2C", Color.Clubs, Rank.Two)]
        public void ToCard_ValidCardString_ShouldReturnCard(string cardString, Color expectedColor, Rank expectedRank)
        {
            // Arrange
            // Act
            var result = cardString.ToCard();
            
            // Assert
            result.Color.Should().Be(expectedColor);
            result.Rank.Should().Be(expectedRank);
        }

        [Theory]
        [InlineData("A", "Invalid card string: A")]
        [InlineData("ASD", "Invalid card string: ASD")]
        [InlineData("1H", "Invalid rank: 1")]
        [InlineData("AX", "Invalid color: X")]
        public void ToCard_InvalidCardString_ShouldThrow(string cardString, string expectedMessage)
        {
            // Arrange
            // Act
            Action act = () => cardString.ToCard();
            
            // Assert
            act.Should().Throw<ArgumentException>()
                .WithMessage(expectedMessage);
        }
    }
}
