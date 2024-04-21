using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWorkoutRepository : IBaseRepository<Workout>, IWorkoutRepositoryCustom<Workout>
{
}

public interface IWorkoutRepositoryCustom<TEntity>
{
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}