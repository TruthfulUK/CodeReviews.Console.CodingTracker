using CodingTracker;
using CodingTracker.Data;

if (File.Exists("ApplicationData.db"))
{
    File.Delete("ApplicationData.db");
    Database.InitializeDatabase();
    Database.SeedDatabase();
}

UserInterface.DisplayMainMenu();

Console.ReadKey();