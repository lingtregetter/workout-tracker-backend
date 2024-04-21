using Base.Domain;

namespace App.Domain;

public class ExerciseMuscle : DomainEntityId
{
    public Guid ExerciseId { get; set; }
    public Exercise? Exercise { get; set; }
    
    public Guid MuscleGroupId { get; set; }
    public MuscleGroup? MuscleGroup { get; set; }
}