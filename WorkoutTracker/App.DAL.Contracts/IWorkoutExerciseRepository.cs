using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWorkoutExerciseRepository : IBaseRepository<WorkoutExercise>,
    IWorkoutExerciseRepositoryCustom<WorkoutExercise>
{
}

public interface IWorkoutExerciseRepositoryCustom<TEntity>
{
    Task<List<TEntity>> FindAsyncByWorkoutId(Guid id);
    Task<TEntity?> FindAsyncByExerciseAndWorkoutId(Guid workoutId, Guid exerciseId);
}