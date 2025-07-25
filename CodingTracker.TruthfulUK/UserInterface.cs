using Spectre.Console;

namespace CodingTracker.TruthfulUK;
internal class UserInterface
{
    public static void DisplayNavigation()
    {
        while (true)
        {
            AnsiConsole.MarkupLine("Hello World.");
            Console.ReadKey();
            Environment.Exit(0);
        }
    }
}
