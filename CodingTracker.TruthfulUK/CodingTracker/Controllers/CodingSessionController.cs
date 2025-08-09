using CodingTracker.Data;
using CodingTracker.Helpers;
using CodingTracker.Models;
using Spectre.Console;
using static CodingTracker.Enums;
using static CodingTracker.Helpers.Formatting;

namespace CodingTracker.Controllers;
internal class CodingSessionController
{
    internal static void ManageSessions()
    {
        int limit = 5;
        int page = 1;
        int offset = 0;
        bool exitSessionManager = false;

        while (!exitSessionManager)
        {
            List<CodingSession> rows = Database.FetchSessions(limit, offset);
            List<int> currentRowIds = new List<int>();

            var CSTable = new Table();
            CSTable
                .AddColumn(new TableColumn("[white on blue] ID # [/]").Centered())
                .AddColumn("[white on blue] Session Started [/]")
                .AddColumn("[white on blue] Session Ended [/]")
                .AddColumn("[white on blue] Duration [/]");

            foreach (CodingSession row in rows)
            {
                currentRowIds.Add(row.Id);
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

            InputHelpers.DisplayHeader($"Coding Sessions: Page #{page}");

            AnsiConsole.Write(CSTable);

            var CSRecentMenu = InputHelpers.GetMenuOptions<CodingSessionManageMenu>();
            var CSRecentMenuChoice = InputHelpers.SelectionPrompt(CSRecentMenu);

            switch (CSRecentMenuChoice)
            {
                case CodingSessionManageMenu.NextPage:
                    List<CodingSession> nextPageRows = Database.FetchSessions(limit, offset + limit);
                    if (nextPageRows.Count == 0) break;
                    offset += limit;
                    page++;
                    break;
                case CodingSessionManageMenu.PrevPage:
                    if (offset == 0) break;
                    offset -= limit;
                    page--;
                    break;
                case CodingSessionManageMenu.UpdateSession:
                    var rowToUpdate = InputHelpers.RowIdPrompt(currentRowIds);
                    LogSession(isUpdate: true, rowToUpdate);
                    InputHelpers.PressKeyToContinue();
                    break;
                case CodingSessionManageMenu.DeleteSession:
                    var rowToDelete = InputHelpers.RowIdPrompt(currentRowIds);
                    DeleteSession(rowToDelete);
                    InputHelpers.PressKeyToContinue();
                    break;
                case CodingSessionManageMenu.BackToMain:
                    exitSessionManager = true;
                    break;
            }
        }
    }

    internal static void LogSession(bool isUpdate = false, int updateRowId = 0)
    {
        bool datesValidated = false;

        AnsiConsole.MarkupLine("\n[white on blue]Tip:[/] Press enter with no input to use the [green]default values[/] (today / now)\n");
        AnsiConsole.MarkupLine("Please enter the date and time your [bold]Coding Session started[/]: \n");
        var dateStarted = InputHelpers.DatePrompt();
        var timeStarted = InputHelpers.TimePrompt();
        var sessionStarted = dateStarted.ToDateTime(timeStarted);

        while (datesValidated == false) { 
            AnsiConsole.MarkupLine("\nPlease enter the date and time your [bold]Coding Session ended[/]: \n");
            var dateEnded = InputHelpers.DatePrompt();
            var timeEnded = InputHelpers.TimePrompt();
            var sessionEnded = dateEnded.ToDateTime(timeEnded);

            if (sessionEnded >= sessionStarted)
            {
                datesValidated = true;
                if (isUpdate)
                {
                    Database.UpdateSession(updateRowId, FormatSQLDateString(sessionStarted), FormatSQLDateString(sessionEnded));
                    AnsiConsole.MarkupLine($"\n[bold]Your Coding Session with the [blue]ID # {updateRowId}[/] has been successfully updated![/]");
                } 
                else
                {
                    Database.InsertSession(FormatSQLDateString(sessionStarted), FormatSQLDateString(sessionEnded));
                    AnsiConsole.MarkupLine($"\n[bold]Your Coding Session has been successfully logged![/]");
                }       
            }
            else
            {
                AnsiConsole.MarkupLine($"\n[red]Your session cannot end before it has started - please enter a valid end date and time.[/]\n");
            }
        }
    }

    internal static void DeleteSession(int deleteRowId)
    {
        Database.DeleteSession(deleteRowId);
        AnsiConsole.MarkupLine($"\n[bold]Your Coding Session with the [blue]ID # {deleteRowId}[/] has been successfully [red]deleted[/].[/]\n");
    }
}
