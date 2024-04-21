using App.BLL.Contracts;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class TrainingBlockService :
    BaseEntityService<App.BLL.DTO.TrainingBlock, App.Domain.TrainingBlock, ITrainingBlockRepository>,
    ITrainingBlockService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public TrainingBlockService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.TrainingBlock, App.Domain.TrainingBlock> mapper) :
        base(appUnitOfWork.TrainingBlockRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await AppUnitOfWork.TrainingBlockRepository.IsOwnedByUserAsync(id, userId);
    }
}