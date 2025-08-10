using CodingTracker.Controllers;
using CodingTracker.Helpers;
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
                            CodingSessionController.ViewManageAllSessions();
                            break;
                        case CodingSessionMenu.PeriodSessions:
                            CodingSessionController.ViewManageFilteredSessions();
                            break;
                        case CodingSessionMenu.LogSession:
                            CodingSessionController.LogSession();
                            InputHelpers.PressKeyToContinue();
                            break;
                        case CodingSessionMenu.BackToMain:
                            break;
                    }
                    break;
                case MainMenu.StopwatchMode:
                    StopwatchController.StartTimedSession();
                    break;
                case MainMenu.Reports:
                    ReportsController.GenerateReport();
                    break;
                case MainMenu.Goals:
                    GoalController.DisplayMenu();
                    break;
                case MainMenu.ExitApplication:
                    Environment.Exit(0);
                    break;
            }
        }
    }
}
