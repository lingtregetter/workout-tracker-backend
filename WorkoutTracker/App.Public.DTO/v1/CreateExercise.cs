namespace App.Public.DTO.v1;

public class CreateExercise
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; } = default!;
    public string? ExerciseDescription { get; set; }
    public List<Guid> MuscleGroupIds { get; set; } = default!;
}