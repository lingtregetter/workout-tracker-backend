namespace App.Public.DTO.v1;

public class CreateWorkoutSet
{
    public Guid Id { get; set; }
    public Guid WorkoutExerciseId { get; set; }
    public decimal UsedWeight { get; set; }
    public int RepNumber { get; set; }
}