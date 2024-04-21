using App.Domain.Identity;
using Base.Domain;

namespace App.DAL.DTO;

public class UserProgram : DomainEntityId
{
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid TrainingProgramId { get; set; }
    public TrainingProgram? TrainingProgram { get; set; }
}