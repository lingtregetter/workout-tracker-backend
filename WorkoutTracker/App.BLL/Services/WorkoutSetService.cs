using App.BLL.Contracts;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;
using WorkoutSet = App.BLL.DTO.WorkoutSet;

namespace App.BLL.Services;

public class WorkoutSetService :
    BaseEntityService<App.BLL.DTO.WorkoutSet, App.Domain.WorkoutSet, IWorkoutSetRepository>,
    IWorkoutSetService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public WorkoutSetService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.WorkoutSet, App.Domain.WorkoutSet> mapper) :
        base(appUnitOfWork.WorkoutSetRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<List<App.BLL.DTO.WorkoutSet>> AllAsync(Guid workoutExerciseId)
    {
        return (await AppUnitOfWork.WorkoutSetRepository.AllAsync(workoutExerciseId))
            .Select(e => Mapper.Map(e))
            .ToList()!;
    }

    public void UpdateSet(WorkoutSet workoutSet)
    {
        var rep = workoutSet.Reps!.FirstOrDefault();
        var weight = workoutSet.Weights!.FirstOrDefault();

        var repFromDb = AppUnitOfWork.RepRepository.FindAsyncBySetId(workoutSet.Id);
        repFromDb.Result!.RepAmount = rep!.RepAmount;
        AppUnitOfWork.RepRepository.Update(repFromDb.Result);

        var weightFromDb = AppUnitOfWork.WeightRepository.FindAsyncBySetId(workoutSet.Id);
        weightFromDb.Result!.UsedWeight = weight!.UsedWeight;
        AppUnitOfWork.WeightRepository.Update(weightFromDb.Result);
    }

    public new void Add(WorkoutSet entity)
    {
        var workoutSet = AppUnitOfWork.WorkoutSetRepository.Add(Mapper.Map(entity)!);
        if (workoutSet.Reps != null) AppUnitOfWork.RepRepository.Add(workoutSet.Reps.FirstOrDefault()!);
        if (workoutSet.Weights != null) AppUnitOfWork.WeightRepository.Add(workoutSet.Weights.FirstOrDefault()!);
    }
}