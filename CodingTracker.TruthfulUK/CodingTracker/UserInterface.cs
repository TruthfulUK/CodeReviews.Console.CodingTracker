using CodingTracker.Controllers;
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
                    var CSMenu = InputHelpers.GetMenuOptions<CodingSessionMenu>();
                    var CSMenuChoice = InputHelpers.SelectionPrompt(CSMenu);
                    switch (CSMenuChoice)
                    {
                        case CodingSessionMenu.ViewRecent:
                            CodingSessionController.ViewRecent();
                            InputHelpers.PressKeyToContinue();
                            break;
                        case CodingSessionMenu.LogSession:
                            break;
                        case CodingSessionMenu.UpdateSession:
                            break;
                        case CodingSessionMenu.DeleteSession:
                            break;
                        case CodingSessionMenu.BackToMain:
                            break;
                    }
                    break;
                case MainMenu.ManageGoals:
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.StopwatchMode:
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.Reporting:
                    InputHelpers.PressKeyToContinue();
                    break;
                case MainMenu.ExitApplication:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
