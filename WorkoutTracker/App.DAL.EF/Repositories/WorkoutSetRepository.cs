using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkoutSetRepository : EFBaseRepository<WorkoutSet, ApplicationDbContext>, IWorkoutSetRepository
{
    public WorkoutSetRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
    
    public virtual async Task<List<WorkoutSet>> AllAsync(Guid workoutExerciseId)
    {
        return await RepositoryDbSet
            .Include(e => e.Reps)
            .Include(e => e.Weights)
            .Where(e => e.WorkoutExerciseId.Equals(workoutExerciseId))
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();
    }
}