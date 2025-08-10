using CodingTracker.Data;
using CodingTracker.Helpers;
using CodingTracker.Models;
using Spectre.Console;
using static CodingTracker.Enums;
using static CodingTracker.Helpers.Formatting;

namespace CodingTracker.Controllers;
internal static class ReportsController
{
    internal static void GenerateReport()
    {
        AnsiConsole.Clear();
        InputHelpers.DisplayHeader($"Select Report Period");

        List<CodingSession> rows = new List<CodingSession>();
        TimeSpan totalTimeLogged = new TimeSpan();

        var PeriodFilters = InputHelpers.GetMenuOptions<CodingSessionPeriodOptions>();
        var PeriodFiltersOption = InputHelpers.SelectionPrompt(PeriodFilters);
        int days = 0;

        AnsiConsole.MarkupLine($"\n[white on blue] Note: [/] This will create a report for all of your logged Coding Sessions from the entered timeframe in Days, Weeks or Months to today.\n");

        switch (PeriodFiltersOption)
        {
            case CodingSessionPeriodOptions.Days:
                days = InputHelpers.PeriodLengthPrompt("Enter number of days (from today):");
                rows = Database.FetchSessions(limit: -1, offset: 0, "DAY", days);
                break;
            case CodingSessionPeriodOptions.Weeks:
                days = InputHelpers.PeriodLengthPrompt("Enter number of weeks (from today):");
                rows = Database.FetchSessions(limit: -1, offset: 0, "DAY", days * 7);
                break;
            case CodingSessionPeriodOptions.Months:
                int months = InputHelpers.PeriodLengthPrompt("Enter number of months (from today):");
                rows = Database.FetchSessions(limit: -1, offset: 0, "MONTH", months);
                break;
        }

        int rowCount = rows.Count;

        foreach (CodingSession row in rows)
        {
            totalTimeLogged += row.Duration;
        }

        DisplayReport(totalTimeLogged, rowCount);
    }

    internal static void DisplayReport(TimeSpan totalTimeLogged, int rowCount)
    {
        var ReportTable = new Table();
        ReportTable
            .AddColumn("[white on blue] Total Sessions [/]")
            .AddColumn("[white on blue] Total Time Logged [/]")
            .AddColumn("[white on blue] Avg. Session Length [/]")
            .AddRow(
                $"{rowCount}",
                $"{FormatTimeSpan(totalTimeLogged)}",
                $"{FormatTimeSpan(totalTimeLogged / rowCount)}")
            .ShowRowSeparators()
            .Border(TableBorder.Horizontal)
            .Expand();

        AnsiConsole.Clear();
        InputHelpers.DisplayHeader($"Report");
        AnsiConsole.Write(ReportTable);

        InputHelpers.PressKeyToContinue();
    }
}
