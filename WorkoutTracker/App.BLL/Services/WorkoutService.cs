using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class WorkoutService : BaseEntityService<App.BLL.DTO.Workout, App.Domain.Workout, IWorkoutRepository>,
    IWorkoutService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public WorkoutService(IAppUnitOfWork appUnitOfWork, IMapper<App.BLL.DTO.Workout, App.Domain.Workout> mapper):
        base(appUnitOfWork.WorkoutRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await AppUnitOfWork.WorkoutRepository.IsOwnedByUserAsync(id, userId);
    }

    public new Workout Add(Workout entity)
    {
        var workout = AppUnitOfWork.WorkoutRepository.Add(Mapper.Map(entity)!);
        
        foreach (var w in workout.WorkoutExercises!)
        {
            AppUnitOfWork.WorkoutExerciseRepository.Add(w);
        }

        return Mapper.Map(workout)!;
    }
}