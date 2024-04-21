using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IExerciseRepository : IBaseRepository<Exercise>,
    IExerciseRepositoryCustom<Exercise>
{
}

public interface IExerciseRepositoryCustom<TEntity>{}