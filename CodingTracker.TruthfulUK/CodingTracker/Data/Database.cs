using CodingTracker.Models;
using Dapper;
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
                    StartTime DATETIME,
                    EndTime DATETIME
                );
                CREATE TABLE IF NOT EXISTS Goals (
                    Id INTEGER PRIMARY KEY AUTOINCREMENT,
                    GoalStartDate DATETIME,
                    GoalEndDate DATETIME,
                    GoalMinutes INTEGER
                );";
            connection.Execute(createTableQuery);
        }
    }

    public static List<CodingSession> FetchSessions(int limit = 0, int offset = 0, string period = "", int periodRange = 0)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var selectQuery = "";
            var parameters = new { Limit = limit, Offset = offset, Period = period, PeriodRange = periodRange };
            
            if (string.IsNullOrEmpty(period))
            {
                selectQuery = @"
                SELECT * From CodingSessions
                ORDER by Id DESC
                LIMIT @Limit OFFSET @Offset";
            } else
            {
                selectQuery = @"
                SELECT * From CodingSessions
                WHERE StartTime >= datetime('now', printf('-%d %s', @PeriodRange, @Period))
                ORDER by Id DESC
                LIMIT @Limit OFFSET @Offset";
            }

            return connection.Query<CodingSession>(selectQuery, parameters).ToList();
        }
    }

    public static List<CodingSession> FetchSessionsBetweenDates(DateTime start, DateTime end)
    {
        using (var connection = GetConnection())
        {
            connection.Open();

            var parameters = new { StartDate = start, EndDate = end };
            var selectQuery = @"
                SELECT * From CodingSessions
                WHERE StartTime >= @StartDate AND EndTime <= @EndDate
                ";

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

    public static void UpdateSession(int rowId, string startTime, string endTime)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var parameters = new { RowId = rowId, StartTime = startTime, EndTime = endTime };
            var updateRow = @"
                UPDATE CodingSessions 
                SET StartTime = @StartTime, EndTime = @EndTime
                WHERE Id = @RowId";
            connection.Execute(updateRow, parameters);
        }
    }

    public static void DeleteSession(int rowId)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var parameters = new { RowId = rowId };
            var deleteRow = @"
                DELETE FROM CodingSessions
                WHERE Id = @RowId";
            connection.Execute(deleteRow, parameters);
        }
    }

    public static void CreateGoal(string goalStart, string goalEnd, int goalMinutes)
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var parameters = new { GoalStartDate = goalStart, GoalEndDate = goalEnd, GoalMinutes = goalMinutes };
            var insertGoal = @"
            INSERT INTO Goals (GoalStartDate, GoalEndDate, GoalMinutes)
            VALUES (@GoalStartDate, @GoalEndDate, @GoalMinutes)";
            connection.Execute(insertGoal, parameters);
        }
    }

    public static List<Goal> FetchGoals()
    {
        using (var connection = GetConnection())
        {
            connection.Open();
            var selectQuery = @"
            SELECT * FROM Goals
            ORDER by Id ASC";

            return connection.Query<Goal>(selectQuery).ToList();
        }
    }
}
