using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class Workout : DomainEntityId
{
    public string WorkoutName { get; set; } = default!;
    public int AvPerformanceTime { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid TrainingBlockId { get; set; }
    public Domain.TrainingBlock? TrainingBlock { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<Domain.WorkoutExercise>? WorkoutExercises { get; set; }
}