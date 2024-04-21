using App.BLL;
using App.BLL.Contracts;
using App.DAL.EF;
using App.Domain;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Xunit.Abstractions;

namespace Tests.Unit;

public class TrainingProgramUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _ctx;
    private readonly IAppBLL _appBll;

    // predefined id's
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Guid _userId2 = Guid.NewGuid();
    private readonly Guid _programId = Guid.NewGuid();

    public TrainingProgramUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;

        // set up mock database - in memory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);

        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        // mapper conf
        var testMapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<App.BLL.DTO.TrainingProgram, App.Public.DTO.v1.TrainingProgram>().ReverseMap();
                config.CreateMap<App.BLL.DTO.TrainingProgram, App.Domain.TrainingProgram>().ReverseMap();
                config.CreateMap<App.BLL.DTO.UserProgram, App.Public.DTO.v1.UserProgram>().ReverseMap();
                config.CreateMap<App.BLL.DTO.UserProgram, App.Domain.UserProgram>().ReverseMap();
                config.CreateMap<App.BLL.DTO.TrainingBlock, App.Public.DTO.v1.TrainingBlock>().ReverseMap();
                config.CreateMap<App.BLL.DTO.TrainingBlock, App.Domain.TrainingBlock>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Workout, App.Public.DTO.v1.Workout>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Workout, App.Domain.Workout>().ReverseMap();
                config.CreateMap<App.BLL.DTO.WorkoutExercise, App.Public.DTO.v1.WorkoutExercise>().ReverseMap();
                config.CreateMap<App.BLL.DTO.WorkoutExercise, App.Domain.WorkoutExercise>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Exercise, App.Public.DTO.v1.Exercise>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Exercise, App.Domain.Exercise>().ReverseMap();
                config.CreateMap<App.BLL.DTO.WorkoutSet, App.Public.DTO.v1.WorkoutSet>().ReverseMap();
                config.CreateMap<App.BLL.DTO.WorkoutSet, App.Domain.WorkoutSet>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Rep, App.Public.DTO.v1.Rep>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Rep, App.Domain.Rep>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Weight, App.Public.DTO.v1.Weight>().ReverseMap();
                config.CreateMap<App.BLL.DTO.Weight, App.Domain.Weight>().ReverseMap();
            }
        );

        _appBll = new AppBLL(new AppUnitOfWork(_ctx), new Mapper(testMapperConfiguration));
    }

    [Fact]
    public async Task TestGetUserTrainingPrograms()
    {
        SeedDataToTestTrainingProgramAsync();

        var result = await _appBll.UserProgramService.AllAsync(_userId);

        foreach (var userProgram in result)
        {
            Assert.Equal(_userId, userProgram.AppUserId);
        }

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task TestGetTrainingProgramByIdAndCheckIfIsOwnedByUser()
    {
        SeedDataToTestTrainingProgramAsync();

        var ownerShip =
            _appBll.TrainingProgramService.IsOwnedByUserAsync(_programId, _userId)
                .Result;

        Assert.True(ownerShip);

        var ownerShip2 =
            _appBll.TrainingProgramService.IsOwnedByUserAsync(_programId, Guid.NewGuid()).Result;

        Assert.False(ownerShip2);

        var result = await _appBll.TrainingProgramService.FindAsync(_programId);

        Assert.NotNull(result);
        Assert.Equal(_userId, result.AppUserId);
        Assert.Equal(_programId, result.Id);
        Assert.Equal("Test name 1", result.ProgramName);
        Assert.Equal("Test 1 description", result.ProgramDescription);
        Assert.Equal(1, result.TrainingBlocks!.Count);
    }

    [Fact]
    public async Task TestDeleteTrainingProgram()
    {
        SeedDataToTestTrainingProgramAsync();

        var ownerShip =
            _appBll.TrainingProgramService.IsOwnedByUserAsync(_programId, _userId)
                .Result;

        Assert.True(ownerShip);

        var findTrainingProgramWithWrongUser = await _appBll.TrainingProgramService.FindAsync(_programId, _userId2);

        Assert.Null(findTrainingProgramWithWrongUser);

        var result = await _appBll.TrainingProgramService.FindAsync(_programId, _userId);

        Assert.NotNull(result);

        await _appBll.TrainingProgramService.RemoveAsync(result.Id);
        await _appBll.SaveChangesAsync();

        Assert.Null(_ctx.TrainingPrograms.FirstOrDefault(e => e.Id == _programId));
    }

    [Fact]
    public async Task TestAddTrainingProgramToUser()
    {
        SeedDataToTestTrainingProgramAsync();

        var userPrograms = await _appBll.UserProgramService.AllAsync(_userId);

        Assert.Equal(2, userPrograms.Count());

        var id = Guid.NewGuid();

        var newProgram = new App.BLL.DTO.TrainingProgram()
        {
            Id = id,
            AppUserId = _userId,
            ProgramName = "New program",
            ProgramDescription = "New description",
            CreatedAt = DateTime.UtcNow,
            TrainingBlocks = new[]
            {
                new App.BLL.DTO.TrainingBlock()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = _userId,
                    TrainingProgramId = id,
                    BlockName = "New block",
                    CreatedAt = DateTime.UtcNow
                }
            },
            UserPrograms = new[]
            {
                new App.BLL.DTO.UserProgram()
                {
                    Id = Guid.NewGuid(),
                    AppUserId = _userId,
                    TrainingProgramId = id
                }
            }
        };

        _appBll.TrainingProgramService.Add(newProgram);
        await _appBll.SaveChangesAsync();

        var newUserPrograms = await _appBll.UserProgramService.AllAsync(_userId);
        Assert.Equal(3, newUserPrograms.Count());

        var findNewProgram = await _appBll.TrainingProgramService.FindAsync(id);
        
        Assert.NotNull(findNewProgram);
        Assert.Equal(newProgram.ProgramName, findNewProgram.ProgramName);
        Assert.Equal(newProgram.ProgramDescription, findNewProgram.ProgramDescription);
        Assert.Equal(newProgram.TrainingBlocks.Count, findNewProgram.TrainingBlocks!.Count);
    }

    private async void SeedDataToTestTrainingProgramAsync()
    {
        var testUserProgramId1 = Guid.NewGuid();
        var testUserProgramId2 = Guid.NewGuid();
        var testUserProgramId3 = Guid.NewGuid();
        var testTrainingProgramId2 = Guid.NewGuid();
        var testTrainingProgramId3 = Guid.NewGuid();
        var testTrainingBlockId = Guid.NewGuid();
        var testWorkoutId = Guid.NewGuid();
        var testWorkoutExerciseId = Guid.NewGuid();
        var testExerciseId = Guid.NewGuid();
        var testWorkoutSetId = Guid.NewGuid();

        _ctx.Users.Add(new AppUser()
        {
            Id = _userId,
            Email = "test@app.com",
            UserName = "test@app.com",
            FirstName = "Test",
            LastName = "App"
        });

        _ctx.Users.Add(new AppUser()
        {
            Id = _userId2,
            Email = "test2@app.com",
            UserName = "test2@app.com",
            FirstName = "Test2",
            LastName = "App2"
        });

        _ctx.Exercises.Add(new Exercise()
        {
            Id = testExerciseId,
            ExerciseName = "Test exercise",
            ExerciseDescription = "Test exercise description",
        });

        var newWorkoutSet = new WorkoutSet()
        {
            Id = testWorkoutSetId,
            CreatedAt = DateTime.UtcNow,
            WorkoutExerciseId = testWorkoutExerciseId,
            Reps = new[]
            {
                new Rep()
                {
                    Id = Guid.NewGuid(),
                    WorkoutSetId = testWorkoutSetId,
                    RepAmount = 12
                }
            },
            Weights = new[]
            {
                new Weight()
                {
                    Id = Guid.NewGuid(),
                    WorkoutSetId = testWorkoutSetId,
                    UsedWeight = 20
                }
            }
        };

        var newWorkoutExercise = new WorkoutExercise()
        {
            Id = testWorkoutExerciseId,
            WorkoutId = testWorkoutId,
            ExerciseId = testExerciseId,
            WorkoutSets = new[]
            {
                newWorkoutSet
            }
        };

        var newWorkout = new Workout()
        {
            Id = testWorkoutId,
            WorkoutName = "Test workout name",
            AvPerformanceTime = 60,
            TrainingBlockId = testTrainingBlockId,
            AppUserId = _userId,
            CreatedAt = DateTime.UtcNow,
            WorkoutExercises = new[]
            {
                newWorkoutExercise
            }
        };

        _ctx.TrainingPrograms.Add(new TrainingProgram()
        {
            Id = _programId,
            AppUserId = _userId,
            ProgramName = "Test name 1",
            ProgramDescription = "Test 1 description",
            CreatedAt = DateTime.UtcNow,
            TrainingBlocks = new[]
            {
                new TrainingBlock()
                {
                    Id = testTrainingBlockId,
                    BlockName = "Test block name",
                    AppUserId = _userId,
                    CreatedAt = DateTime.UtcNow,
                    TrainingProgramId = _programId,
                    Workouts = new[]
                    {
                        newWorkout
                    }
                }
            }
        });

        _ctx.TrainingPrograms.Add(new TrainingProgram()
        {
            Id = testTrainingProgramId2,
            AppUserId = _userId,
            ProgramName = "Test name 2",
            ProgramDescription = "Test 2 description",
            CreatedAt = DateTime.UtcNow
        });

        _ctx.TrainingPrograms.Add(new TrainingProgram()
        {
            Id = testTrainingProgramId3,
            AppUserId = _userId2,
            ProgramName = "Test name 3",
            ProgramDescription = "Test 3 description",
            CreatedAt = DateTime.UtcNow
        });

        _ctx.UserPrograms.Add(new UserProgram()
        {
            AppUserId = _userId,
            Id = testUserProgramId1,
            TrainingProgramId = _programId
        });

        _ctx.UserPrograms.Add(new UserProgram()
        {
            AppUserId = _userId,
            Id = testUserProgramId2,
            TrainingProgramId = testTrainingProgramId2
        });

        _ctx.UserPrograms.Add(new UserProgram()
        {
            AppUserId = _userId2,
            Id = testUserProgramId3,
            TrainingProgramId = testTrainingProgramId3
        });

        await _ctx.SaveChangesAsync();
    }
}