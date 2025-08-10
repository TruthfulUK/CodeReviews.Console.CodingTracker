using System.ComponentModel.DataAnnotations;

namespace CodingTracker;
internal class Enums
{
    internal enum MainMenu
    {
        [Display(Name = "Manage Coding Sessions")]
        ManageCodingSessions,

        [Display(Name = "Stopwatch Mode")]
        StopwatchMode,

        [Display(Name = "Generate Report")]
        Reports,

        [Display(Name = "My Goals")]
        Goals,

        [Display(Name = "Exit Application")]
        ExitApplication
    }

    internal enum CodingSessionMenu
    {
        [Display(Name = "View & Manage All Logged Sessions")]
        ManageSessions,

        [Display(Name = "View & Manage Logged Sessions by Period")]
        PeriodSessions,

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

    internal enum CodingSessionPeriodOptions
    {
        [Display(Name = "Last X Days")]
        Days,

        [Display(Name = "Last X Weeks")]
        Weeks,

        [Display(Name = "Last X Months")]
        Months
    }

    internal enum GoalsMenu
    {
        [Display(Name = "View Goals & Status")]
        ViewGoals,

        [Display(Name = "Create a Goal")]
        CreateGoal,

        [Display(Name = "Back to Main Menu")]
        BackToMain
    }
}
