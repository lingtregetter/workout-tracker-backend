using Swashbuckle.AspNetCore.Annotations;

namespace App.Public.DTO.v1;

public class CreateWorkout
{
    [SwaggerSchema(ReadOnly = true)]
    public Guid Id { get; set; }
    public string WorkoutName { get; set; } = default!;
    public int AvPerformanceTime { get; set; }
    public Guid TrainingBlockId { get; set; }
    public List<Guid> ExerciseIds { get; set; } = default!;
}