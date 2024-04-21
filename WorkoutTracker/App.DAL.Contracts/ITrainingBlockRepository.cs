using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface ITrainingBlockRepository : IBaseRepository<TrainingBlock>,
    ITrainingBlockRepositoryCustom<TrainingBlock>
{
}

public interface ITrainingBlockRepositoryCustom<TEntity>
{
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}