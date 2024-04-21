namespace App.Public.DTO.v1;

public class TrainingBlock
{
    public Guid Id { get; set; }
    public string BlockName { get; set; } = default!;
    public List<Workout> Workouts { get; set; } = default!;
}