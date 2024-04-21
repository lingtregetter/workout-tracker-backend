using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IMuscleGroupRepository : IBaseRepository<MuscleGroup>,
    IMuscleGroupRepositoryCustom<MuscleGroup>
{
}

public interface IMuscleGroupRepositoryCustom<TEntity>
{
}