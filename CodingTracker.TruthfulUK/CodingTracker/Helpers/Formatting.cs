namespace CodingTracker.Helpers;
internal static class Formatting
{
    internal static string FormatTimeSpan(TimeSpan ts) =>
        ts switch
        {
            { TotalSeconds: < 60 } => $"{ts.Seconds}s",
            { TotalMinutes: < 60 } => $"{ts.Minutes}m {ts.Seconds}s",
            _ => $"{(int)ts.TotalHours}h {ts.Minutes}m {ts.Seconds}s"
        };

    internal static string FormalSqlDateTimeString(DateTime date)
    {
        return date.ToString("yyyy-MM-dd HH:mm:ss");
    }

    internal static string FormatSqlDateOnlyString(DateOnly date)
    {
        return date.ToString("yyyy-MM-dd");
    }
}

