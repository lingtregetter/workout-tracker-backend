using Base.BLL.Contracts;

namespace App.BLL.Contracts;

public interface IAppBLL : IBaseBLL
{
    IPersonalInformationService PersonalInformationService { get; }
    IExerciseMuscleService ExerciseMuscleService { get; }
    IExerciseService ExerciseService { get; }
    IMuscleGroupService MuscleGroupService { get; }
    IRepService RepService { get; }
    ITrainingBlockService TrainingBlockService { get; }
    ITrainingProgramService TrainingProgramService { get; }
    IUserProgramService UserProgramService { get; }
    IWeightService WeightService { get; }
    IWorkoutExerciseService WorkoutExerciseService { get; }
    IWorkoutService WorkoutService { get; }
    IWorkoutSetService WorkoutSetService { get; }
}