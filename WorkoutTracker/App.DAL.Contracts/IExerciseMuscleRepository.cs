using App.Domain;
using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IExerciseMuscleRepository : IBaseRepository<ExerciseMuscle>,
    IExerciseMuscleRepositoryCustom<ExerciseMuscle>
{
}

public interface IExerciseMuscleRepositoryCustom<TEntity>
{
}