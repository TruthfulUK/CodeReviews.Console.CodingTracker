using CodingTracker.Data;
using CodingTracker.Models;
using Spectre.Console;
using static CodingTracker.Helpers.Formatting;

namespace CodingTracker.Controllers;
internal class CodingSessionController
{
    internal static void ViewRecent()
    {
        List<CodingSession> rows = Database.FetchSessions(10);

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
            .Collapse();

        AnsiConsole.Write(CSTable);

    }

    internal static void LogSession()
    {

    }
}
