namespace App.Public.DTO.v1;

public class WorkoutExerciseWithWorkout
{
    public Guid Id { get; set; }
    public Guid WorkoutId { get; set; }
    public List<string> ExerciseIds { get; set; } = default!;
}