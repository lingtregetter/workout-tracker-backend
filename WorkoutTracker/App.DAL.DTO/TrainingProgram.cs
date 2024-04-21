using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class TrainingProgram : DomainEntityId
{
    public string ProgramName { get; set; } = default!;
    public string? ProgramDescription { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public ICollection<TrainingBlock>? TrainingBlocks { get; set; }
    public ICollection<UserProgram>? UserPrograms { get; set; }
}