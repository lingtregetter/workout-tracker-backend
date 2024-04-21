namespace App.Public.DTO.v1;

public class WorkoutExerciseDetails
{
    public Guid Id { get; set; }
    public Guid WorkoutExerciseId { get; set; }
    public string ExerciseName { get; set; } = default!;
    public string? ExerciseDescription { get; set; }
}