namespace App.Public.DTO.v1;

public class ExerciseMuscle
{
    public Guid Id { get; set; }
    public Guid? MuscleGroupId { get; set; }
    public string? MuscleGroupName { get; set; }
    public List<MuscleExercise> Exercises { get; set; } = default!;
}