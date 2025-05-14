using System.Text;

namespace PokerHands.Verify;

public class VerifyTests
{
    [Fact]
    public Task Verify()
    {
        var fakeoutput = new StringBuilder();
        Console.SetOut(new StringWriter(fakeoutput));
        Console.SetIn(new StringReader($"{Environment.NewLine}"));

        Program.Main(new string[] {} );
        var output = fakeoutput.ToString();

        return Verifier.Verify(output);
    }
}

