using AutoMapper;
using Base.DAL;
using Rep = App.BLL.DTO.Rep;
using Weight = App.BLL.DTO.Weight;

namespace App.Public.DTO.Mappers;

public class WorkoutSetMapper : BaseMapper<App.BLL.DTO.WorkoutSet, App.Public.DTO.v1.WorkoutSet>
{
    public WorkoutSetMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.BLL.DTO.WorkoutSet MapToBll(App.Public.DTO.v1.CreateWorkoutSet workoutSet)
    {
        var res = new App.BLL.DTO.WorkoutSet()
        {
            Id = workoutSet.Id,
            WorkoutExerciseId = workoutSet.WorkoutExerciseId,
            Reps = new List<Rep>()
                {new Rep() {RepAmount = workoutSet.RepNumber, WorkoutSetId = workoutSet.Id}},
            Weights = new List<Weight>()
                {new Weight(){UsedWeight = workoutSet.UsedWeight, WorkoutSetId = workoutSet.Id}}
        };

        return res;
    }

    public App.BLL.DTO.WorkoutSet MapToBll(App.Public.DTO.v1.WorkoutSet workoutSet)
    {
        var res = new App.BLL.DTO.WorkoutSet()
        {
            Id = workoutSet.Id,
            Reps = new List<Rep>() {new Rep() {RepAmount = workoutSet.RepNumber, WorkoutSetId = workoutSet.Id}},
            Weights = new List<Weight>()
                {new Weight() {UsedWeight = workoutSet.UsedWeight, WorkoutSetId = workoutSet.Id}}
        };

        return res;
    }

    public List<App.Public.DTO.v1.WorkoutSet> MapToPublicList(List<App.BLL.DTO.WorkoutSet> workoutSets)
    {
        var list = new List<App.Public.DTO.v1.WorkoutSet>();
        workoutSets.ForEach(workoutSet =>
        {
            list.Add(new v1.WorkoutSet()
            {
                Id = workoutSet.Id,
                RepNumber = workoutSet.Reps!.First().RepAmount,
                UsedWeight = workoutSet.Weights!.First().UsedWeight
            });
        });

        return list;
    }
}