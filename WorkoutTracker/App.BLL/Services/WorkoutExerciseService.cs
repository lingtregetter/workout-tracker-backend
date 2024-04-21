using App.BLL.Contracts;
using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.BLL;
using Base.Contracts;
using WorkoutExercise = App.BLL.DTO.WorkoutExercise;

namespace App.BLL.Services;

public class WorkoutExerciseService : BaseEntityService<App.BLL.DTO.WorkoutExercise, App.Domain.WorkoutExercise,
    IWorkoutExerciseRepository>, IWorkoutExerciseService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public WorkoutExerciseService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.WorkoutExercise, App.Domain.WorkoutExercise> mapper) :
        base(appUnitOfWork.WorkoutExerciseRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<List<WorkoutExercise>> FindAsyncByWorkoutId(Guid id)
    {
        return (await AppUnitOfWork.WorkoutExerciseRepository.FindAsyncByWorkoutId(id))
            .Select(e => Mapper.Map(e))
            .ToList()!;
    }

    public async Task<WorkoutExercise?> FindAsyncByExerciseAndWorkoutId(Guid workoutId, Guid exerciseId)
    {
        return Mapper.Map(
            await AppUnitOfWork.WorkoutExerciseRepository.FindAsyncByExerciseAndWorkoutId(workoutId, exerciseId));
    }

    public void AddWorkoutExercises(WorkoutExerciseWithWorkout workoutExercise)
    {
        foreach (var id in workoutExercise.ExerciseIds)
        {
            var findWorkoutExercise = FindAsyncByExerciseAndWorkoutId(workoutExercise.WorkoutId, Guid.Parse(id));
            if (findWorkoutExercise.Result == null)
            {
                AppUnitOfWork.WorkoutExerciseRepository.Add(new Domain.WorkoutExercise()
                {
                    WorkoutId = workoutExercise.WorkoutId,
                    ExerciseId = Guid.Parse(id)
                });
            }
        }
    }
}