using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IWorkoutService : IBaseRepository<App.BLL.DTO.Workout>,
    IWorkoutRepositoryCustom<App.BLL.DTO.Workout>
{
    
}