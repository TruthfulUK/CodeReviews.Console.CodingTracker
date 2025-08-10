using static CodingTracker.Helpers.Formatting;
using CodingTracker.Data;
using CodingTracker.Helpers;
using CodingTracker.Models;
using Spectre.Console;
using System.Diagnostics;
namespace CodingTracker.Controllers;
internal static class StopwatchController
{
    internal static void StartTimedSession()
    {
        CodingSession session = new CodingSession();
        Stopwatch stopwatch = new Stopwatch();

        bool sessionEnded = false;

        AnsiConsole.Clear();
        InputHelpers.DisplayHeader($"Stopwatch Mode: Timed Coding Session");

        sessionEnded = !InputHelpers.ConfirmationPrompt("Ready to time your Coding Session? Type 'Y' or just hit Enter to start!\n", defaultValue: true);

        if (!sessionEnded)
        {
            stopwatch.Start();
            session.StartTime = DateTime.Now;
            AnsiConsole.MarkupLine($"\n[bold]Your Timed Coding Session has started.[/] The default value is now set to `N` so you do not accidentally end your session. Please type and Enter 'Y' to explicity end the timer.\n");

            while (!sessionEnded)
            {
                sessionEnded = InputHelpers.ConfirmationPrompt("End Timed Coding Session?", defaultValue: false);
            }

            AnsiConsole.MarkupLine($"\n[bold]Coding Session Stopwatch Ended at: [blue]{stopwatch.Elapsed}[/][/]");

            session.EndTime = DateTime.Now;
            LogSession(session);

            InputHelpers.PressKeyToContinue();
        }
    }

    internal static void LogSession(CodingSession session)
    {
        Database.InsertSession(FormalSqlDateTimeString(session.StartTime), FormalSqlDateTimeString(session.EndTime));
        AnsiConsole.MarkupLine($"\n[bold]Your Coding Session has been successfully logged![/]");
    }
}
