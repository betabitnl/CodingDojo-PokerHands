using System.Text.RegularExpressions;
using PokerHands;

public class Program
{

    static string[] listOfHands = new[]
    {
        "Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C AH",
        "Black: 2H 4S 4C 2D 4H  White: 2S 8S AS QS 3S",
        "Black: 2H 3D 5S 9C KD  White: 2C 3H 4S 8C KH",
        "Black: 2H 3D 5S 9C KD  White: 2D 3H 5C 9S KH"
    };

    const string pattern = @"^Black: (?'hand1'.*)  White: (?'hand2'.*)";

// parse the hands
// compare them
// print the result

    public static Task Main(string[] args)
    {
        foreach (var hands in listOfHands)
        {
            foreach (Match m in Regex.Matches(hands, pattern))
            {
                var black = m.Groups[1];
                var white = m.Groups[2];

                var blackHand = black.Value.ToHand();
                var whiteHand = white.Value.ToHand();

                var comparer = new HandComparer();

                var result = comparer.Compare(blackHand, whiteHand);

                switch (result)
                {
                    case -1:
                        Console.WriteLine($"Black wins. - with {blackHand}");
                        break;
                    case 0:
                        Console.WriteLine("Tie");
                        break;
                    case 1:
                        Console.WriteLine($"White wins. - with {whiteHand}");
                        break;
                }
            }
        }
        return Task.CompletedTask;
    }
}