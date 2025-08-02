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
        [Display(Name = "View Recent Sessions")]
        ViewRecent,

        [Display(Name = "Log a Coding Session")]
        LogSession,

        [Display(Name = "Update a Coding Session")]
        UpdateSession,

        [Display(Name = "Delete a Coding Session")]
        DeleteSession,

        [Display(Name = "Back to Main Menu")]
        BackToMain
    }
}
