using App.BLL.Contracts;
using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.BLL;
using Base.Contracts;
using ExerciseMuscle = App.BLL.DTO.ExerciseMuscle;

namespace App.BLL.Services;

public class ExerciseMuscleService : BaseEntityService<App.BLL.DTO.ExerciseMuscle, App.Domain.ExerciseMuscle,
        IExerciseMuscleRepository>,
    IExerciseMuscleService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public ExerciseMuscleService(IAppUnitOfWork appUnitOfWork, IMapper<ExerciseMuscle, Domain.ExerciseMuscle> mapper) :
        base(appUnitOfWork.ExerciseMuscleRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public List<Domain.ExerciseMuscle> AddExerciseMuscles(CreateExercise exercise, Guid newExerciseId)
    {
        return exercise.MuscleGroupIds
            .Select(item => AppUnitOfWork.ExerciseMuscleRepository
                .Add(Mapper
                    .Map(new ExerciseMuscle() {ExerciseId = newExerciseId, MuscleGroupId = item})!))
            .ToList();
    }
}