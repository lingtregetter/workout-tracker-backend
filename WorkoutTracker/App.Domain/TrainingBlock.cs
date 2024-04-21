using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class TrainingBlock : DomainEntityId
{
    [MaxLength(128)]
    public string BlockName { get; set; } = default!;
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid TrainingProgramId { get; set; }
    public TrainingProgram? TrainingProgram { get; set; }
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
    
    public ICollection<Workout>? Workouts { get; set; }
}