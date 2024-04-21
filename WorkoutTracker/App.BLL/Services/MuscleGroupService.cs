using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.BLL;
using Base.Contracts;
using MuscleGroup = App.BLL.DTO.MuscleGroup;

namespace App.BLL.Services;

public class MuscleGroupService :
    BaseEntityService<App.BLL.DTO.MuscleGroup, App.Domain.MuscleGroup, IMuscleGroupRepository>,
    IMuscleGroupService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public MuscleGroupService(IAppUnitOfWork appUnitOfWork, IMapper<MuscleGroup, Domain.MuscleGroup> mapper) :
        base(appUnitOfWork.MuscleGroupRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }
}