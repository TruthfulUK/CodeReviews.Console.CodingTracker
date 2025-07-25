using CodingTracker.TruthfulUK;
using CodingTracker.TruthfulUK.Data;

if (!File.Exists("ApplicationData.db"))
{
    File.Delete("ApplicationData.db");
}

Database.InitializeDatabase();
UserInterface.DisplayNavigation();

Console.ReadKey();