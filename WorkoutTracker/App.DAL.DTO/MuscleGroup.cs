using Base.Domain;

namespace App.DAL.DTO;

public class MuscleGroup : DomainEntityId
{
    public string MuscleName { get; set; } = default!;

    public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
}