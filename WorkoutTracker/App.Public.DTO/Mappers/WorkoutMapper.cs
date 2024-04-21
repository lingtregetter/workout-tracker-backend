using App.Domain;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class WorkoutMapper : BaseMapper<App.BLL.DTO.Workout, App.Public.DTO.v1.Workout>
{
    public WorkoutMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.Workout MapToPublic(App.BLL.DTO.Workout workout)
    {
        var res = new App.Public.DTO.v1.Workout()
        {
            Id = workout.Id,
            AvPerformanceTime = workout.AvPerformanceTime,
            WorkoutName = workout.WorkoutName
        };
        return res;
    }

    public App.BLL.DTO.Workout CreateMapToBll(App.Public.DTO.v1.CreateWorkout workout, Guid userId)
    {
        var exercises = new List<App.BLL.DTO.WorkoutExercise>();
        
        workout.ExerciseIds.ForEach(id =>
        {
            var workoutExercise = new App.BLL.DTO.WorkoutExercise()
            {
                WorkoutId = workout.Id,
                ExerciseId = id
            };
            
            exercises.Add(workoutExercise);
        });
        
        var res = new App.BLL.DTO.Workout()
        {
            Id = workout.Id,
            AvPerformanceTime = workout.AvPerformanceTime,
            TrainingBlockId = workout.TrainingBlockId,
            WorkoutName = workout.WorkoutName,
            AppUserId = userId,
            WorkoutExercises = exercises
        };
        return res;
    }
}