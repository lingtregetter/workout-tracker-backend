using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class RepRepository : EFBaseRepository<Rep, ApplicationDbContext>, IRepRepository
{
    public RepRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<Rep?> FindAsyncBySetId(Guid setId)
    {
        return await RepositoryDbSet.Include(r => r.WorkoutSet)
            .FirstOrDefaultAsync(r => r.WorkoutSetId == setId);
    }
    
    public async Task<Rep?> RemoveAsyncBySetId(Guid setId)
    {
        var rep = await FindAsyncBySetId(setId);
        return rep == null ? null : Remove(rep);
    }
}