using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WeightRepository : EFBaseRepository<Weight, ApplicationDbContext>, IWeightRepository
{
    public WeightRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public async Task<Weight?> FindAsyncBySetId(Guid setId)
    {
        return await RepositoryDbSet.Include(w => w.WorkoutSet)
            .FirstOrDefaultAsync(w => w.WorkoutSetId == setId);
    }
    
    public async Task<Weight?> RemoveAsyncBySetId(Guid setId)
    {
        var weight = await FindAsyncBySetId(setId);
        return weight == null ? null : Remove(weight);
    }
}