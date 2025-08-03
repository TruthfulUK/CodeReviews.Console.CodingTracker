using CodingTracker.Data;
using CodingTracker.Helpers;
using CodingTracker.Models;
using Spectre.Console;
using static CodingTracker.Enums;
using static CodingTracker.Helpers.Formatting;

namespace CodingTracker.Controllers;
internal class CodingSessionController
{
    internal static void ViewRecent()
    {
        int limit = 5;
        int offset = 0;
        bool exitView = false;

        while (!exitView)
        {
            List<CodingSession> rows = Database.FetchSessions(limit, offset);

            var CSTable = new Table();
            CSTable
                .AddColumn(new TableColumn("[white on blue] ID # [/]").Centered())
                .AddColumn("[white on blue] Session Started [/]")
                .AddColumn("[white on blue] Session Ended [/]")
                .AddColumn("[white on blue] Duration [/]");

            foreach (CodingSession row in rows)
            {
                CSTable.AddRow(
                    $"{row.Id}",
                    $"{row.StartTime}",
                    $"{row.EndTime}",
                    $"{FormatTimeSpan(row.Duration)}"
                );
            }

            CSTable
                .ShowRowSeparators()
                .Border(TableBorder.Horizontal)
                .Expand();

            InputHelpers.DisplayHeader("Recent Coding Sessions");

            AnsiConsole.Write(CSTable);

            var CSRecentMenu = InputHelpers.GetMenuOptions<CodingSessionViewMenu>();
            var CSRecentMenuChoice = InputHelpers.SelectionPrompt(CSRecentMenu);

            switch (CSRecentMenuChoice)
            {
                case CodingSessionViewMenu.NextPage:
                    List<CodingSession> nextPageRows = Database.FetchSessions(limit, offset + limit);
                    if (nextPageRows.Count == 0) break;
                    offset += limit;
                    break;
                case CodingSessionViewMenu.PrevPage:
                    if (offset == 0) break;
                    offset -= limit;
                    break;
                case CodingSessionViewMenu.BackToMain:
                    exitView = true;
                    break;
            }
        }
    }

    internal static void LogSession()
    {
        bool datesValidated = false;

        AnsiConsole.MarkupLine("[white on blue]Tip:[/] Press enter with no input to use the [green]default values[/] (today / now)");
        AnsiConsole.MarkupLine("Please enter the date and time your [underline]Coding Session started[/]: \n");
        var dateStarted = InputHelpers.DatePrompt();
        var timeStarted = InputHelpers.TimePrompt();
        var sessionStarted = dateStarted.ToDateTime(timeStarted);

        while (datesValidated == false) { 
            AnsiConsole.MarkupLine("\n[white on blue]Tip:[/] Press enter with no input to use the [green]default values[/] (today / now)");
            AnsiConsole.MarkupLine("Please enter the date and time your [underline]Coding Session ended[/]: \n");
            var dateEnded = InputHelpers.DatePrompt();
            var timeEnded = InputHelpers.TimePrompt();
            var sessionEnded = dateEnded.ToDateTime(timeEnded);

            if (sessionEnded >= sessionStarted)
            {
                datesValidated = true;
                Database.InsertSession(FormatSQLDateString(sessionStarted), FormatSQLDateString(sessionEnded));
            }
            else
            {
                AnsiConsole.MarkupLine($"\n[red]Your session cannot end before it has started - please enter a valid end date and time.[/]\n");
            }
        }
    }
}
