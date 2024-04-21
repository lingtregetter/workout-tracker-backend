using App.BLL.Contracts;
using App.BLL.Mappers;
using App.BLL.Services;
using App.DAL.Contracts;
using AutoMapper;
using Base.BLL;

namespace App.BLL;

public class AppBLL : BaseBLL<IAppUnitOfWork>, IAppBLL
{
    protected IAppUnitOfWork AppUnitOfWork;
    private readonly AutoMapper.IMapper _mapper;

    public AppBLL(IAppUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
    {
        AppUnitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private IPersonalInformationService? _personalInformationService;

    public IPersonalInformationService PersonalInformationService =>
        _personalInformationService ??=
            new PersonalInformationService(AppUnitOfWork, new PersonalInformationMapper(_mapper));

    private IExerciseMuscleService? _exerciseMuscleService;

    public IExerciseMuscleService ExerciseMuscleService => _exerciseMuscleService ??=
        new ExerciseMuscleService(AppUnitOfWork, new ExerciseMuscleMapper(_mapper));

    private IExerciseService? _exerciseService;

    public IExerciseService ExerciseService => _exerciseService ??=
        new ExerciseService(AppUnitOfWork, new ExerciseMapper(_mapper));

    private IMuscleGroupService? _muscleGroupService;

    public IMuscleGroupService MuscleGroupService => _muscleGroupService ??=
        new MuscleGroupService(AppUnitOfWork, new MuscleGroupMapper(_mapper));

    private IRepService? _repService;

    public IRepService RepService => _repService ??=
        new RepService(AppUnitOfWork, new RepMapper(_mapper));

    private ITrainingBlockService? _trainingBlockService;

    public ITrainingBlockService TrainingBlockService => _trainingBlockService ??=
        new TrainingBlockService(AppUnitOfWork, new TrainingBlockMapper(_mapper));

    private ITrainingProgramService? _trainingProgramService;

    public ITrainingProgramService TrainingProgramService => _trainingProgramService ??=
        new TrainingProgramService(AppUnitOfWork, new TrainingProgramMapper(_mapper));

    private IUserProgramService? _userProgramService;

    public IUserProgramService UserProgramService => _userProgramService ??=
        new UserProgramService(AppUnitOfWork, new UserProgramMapper(_mapper));

    private IWeightService? _weightService;

    public IWeightService WeightService => _weightService ??=
        new WeightService(AppUnitOfWork, new WeightMapper(_mapper));

    private IWorkoutExerciseService? _workoutExerciseService;

    public IWorkoutExerciseService WorkoutExerciseService => _workoutExerciseService ??=
        new WorkoutExerciseService(AppUnitOfWork, new WorkoutExerciseMapper(_mapper));

    private IWorkoutService? _workoutService;

    public IWorkoutService WorkoutService => _workoutService ??=
        new WorkoutService(AppUnitOfWork, new WorkoutMapper(_mapper));

    private IWorkoutSetService? _workoutSetService;

    public IWorkoutSetService WorkoutSetService => _workoutSetService ??=
        new WorkoutSetService(AppUnitOfWork, new WorkoutSetMapper(_mapper));
}