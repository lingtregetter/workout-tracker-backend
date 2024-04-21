using System.ComponentModel.DataAnnotations;
using Base.Domain;

namespace App.Domain;

public class Exercise : DomainEntityId
{
    [MaxLength(128)]
    public string ExerciseName { get; set; } = default!;
    [MaxLength(256)]
    public string? ExerciseDescription { get; set; }
    
    public ICollection<ExerciseMuscle>? ExerciseMuscles { get; set; }
    public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
}