namespace CodingTracker.Helpers;
internal class Formatting
{
    internal static string FormatTimeSpan(TimeSpan ts) =>
        ts switch
        {
            { TotalSeconds: < 60 } => $"{ts.Seconds}s",
            { TotalMinutes: < 60 } => $"{ts.Minutes}m {ts.Seconds}s",
            _ => $"{(int)ts.TotalHours}h {ts.Minutes}m {ts.Seconds}s"
        };

    internal static string FormatSQLDateString(DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss");
    }
}

