using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkoutRepository : EFBaseRepository<Workout, ApplicationDbContext>, IWorkoutRepository
{
    public WorkoutRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<Workout?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(w => w.WorkoutExercises)
            .FirstOrDefaultAsync(w => w.Id == id);
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await RepositoryDbSet.AnyAsync(w => w.Id == id && w.AppUserId == userId);
    }
}