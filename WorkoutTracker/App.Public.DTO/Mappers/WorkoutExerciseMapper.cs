using App.BLL.DTO;
using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class WorkoutExerciseMapper : BaseMapper<App.BLL.DTO.WorkoutExercise, App.Public.DTO.v1.WorkoutExercise>
{
    public WorkoutExerciseMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.WorkoutExercise MapToPublic(List<App.BLL.DTO.WorkoutExercise> workoutExercises)
    {
        var workoutExercise = new App.Public.DTO.v1.WorkoutExercise()
        {
            Id = workoutExercises.First().WorkoutId,
            WorkoutName = workoutExercises.First().Workout!.WorkoutName,
            Exercises = new List<WorkoutExerciseDetails>()
        };

        workoutExercises.ForEach(exercise =>
        {
            workoutExercise.Exercises.Add(new WorkoutExerciseDetails()
            {
                Id = exercise.ExerciseId,
                WorkoutExerciseId = exercise.Id,
                ExerciseName = exercise.Exercise!.ExerciseName,
                ExerciseDescription = exercise.Exercise!.ExerciseDescription,
            });
        });

        return workoutExercise;
    }
}