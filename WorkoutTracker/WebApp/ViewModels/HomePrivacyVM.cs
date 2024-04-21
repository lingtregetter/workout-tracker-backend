using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp.ViewModels;

/// <summary>
/// 
/// </summary>
public class HomePrivacyVM
{
    /// <summary>
    /// 
    /// </summary>
    public AppUser? AppUser { get; set; }
    
    /// <summary>
    /// 
    /// </summary>
    public ICollection<IdentityUserClaim<Guid>>? AppUserClaims { get; set; }
}