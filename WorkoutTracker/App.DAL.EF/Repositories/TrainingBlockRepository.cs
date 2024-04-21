using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class TrainingBlockRepository : EFBaseRepository<TrainingBlock, ApplicationDbContext>, ITrainingBlockRepository
{
    public TrainingBlockRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<TrainingBlock?> FindAsync(Guid blockId)
    {
        return await RepositoryDbSet
            .Include(e => e.Workouts)
            .FirstOrDefaultAsync(e => e.Id.Equals(blockId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(t => t.Id == id && t.AppUserId == userId);
    }
}