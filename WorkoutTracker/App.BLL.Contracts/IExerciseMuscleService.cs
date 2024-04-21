using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IExerciseMuscleService : IBaseRepository<App.BLL.DTO.ExerciseMuscle>,
    IExerciseMuscleRepositoryCustom<App.BLL.DTO.ExerciseMuscle>
{
    List<Domain.ExerciseMuscle> AddExerciseMuscles(CreateExercise exercise, Guid newExerciseId);
}