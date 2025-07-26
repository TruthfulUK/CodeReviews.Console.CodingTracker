using CodingTracker.Models;

namespace CodingTracker.Controllers;
internal class CodingSessionManager
{
    internal static void DisplayRows(List<CodingSession> rows)
    {
        foreach (CodingSession row in rows)
        {
            Console.WriteLine($"{row.Duration.Hours} hours, {row.Duration.Minutes} minutes");
        }
    }
}
