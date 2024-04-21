using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IRepRepository : IBaseRepository<Rep>, IRepRepositoryCustom<Rep>
{
}

public interface IRepRepositoryCustom<TEntity>
{
    Task<TEntity?> FindAsyncBySetId(Guid setId);
    Task<TEntity?> RemoveAsyncBySetId(Guid setId);
}