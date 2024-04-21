using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IWeightRepository : IBaseRepository<Weight>, IWeightRepositoryCustom<Weight>
{
}

public interface IWeightRepositoryCustom<TEntity>
{
    Task<TEntity?> FindAsyncBySetId(Guid setId);
    Task<TEntity?> RemoveAsyncBySetId(Guid setId);
}