using CodingTracker;
using CodingTracker.Data;

if (!File.Exists("ApplicationData.db"))
{
    Database.InitializeDatabase();
    SeedData.Initialize();
}

UserInterface.DisplayMainMenu();