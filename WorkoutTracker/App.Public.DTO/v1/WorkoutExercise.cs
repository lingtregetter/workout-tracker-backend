namespace App.Public.DTO.v1;

public class WorkoutExercise
{
    public Guid Id { get; set; }
    public string WorkoutName { get; set; } = default!;
    public List<WorkoutExerciseDetails> Exercises { get; set; } = default!;
}