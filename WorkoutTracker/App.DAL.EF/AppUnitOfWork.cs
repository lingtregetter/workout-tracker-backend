using App.DAL.Contracts;
using App.DAL.EF.Repositories;
using Base.DAL.EF;

namespace App.DAL.EF;

public class AppUnitOfWork : EFBaseUnitOfWork<ApplicationDbContext>, IAppUnitOfWork
{
    public AppUnitOfWork(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    private IPersonalInformationRepository? _personalInformationRepository;

    public IPersonalInformationRepository PersonalInformationRepository =>
        _personalInformationRepository ??= new PersonalInformationRepository(UnitOfWorkDbContext);

    private IUserProgramRepository? _userProgramRepository;

    public IUserProgramRepository UserProgramRepository =>
        _userProgramRepository ??= new UserProgramRepository(UnitOfWorkDbContext);

    private ITrainingProgramRepository? _trainingProgramRepository;

    public ITrainingProgramRepository TrainingProgramRepository =>
        _trainingProgramRepository ??= new TrainingProgramRepository(UnitOfWorkDbContext);

    private ITrainingBlockRepository? _trainingBlockRepository;

    public ITrainingBlockRepository TrainingBlockRepository =>
        _trainingBlockRepository ??= new TrainingBlockRepository(UnitOfWorkDbContext);

    private IWorkoutRepository? _workoutRepository;

    public IWorkoutRepository WorkoutRepository =>
        _workoutRepository ??= new WorkoutRepository(UnitOfWorkDbContext);

    private IMuscleGroupRepository? _muscleGroupRepository;

    public IMuscleGroupRepository MuscleGroupRepository =>
        _muscleGroupRepository ??= new MuscleGroupRepository(UnitOfWorkDbContext);

    private IExerciseMuscleRepository? _exerciseMuscleRepository;

    public IExerciseMuscleRepository ExerciseMuscleRepository =>
        _exerciseMuscleRepository ??= new ExerciseMuscleRepository(UnitOfWorkDbContext);

    private IExerciseRepository? _exerciseRepository;

    public IExerciseRepository ExerciseRepository =>
        _exerciseRepository ??= new ExerciseRepository(UnitOfWorkDbContext);

    private IRepRepository? _repRepository;

    public IRepRepository RepRepository =>
        _repRepository ??= new RepRepository(UnitOfWorkDbContext);

    private IWeightRepository? _weightRepository;

    public IWeightRepository WeightRepository =>
        _weightRepository ??= new WeightRepository(UnitOfWorkDbContext);

    private IWorkoutSetRepository? _workoutSetRepository;

    public IWorkoutSetRepository WorkoutSetRepository =>
        _workoutSetRepository ??= new WorkoutSetRepository(UnitOfWorkDbContext);

    private IWorkoutExerciseRepository? _workoutExerciseRepository;

    public IWorkoutExerciseRepository WorkoutExerciseRepository =>
        _workoutExerciseRepository ??= new WorkoutExerciseRepository(UnitOfWorkDbContext);
}