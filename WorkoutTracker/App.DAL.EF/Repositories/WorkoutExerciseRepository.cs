using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Repositories;

public class WorkoutExerciseRepository : EFBaseRepository<WorkoutExercise, ApplicationDbContext>,
    IWorkoutExerciseRepository
{
    public WorkoutExerciseRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public async Task<List<WorkoutExercise>> FindAsyncByWorkoutId(Guid id)
    {
        return await RepositoryDbSet
            .Include(e => e.Exercise)
            .Include(e => e.Workout)
            .Where(e => e.WorkoutId.Equals(id))
            .OrderBy(e => e.CreatedAt)
            .ToListAsync();
    }

    public async Task<WorkoutExercise?> FindAsyncByExerciseAndWorkoutId(Guid workoutId, Guid exerciseId)
    {
        return await RepositoryDbSet
            .Include(e => e.Exercise)
            .Include(e => e.Workout)
            .Where(e => e.WorkoutId.Equals(workoutId) && e.ExerciseId.Equals(exerciseId))
            .FirstOrDefaultAsync();
    }
    
    public override async Task<WorkoutExercise?> FindAsync(Guid id)
    {
        return await RepositoryDbSet
            .Include(w => w.WorkoutSets)
            .FirstOrDefaultAsync(w => w.Id == id);
    }
}