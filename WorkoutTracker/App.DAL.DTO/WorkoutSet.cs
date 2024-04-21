using Base.Domain;

namespace App.DAL.DTO;

public class WorkoutSet : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid WorkoutExerciseId { get; set; }
    public WorkoutExercise? WorkoutExercise { get; set; }

    public ICollection<Rep>? Reps { get; set; }
    public ICollection<Weight>? Weights { get; set; }
}