using System.Data.SQLite;
using System.Configuration;
using Dapper;
using CodingTracker.Models;

namespace CodingTracker.Data;
internal static class Database
{
    public static SQLiteConnection GetConnection()
    {
        var connectionString = ConfigurationManager.AppSettings.Get("DataSource");

        return new SQLiteConnection(connectionString);
    }

    public static void InitializeDatabase()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var createTableQuery = @"
                CREATE TABLE IF NOT EXISTS CodingSessions (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    StartTime DATETIME NOT NULL,
                    EndTime DATETIME NOT NULL
                )";
            connection.Execute(createTableQuery);
        }
    }

    public static List<CodingSession> FetchAllSessions()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var selectQuery = @"
            SELECT * From CodingSessions";
            return connection.Query<CodingSession>(selectQuery).ToList();
        }
    }

    public static void SeedDatabase()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var insertQuery = @"
                INSERT INTO CodingSessions (StartTime, EndTime) VALUES
                ('2025-07-15 09:00:00', '2025-07-15 09:50:00'),
                ('2025-07-15 14:10:00', '2025-07-15 15:00:00'),
                ('2025-07-16 08:30:00', '2025-07-16 09:20:00'),
                ('2025-07-16 18:45:00', '2025-07-16 19:40:00'),
                ('2025-07-17 10:00:00', '2025-07-17 11:00:00'),
                ('2025-07-17 15:30:00', '2025-07-17 16:15:00'),
                ('2025-07-18 07:15:00', '2025-07-18 08:05:00'),
                ('2025-07-18 20:00:00', '2025-07-18 20:50:00'),
                ('2025-07-19 11:30:00', '2025-07-19 12:20:00'),
                ('2025-07-19 16:00:00', '2025-07-19 17:00:00'),
                ('2025-07-20 09:10:00', '2025-07-20 10:00:00'),
                ('2025-07-20 13:40:00', '2025-07-20 14:35:00'),
                ('2025-07-21 08:00:00', '2025-07-21 08:55:00'),
                ('2025-07-21 17:20:00', '2025-07-21 18:10:00'),
                ('2025-07-22 10:10:00', '2025-07-22 11:05:00'),
                ('2025-07-22 19:00:00', '2025-07-22 19:50:00'),
                ('2025-07-23 07:45:00', '2025-07-23 08:35:00'),
                ('2025-07-23 14:25:00', '2025-07-23 15:20:00'),
                ('2025-07-24 12:00:00', '2025-07-24 13:00:00'),
                ('2025-07-24 18:30:00', '2025-07-24 19:15:00');";
            connection.Execute(insertQuery);
        }
    }
}
