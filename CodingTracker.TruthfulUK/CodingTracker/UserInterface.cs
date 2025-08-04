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
            InputHelpers.DisplayHeader("Main Menu");

            var mainMenuOptions = InputHelpers.GetMenuOptions<MainMenu>();
            var mainMenuChoice = InputHelpers.SelectionPrompt(mainMenuOptions);

            switch (mainMenuChoice)
            {
                case MainMenu.ManageCodingSessions:
                    var CSMenu = InputHelpers.GetMenuOptions<CodingSessionMenu>();
                    var CSMenuChoice = InputHelpers.SelectionPrompt(CSMenu);
                    switch (CSMenuChoice)
                    {
                        case CodingSessionMenu.ManageSessions:
                            CodingSessionController.ManageSessions();
                            break;
                        case CodingSessionMenu.LogSession:
                            CodingSessionController.LogSession();
                            InputHelpers.PressKeyToContinue();
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
