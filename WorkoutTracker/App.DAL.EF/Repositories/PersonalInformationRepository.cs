using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class PersonalInformationRepository : EFBaseRepository<PersonalInformation, ApplicationDbContext>,
    IPersonalInformationRepository
{
    public PersonalInformationRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<PersonalInformation>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(entity => entity.AppUser)
            .OrderBy(entity => entity.Gender)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<PersonalInformation>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(entity => entity.AppUser)
            .OrderBy(entity => entity.Gender)
            .Where(entity => entity.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<PersonalInformation?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(p => p.AppUser)
            .FirstOrDefaultAsync(m => m.AppUserId == id);
    }

    public virtual async Task<PersonalInformation?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(p => p.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }

    public async Task<PersonalInformation?> RemoveAsync(Guid id, Guid userId)
    {
        var personalInformation = await FindAsync(id, userId);
        return personalInformation == null ? null : Remove(personalInformation);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(p => p.Id == id && p.AppUserId == userId);
    }
}