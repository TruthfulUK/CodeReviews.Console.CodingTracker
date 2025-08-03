using CodingTracker.Models;
using Dapper;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SQLite;

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

    public static List<CodingSession> FetchSessions(int limit = 0, int offset = 0)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var parameters = new { Limit = limit, Offset = offset };
            var selectQuery = @"
                SELECT * From CodingSessions
                ORDER by id DESC
                LIMIT @Limit OFFSET @Offset";
            return connection.Query<CodingSession>(selectQuery, parameters).ToList();
        }
    }

    public static void InsertSession(string startTime, string endTime)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var parameters = new { StartTime = startTime, EndTime = endTime };
            var insertRow = @"
                INSERT INTO CodingSessions (StartTime, EndTime) 
                VALUES (@StartTime, @EndTime)";
            connection.Execute(insertRow, parameters);
        }
    }

    public static void SeedDatabase()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var insertQuery = @"
                INSERT INTO CodingSessions (StartTime, EndTime) VALUES
                ('2025-07-15 09:07:23', '2025-07-15 10:02:41'),
                ('2025-07-15 14:13:46', '2025-07-15 15:38:12'),
                ('2025-07-16 08:42:18', '2025-07-16 09:59:33'),
                ('2025-07-16 18:33:07', '2025-07-16 19:25:55'),
                ('2025-07-17 10:12:39', '2025-07-17 11:46:21'),
                ('2025-07-17 15:27:14', '2025-07-17 16:11:48'),
                ('2025-07-18 07:22:51', '2025-07-18 08:43:09'),
                ('2025-07-18 20:08:35', '2025-07-18 21:12:27'),
                ('2025-07-19 11:19:43', '2025-07-19 12:41:16'),
                ('2025-07-19 16:07:29', '2025-07-19 17:48:58'),
                ('2025-07-20 09:14:12', '2025-07-20 10:35:44'),
                ('2025-07-20 13:37:56', '2025-07-20 14:59:22'),
                ('2025-07-21 08:18:08', '2025-07-21 09:12:31'),
                ('2025-07-21 17:26:47', '2025-07-21 18:44:19'),
                ('2025-07-22 10:05:22', '2025-07-22 11:48:55'),
                ('2025-07-22 19:11:36', '2025-07-22 20:24:41'),
                ('2025-07-23 07:39:11', '2025-07-23 08:53:29'),
                ('2025-07-23 14:17:58', '2025-07-23 15:09:43'),
                ('2025-07-24 12:08:15', '2025-07-24 13:51:39'),
                ('2025-07-24 18:26:24', '2025-07-24 19:34:51');";
            connection.Execute(insertQuery);
        }
    }
}
