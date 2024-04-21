using Base.DAL.Contracts;
using UserProgram = App.Domain.UserProgram;

namespace App.DAL.Contracts;

public interface IUserProgramRepository : IBaseRepository<UserProgram>, IUserProgramRepositoryCustom<UserProgram>
{
}

public interface IUserProgramRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    Task<TEntity?> FindAsyncByProgramId(Guid programId, Guid userId);
    Task<TEntity?> RemoveAsyncByProgramId(Guid programId, Guid userId);
}