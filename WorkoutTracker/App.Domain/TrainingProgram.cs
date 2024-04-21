using System.ComponentModel.DataAnnotations;
using App.Domain.Identity;
using Base.Domain;

namespace App.Domain;

public class TrainingProgram : DomainEntityId
{
    [MaxLength(128)]
    public string ProgramName { get; set; } = default!;
    [MaxLength(256)]
    public string? ProgramDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<TrainingBlock>? TrainingBlocks { get; set; }
    public ICollection<UserProgram>? UserPrograms { get; set; }
}