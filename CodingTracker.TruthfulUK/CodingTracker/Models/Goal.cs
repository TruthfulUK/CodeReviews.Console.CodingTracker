namespace CodingTracker.Models;
internal class Goal
{
    public int Id { get; set; }

    public DateTime GoalStartDate { get; set; }

    public DateTime GoalEndDate { get; set; }

    public int GoalMinutes { get; set; }
    
}
