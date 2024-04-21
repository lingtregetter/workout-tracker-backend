using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IExerciseService : IBaseRepository<App.BLL.DTO.Exercise>,
    IExerciseRepositoryCustom<App.BLL.DTO.Exercise>
{
    
}