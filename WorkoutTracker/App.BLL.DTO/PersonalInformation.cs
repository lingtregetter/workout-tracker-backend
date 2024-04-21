using App.Domain.Identity;
using Base.Domain;

namespace App.BLL.DTO;

public class PersonalInformation : DomainEntityId
{
    public string Gender { get; set; } = default!;
    public decimal Height { get; set; }
    public decimal Weight { get; set; }
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }
}