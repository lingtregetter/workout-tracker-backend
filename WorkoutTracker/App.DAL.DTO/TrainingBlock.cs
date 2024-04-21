using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class TrainingBlock : DomainEntityId
{
    public string BlockName { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid TrainingProgramId { get; set; }
    public TrainingProgram? TrainingProgram { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<Workout>? Workouts { get; set; }
}