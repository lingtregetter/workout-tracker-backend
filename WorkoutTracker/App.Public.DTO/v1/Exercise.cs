namespace App.Public.DTO.v1;

public class Exercise
{
    public Guid Id { get; set; }
    public string ExerciseName { get; set; } = default!;
    public string? ExerciseDescription { get; set; }
}