using Base.Domain;

namespace App.Domain;

public class WorkoutExercise : DomainEntityId
{
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid WorkoutId { get; set; }
    public Workout? Workout { get; set; }
    
    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }
    
    public ICollection<WorkoutSet>? WorkoutSets { get; set; }
}