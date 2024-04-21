using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class MuscleGroup : DomainEntityId
{
    [MaxLength(128)]
    public string MuscleName { get; set; } = default!;
    
    public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
}