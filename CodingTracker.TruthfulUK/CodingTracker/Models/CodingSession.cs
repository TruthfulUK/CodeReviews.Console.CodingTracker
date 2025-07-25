﻿namespace CodingTracker.Models;
internal class CodingSession
{
    public int Id { get; set; }
    public required DateTime StartTime { get; set; }
    public required DateTime EndTime { get; set; }
    public TimeSpan Duration => EndTime - StartTime;
}
