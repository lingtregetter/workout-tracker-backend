using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IWorkoutExerciseService : IBaseRepository<App.BLL.DTO.WorkoutExercise>,
    IWorkoutExerciseRepositoryCustom<App.BLL.DTO.WorkoutExercise>
{
    void AddWorkoutExercises(WorkoutExerciseWithWorkout workoutExercise);
}