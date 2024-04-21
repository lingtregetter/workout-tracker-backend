using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class ExerciseService : BaseEntityService<App.BLL.DTO.Exercise, App.Domain.Exercise, IExerciseRepository>,
    IExerciseService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public ExerciseService( IAppUnitOfWork appUnitOfWork, IMapper<Exercise, Domain.Exercise> mapper) :
        base(appUnitOfWork.ExerciseRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }
}