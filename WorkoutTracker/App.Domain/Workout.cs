using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class Workout : DomainEntityId
{
    [MaxLength(128)]
    public string WorkoutName { get; set; } = default!;
    public int AvPerformanceTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid TrainingBlockId { get; set; }
    public TrainingBlock? TrainingBlock { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<WorkoutExercise>? WorkoutExercises { get; set; }
}