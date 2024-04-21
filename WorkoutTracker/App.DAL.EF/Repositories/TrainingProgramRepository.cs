using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrainingProgramRepository : EFBaseRepository<TrainingProgram, ApplicationDbContext>,
    ITrainingProgramRepository
{
    public TrainingProgramRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<TrainingProgram>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(entity => entity.TrainingBlocks)
            .Include(entity => entity.UserPrograms)
            .ToListAsync();
    }

    public override async Task<TrainingProgram?> FindAsync(Guid programId)
    {
        return await RepositoryDbSet
            .Include(e => e.TrainingBlocks)
            .FirstOrDefaultAsync(e => e.Id.Equals(programId));
    }
    
    public virtual async Task<TrainingProgram?> FindAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet
            .Include(t => t.AppUser)
            .FirstOrDefaultAsync(t => t.Id == id && t.AppUserId == userId);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AppUserId == userId);
    }
}