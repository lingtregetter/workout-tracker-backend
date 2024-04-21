using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IMuscleGroupService : IBaseRepository<App.BLL.DTO.MuscleGroup>,
    IMuscleGroupRepositoryCustom<App.BLL.DTO.MuscleGroup>
{
}