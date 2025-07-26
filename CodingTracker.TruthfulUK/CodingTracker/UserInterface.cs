using CodingTracker.Controllers;
using CodingTracker.Data;
using CodingTracker.Helpers;
using Spectre.Console;
using static CodingTracker.Enums;

namespace CodingTracker;
internal class UserInterface
{
    public static void DisplayMainMenu()
    {
        while (true)
        {
            AnsiConsole.Clear();
            AnsiConsole.Write(
                new FigletText("Coding Tracker")
                    .Centered()
                    .Color(Color.Blue));

            var rule = new Rule().RuleStyle("blue dim");
            AnsiConsole.Write(rule);

            var mainMenuOptions = InputHelpers.GetMenuOptions<MainMenu>();
            var mainMenuChoice = InputHelpers.SelectionPrompt(mainMenuOptions);

            switch (mainMenuChoice)
            {
                case MainMenu.ManageCodingSessions:
                    CodingSessionManager.DisplayRows(Database.FetchAllSessions());
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.ManageGoals:
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.StopwatchMode:
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.ExitApplication:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
