using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class WeightService : BaseEntityService<App.BLL.DTO.Weight, App.Domain.Weight, IWeightRepository>,
    IWeightService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public WeightService(IAppUnitOfWork appUnitOfWork, IMapper<App.BLL.DTO.Weight, App.Domain.Weight> mapper) :
        base(appUnitOfWork.WeightRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public void Add(Guid workoutSetId, decimal usedWeight)
    {
        AppUnitOfWork.WeightRepository
            .Add(Mapper.Map(new App.BLL.DTO.Weight() {WorkoutSetId = workoutSetId, UsedWeight = usedWeight})!);
    }

    public async Task<Weight?> FindAsyncBySetId(Guid setId)
    {
        return Mapper.Map(await AppUnitOfWork.WeightRepository.FindAsyncBySetId(setId));
    }

    public async Task<Weight?> RemoveAsyncBySetId(Guid setId)
    {
        return Mapper.Map(await AppUnitOfWork.WeightRepository.RemoveAsyncBySetId(setId));
    }
}