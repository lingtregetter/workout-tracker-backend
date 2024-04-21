using App.BLL.DTO;
using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IWorkoutSetService : IBaseRepository<App.BLL.DTO.WorkoutSet>,
    IWorkoutSetRepositoryCustom<App.BLL.DTO.WorkoutSet>
{
    void UpdateSet(WorkoutSet workoutSet);
    new void Add(WorkoutSet entity);
}