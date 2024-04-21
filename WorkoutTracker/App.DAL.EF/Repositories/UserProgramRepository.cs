using App.DAL.Contracts;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using UserProgram = App.Domain.UserProgram;

namespace App.DAL.EF.Repositories;

public class UserProgramRepository : EFBaseRepository<UserProgram, ApplicationDbContext>, IUserProgramRepository
{
    public UserProgramRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<UserProgram>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(entity => entity.AppUser)
            .Include(entity => entity.TrainingProgram)
            .OrderBy(entity => entity.TrainingProgram)
            .ToListAsync();
    }

    public virtual async Task<IEnumerable<UserProgram>> AllAsync(Guid userId)
    {
        return await RepositoryDbSet
            .Include(entity => entity.AppUser)
            .Include(entity => entity.TrainingProgram)
            .OrderBy(entity => entity.TrainingProgram!.CreatedAt)
            .Where(entity => entity.AppUserId == userId)
            .ToListAsync();
    }

    public override async Task<UserProgram?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(u => u.AppUser)
            .FirstOrDefaultAsync(m => m.Id == id);
    }

    public virtual async Task<UserProgram?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(u => u.AppUser)
            .Include(u => u.TrainingProgram)
            .FirstOrDefaultAsync(m => m.Id == id && m.AppUserId == userId);
    }

    public async Task<UserProgram?> RemoveAsync(Guid id, Guid userId)
    {
        var userProgram = await FindAsync(id, userId);
        return userProgram == null ? null : Remove(userProgram);
    }

    public async Task<UserProgram?> FindAsyncByProgramId(Guid programId, Guid userId)
    {
        return await RepositoryDbSet
            .Include(u => u.TrainingProgram)
            .FirstOrDefaultAsync(u => u.TrainingProgramId == programId && u.AppUserId == userId);
    }

    public async Task<UserProgram?> RemoveAsyncByProgramId(Guid programId, Guid userId)
    {
        var userProgram = await FindAsyncByProgramId(programId, userId);
        return userProgram == null ? null : Remove(userProgram);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(u => u.TrainingProgramId == id && u.AppUserId == userId);
    }
}