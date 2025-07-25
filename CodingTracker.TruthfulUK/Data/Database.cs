using System.Data.SQLite;
using System.Configuration;
using Dapper;

namespace CodingTracker.TruthfulUK.Data;
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
            Console.WriteLine(connection.FileName);
        }
    }
}
