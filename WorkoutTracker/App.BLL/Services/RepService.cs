using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class RepService : BaseEntityService<App.BLL.DTO.Rep, App.Domain.Rep, IRepRepository>, IRepService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public RepService(IAppUnitOfWork appUnitOfWork, IMapper<App.BLL.DTO.Rep, App.Domain.Rep> mapper) :
        base(appUnitOfWork.RepRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public void Add(Guid workoutSetId, int repAmount)
    {
        AppUnitOfWork.RepRepository
            .Add(Mapper.Map(new App.BLL.DTO.Rep() {WorkoutSetId = workoutSetId, RepAmount = repAmount})!);
    }

    public async Task<Rep?> FindAsyncBySetId(Guid setId)
    {
        return Mapper.Map(await AppUnitOfWork.RepRepository.FindAsyncBySetId(setId));
    }

    public async Task<Rep?> RemoveAsyncBySetId(Guid setId)
    {
        return Mapper.Map(await AppUnitOfWork.RepRepository.RemoveAsyncBySetId(setId));
    }
}