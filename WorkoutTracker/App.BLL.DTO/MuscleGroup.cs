using Base.Domain;

namespace App.BLL.DTO;

public class MuscleGroup : DomainEntityId
{
    public string MuscleName { get; set; } = default!;
    
    public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
}