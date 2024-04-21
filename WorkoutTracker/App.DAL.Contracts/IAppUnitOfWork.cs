using Base.DAL.Contracts;

namespace App.DAL.Contracts;

public interface IAppUnitOfWork : IBaseUnitOfWork
{
    // list your repositories here
    IPersonalInformationRepository PersonalInformationRepository { get; }
    IUserProgramRepository UserProgramRepository { get; }
    ITrainingProgramRepository TrainingProgramRepository { get; }
    ITrainingBlockRepository TrainingBlockRepository { get; }
    IWorkoutRepository WorkoutRepository { get; }
    IMuscleGroupRepository MuscleGroupRepository { get; }
    IExerciseMuscleRepository ExerciseMuscleRepository { get; }
    IExerciseRepository ExerciseRepository { get; }
    IRepRepository RepRepository { get; }
    IWeightRepository WeightRepository { get; }
    IWorkoutSetRepository WorkoutSetRepository { get; }
    IWorkoutExerciseRepository WorkoutExerciseRepository { get; }
}