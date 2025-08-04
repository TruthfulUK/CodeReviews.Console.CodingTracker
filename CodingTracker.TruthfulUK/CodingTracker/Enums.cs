using System.ComponentModel.DataAnnotations;

namespace CodingTracker;
internal class Enums
{
    internal enum MainMenu
    {
        [Display(Name = "Manage Coding Sessions")]
        ManageCodingSessions,

        [Display(Name = "My Goals")]
        ManageGoals,

        [Display(Name = "Stopwatch Mode")]
        StopwatchMode,

        [Display(Name = "Reporting")]
        Reporting,

        [Display(Name = "Exit Application")]
        ExitApplication
    }

    internal enum CodingSessionMenu
    {
        [Display(Name = "View & Manage Logged Sessions")]
        ManageSessions,

        [Display(Name = "Log a New Coding Session")]
        LogSession,

        [Display(Name = "Back to Main Menu")]
        BackToMain
    }

    internal enum CodingSessionManageMenu
    {
        [Display(Name = "Next page")]
        NextPage,

        [Display(Name = "Previous Page")]
        PrevPage,

        [Display(Name = "Update a Session")]
        UpdateSession,

        [Display(Name = "Delete a Session")]
        DeleteSession,

        [Display(Name = "Back to Main Menu")]
        BackToMain
    }
}
