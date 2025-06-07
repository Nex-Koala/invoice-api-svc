using System.IO;
using Xunit;

namespace Server.Tests;

public class ProgramTests
{
    [Fact]
    public void Program_Should_LogExceptionObject()
    {
        var path = Path.Combine("..", "..", "api", "server", "Program.cs");
        var programText = File.ReadAllText(path);
        Assert.Contains("Log.Fatal(ex, \"unhandled exception\");", programText);
        Assert.DoesNotContain("Log.Fatal(ex.Message, \"unhandled exception\");", programText);
    }
}
