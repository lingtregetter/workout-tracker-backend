using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IPersonalInformationRepository : IBaseRepository<PersonalInformation>,
    IPersonalInformationRepositoryCustom<PersonalInformation>
{
}

public interface IPersonalInformationRepositoryCustom<TEntity>
{
    Task<IEnumerable<TEntity>> AllAsync(Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
    Task<TEntity?> RemoveAsync(Guid id, Guid userId);
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
}