using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface ITrainingBlockService : IBaseRepository<App.BLL.DTO.TrainingBlock>,
    ITrainingBlockRepositoryCustom<App.BLL.DTO.TrainingBlock>
{
}