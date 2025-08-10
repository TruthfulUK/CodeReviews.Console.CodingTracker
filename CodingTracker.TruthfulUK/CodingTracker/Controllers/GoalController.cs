using Spectre.Console;
using CodingTracker.Data;
using CodingTracker.Helpers;
using CodingTracker.Models;
using static CodingTracker.Helpers.Formatting;
using static CodingTracker.Enums;

namespace CodingTracker.Controllers;
internal static class GoalController
{
    internal static void DisplayMenu()
    {
        var GoalMenuOptions = InputHelpers.GetMenuOptions<GoalsMenu>();
        var GoalMenuSelection = InputHelpers.SelectionPrompt(GoalMenuOptions);

        switch (GoalMenuSelection)
        {
            case GoalsMenu.ViewGoals:
                ViewGoals();
                break;
            case GoalsMenu.CreateGoal:
                CreateGoal();
                break;
            case GoalsMenu.BackToMain:
                break;
        }
    }

    internal static void ViewGoals()
    {
        List<Goal> Goals = Database.FetchGoals();
        List<CodingSession> MatchingGoalRows = new List<CodingSession>();

        var GoalTable = new Table();
        GoalTable
            .AddColumn(new TableColumn("[white on blue] ID # [/]").Centered())
            .AddColumn("[white on blue] Goal Start Date [/]")
            .AddColumn("[white on blue] Goal End Date [/]")
            .AddColumn("[white on blue] Goal Hours [/]")
            .AddColumn("[white on blue] Logged Time [/]")
            .AddColumn("[white on blue] Daily Target [/]")
            .ShowRowSeparators()
            .Border(TableBorder.Horizontal)
            .Expand();

        foreach (Goal goal in Goals)
        {
            MatchingGoalRows = Database.FetchSessionsBetweenDates(goal.GoalStartDate, goal.GoalEndDate);
            int goalRemainingDays = (goal.GoalEndDate - DateTime.Now).Days + 1;

            TimeSpan loggedTime = CalculateLoggedTime(MatchingGoalRows);
            var dailyTarget = CalculateTargetTime(goal.GoalMinutes, goalRemainingDays, loggedTime, goal.GoalEndDate);

            GoalTable.AddRow(
                $"{goal.Id}",
                $"{goal.GoalStartDate.ToString("dd/MM/yyyy")}",
                $"{goal.GoalEndDate.ToString("dd/MM/yyyy")}",
                $"{goal.GoalMinutes / 60}h",
                $"{FormatTimeSpan(loggedTime)}",
                $"{dailyTarget}"
            );
        }

        InputHelpers.DisplayHeader($"My Goals");
        AnsiConsole.Write(GoalTable);

        InputHelpers.PressKeyToContinue();
    }

    internal static TimeSpan CalculateLoggedTime(List<CodingSession> rows)
    {
        TimeSpan totalTime = new TimeSpan();
        foreach (CodingSession session in rows)
        {
            totalTime += session.Duration;
        }
        return totalTime;
    }

    internal static string CalculateTargetTime(int goal, int remainingDays, TimeSpan loggedTime, DateTime goalEndDate)
    {
        TimeSpan goalMinutes = TimeSpan.FromMinutes(goal);
        TimeSpan target = goalMinutes - loggedTime;

        return (loggedTime > goalMinutes || DateTime.Now > goalEndDate) ? "Goal Ended" : FormatTimeSpan(target / remainingDays);
    }

    internal static void CreateGoal()
    {
        bool datesValidated = false;
        DateOnly goalStart = new DateOnly();
        DateOnly goalEnd = new DateOnly();

        InputHelpers.DisplayHeader($"Create Goal");

        AnsiConsole.MarkupLine($"\n[white on blue] Note: [/] Please enter the goal start date. \n");
        goalStart = InputHelpers.DatePrompt();

        while (datesValidated == false)
        {
            AnsiConsole.MarkupLine($"\n[white on blue] Note: [/] Please enter the target date for achieving your goal. \n");
            goalEnd = InputHelpers.DatePrompt();

            if (goalEnd >= goalStart)
            {
                datesValidated = true;
            }
            else
            {
                AnsiConsole.MarkupLine($"\n[red]Your goal cannot end before it has started - please enter a valid end date.[/]\n");
            }
        }

        int goalHours = AnsiConsole.Prompt(
            new TextPrompt<int>("\nEnter the number of Coding Hours you want to aim for: ")
        );

        Database.CreateGoal(FormatSqlDateOnlyString(goalStart), FormatSqlDateOnlyString(goalEnd), goalHours * 60);

        AnsiConsole.MarkupLine($"\n[bold]Your goal of logging {goalHours} Coding hours between {goalStart} and {goalEnd} has been successfully added![/]");

        InputHelpers.PressKeyToContinue();
    }
}
