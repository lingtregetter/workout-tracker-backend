using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface ITrainingProgramRepository : IBaseRepository<TrainingProgram>,
    ITrainingProgramRepositoryCustom<TrainingProgram>
{
}

public interface ITrainingProgramRepositoryCustom<TEntity>
{
    Task<bool> IsOwnedByUserAsync(Guid id, Guid userId);
    Task<TEntity?> FindAsync(Guid id, Guid userId);
}