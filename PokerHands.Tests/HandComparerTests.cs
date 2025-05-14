using Xunit;

namespace PokerHands.Tests;

public class HandComparerTests
{
    private readonly HandComparer _sut = new();
    
    [Theory]
    [InlineData("2D 3D 4D 5D KC", "2C 2S 4C 5C 6C")] // High card vs One pair
    [InlineData("2C 2S 4C 5C 6C", "3C 3C 4S 4D 6S")] // One pair vs Two pair
    [InlineData("3C 3C 4S 4D 6S", "2C 2S 2D 5C 6C")] // Two pair vs Three of a kind
    [InlineData("2C 2S 2D 5C 6C", "2C 3C 4C 5D 6D")] // Three of a kind vs Straight
    [InlineData("2C 3C 4C 5D 6D", "2S 3S 4S 5S 7S")] // Straight vs Flush
    [InlineData("2S 3S 4S 5S 7S", "2C 2C 2D 5C 5D")] // Flush vs Full house
    [InlineData("2C 2C 2D 5C 5D", "7C 7D 7S 7H 6S")] // Full house vs Four of a kind
    [InlineData("7C 7D 7S 7H 6S", "2C 3C 4C 5C 6C")] // Four of a kind vs Straight flush
    public void Compare_HandsWithDifferentTypes_ReturnsNonZero(string hand1, string hand2)
    {
        // Arrange
        // Act
        var result = _sut.Compare(hand1.ToHand(), hand2.ToHand());
        // Assert
        Assert.Equal(-1, result);
    }

    [Theory]
    [InlineData("3D 4D 5D 6D KC", "2C 4C 5C 6C KS")] // High card 5th card is higher
    [InlineData("2D 4D 5D 6D KC", "2C 3C 5C 6C KS")] // High card 4th card is higher
    [InlineData("2D 3D 5D 6D KC", "2C 3C 4C 6C KS")] // High card 3rd card is higher
    [InlineData("2D 3D 4D 6D KC", "2C 3C 4C 5C KS")] // High card second card is higher
    [InlineData("2D 3D 4D 5D KC", "2C 3C 4C 5C QS")] // High card first card is higher

    [InlineData("3C 3D 4D 5D KC", "2C 2S 4C 5C 6C")] // One pair
    [InlineData("2H 2D 4D 5D KC", "2C 2S 3C 5C KH")] // One pair the same, 3rd card is higher
    [InlineData("2H 2D 4D 5D KC", "2C 2S 3C 4C KH")] // One pair the same, second card is higher
    [InlineData("2H 2D 4D 5D KC", "2C 2S 4C 5C QH")] // One pair the same, first card is higher

    [InlineData("3C 3S 5C 5C 6C", "3C 3H 4S 4D 6S")] // Two pair, higher the same, second pair is higher
    [InlineData("3C 3S 4C 4H 6C", "3C 3H 4S 4D 5S")] // Two pair the same, 5th card is higher
    
    [InlineData("3C 3C 3S 4D 6S", "2C 2S 2D 5C 6C")] // Three of a kind
    
    [InlineData("3C 4C 5D 6D 7D", "2C 2S 2D 5C 6C")] // Straight
    [InlineData("2C 3C 4C 5D 6D", "AC 2C 2S 2D 5C")] // Straight with ace low

    [InlineData("3D 4D 5D 6D KD", "2C 4C 5C 6C KC")] // Flush card 5th card is higher
    [InlineData("2D 4D 5D 6D KD", "2C 3C 5C 6C KC")] // Flush card 4th card is higher
    [InlineData("2D 3D 5D 6D KD", "2C 3C 4C 6C KC")] // Flush card 3rd card is higher
    [InlineData("2D 3D 4D 6D KD", "2C 3C 4C 5C KC")] // Flush card second card is higher
    [InlineData("2D 3D 4D 5D KD", "2C 3C 4C 5C QC")] // Flush card first card is higher

    [InlineData("3C 3H 3D 5C 5D", "2C 2H 2D 6H 6S")] // Full house

    [InlineData("7C 7D 7S 7H 6S", "2C 2H 2D 2S 5D")] // Four of a kind

    [InlineData("3D 4D 5D 6D 7D", "2C 3C 4C 5C 6C")] // Straight flush
    [InlineData("AD 2D 3D 4D 5D", "2C 3C 4C 5C 6C")] // Straight flush with ace low
    public void Compare_HandsWithSameTypeDifferentCards_ReturnsNonZero(string hand1, string hand2)
    {
        // Arrange
        // Act
        var result = _sut.Compare(hand1.ToHand(), hand2.ToHand());
        // Assert
        Assert.Equal(1, result);
    }

    [Theory]
    [InlineData("2D 3D 4D 5D KC", "2D 3D 4D 5D KC")] // High card 
    [InlineData("2C 2S 4C 5C 6C", "2C 2S 4C 5C 6C")] // One pair
    [InlineData("3C 3C 4S 4D 6S", "3C 3C 4S 4D 6S")] // Two pair
    //[InlineData("2C 2S 2D 5C 6C", "2C 2S 2D 5C 6C")] // Three of a kind   No Tie possible in three of a kind
    [InlineData("2C 3C 4C 5D 6D", "2C 3C 4C 5D 6D")] // Straight
    [InlineData("2S 3S 4S 5S 7S", "2S 3S 4S 5S 7S")] // Flush
    //[InlineData("2C 2C 2D 5C 5D", "2C 2C 2D 5C 5D")] // Full house        No Tie possible in full house
    //[InlineData("7C 7D 7S 7H 6S", "7C 7D 7S 7H 6S")] // Four of a kind    No Tie possible in four of a kind
    [InlineData("2C 3C 4C 5C 6C", "2C 3C 4C 5C 6C")] // Straight flush
    public void Compare_HandsWithSameTypeSameCardsDifferentColor_ReturnsZero(string hand1, string hand2)
    {
        // Arrange
        // Act
        var result = _sut.Compare(hand1.ToHand(), hand2.ToHand());
        // Assert
        Assert.Equal(0, result);
    }
}

