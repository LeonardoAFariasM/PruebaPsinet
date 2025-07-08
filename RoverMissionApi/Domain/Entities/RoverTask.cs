// Implementamos la entidad principal
namespace Domain.Entities;

public class RoverTask
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public required string RoverName { get; set; }
    public TaskType TaskType { get; set; }
    public double Latitude { get; set; }
    public double Longitude { get; set; }
    public DateTime StartsAt { get; set; }
    public int DurationMinutes { get; set; }
    public Status Status { get; set; } = Status.Planned;
}

public enum TaskType { Drill, Sample, Photo, Charge }
public enum Status { Planned, InProgress, Completed, Aborted }

