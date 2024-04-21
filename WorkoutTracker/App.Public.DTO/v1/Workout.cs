namespace App.Public.DTO.v1;

public class Workout
{
    public Guid Id { get; set; }
    public string WorkoutName { get; set; } = default!;
    public int AvPerformanceTime { get; set; }
}