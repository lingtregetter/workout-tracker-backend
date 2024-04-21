using App.BLL.DTO;
using AutoMapper;
using Base.DAL;
using Workout = App.Public.DTO.v1.Workout;

namespace App.Public.DTO.Mappers;

public class TrainingBlockMapper : BaseMapper<App.BLL.DTO.TrainingBlock, App.Public.DTO.v1.TrainingBlock>
{
    public TrainingBlockMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.TrainingBlock? MapToPublic(App.BLL.DTO.TrainingBlock trainingBlock)
    {
        var workouts = new List<Workout>();
        trainingBlock.Workouts?.ToList().ForEach(workout =>
        {
            var dtoWorkout = new Workout()
            {
                Id = workout.Id,
                AvPerformanceTime = workout.AvPerformanceTime,
                WorkoutName = workout.WorkoutName
            };
            workouts.Add(dtoWorkout);
        });

        var res = new App.Public.DTO.v1.TrainingBlock()
        {
            Id = trainingBlock.Id,
            BlockName = trainingBlock.BlockName,
            Workouts = workouts
        };

        return res;
    }

    public List<App.BLL.DTO.TrainingBlock> MapToBll(App.Public.DTO.v1.TrainingBlockWithProgram trainingBlockWithProgram,
        Guid userId)
    {
        var blockList = new List<App.BLL.DTO.TrainingBlock>();

        trainingBlockWithProgram.Blocks.ForEach(block => blockList.Add(new App.BLL.DTO.TrainingBlock()
        {
            Id = trainingBlockWithProgram.Id,
            TrainingProgramId = trainingBlockWithProgram.TrainingProgramId,
            AppUserId = userId,
            BlockName = block
        }));

        return blockList;
    }
}