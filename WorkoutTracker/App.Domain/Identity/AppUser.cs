using System.ComponentModel.DataAnnotations;
using Base.Domain.Contracts;
using Microsoft.AspNetCore.Identity;

namespace App.Domain.Identity;

public class AppUser : IdentityUser<Guid>, IDomainEntityId
{
    [MaxLength(128)]
    public string FirstName { get; set; } = default!;
    
    [MaxLength(128)]
    public string LastName { get; set; } = default!;

    public ICollection<UserProgram>? UserPrograms { get; set; }
    public ICollection<PersonalInformation>? PersonalInformations { get; set; }
    public ICollection<TrainingProgram>? TrainingPrograms { get; set; }
    public ICollection<TrainingBlock>? TrainingBlocks { get; set; }
    public ICollection<Workout>? Workouts { get; set; }
    public ICollection<AppRefreshToken>? AppRefreshTokens { get; set; }
}