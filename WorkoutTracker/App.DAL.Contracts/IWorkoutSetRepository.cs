using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWorkoutSetRepository : IBaseRepository<WorkoutSet>, IWorkoutSetRepositoryCustom<WorkoutSet>
{
}

public interface IWorkoutSetRepositoryCustom<TEntity>
{
    Task<List<TEntity>> AllAsync(Guid workoutExerciseId);
}