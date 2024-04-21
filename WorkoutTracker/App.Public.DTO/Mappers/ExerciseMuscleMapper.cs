using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ExerciseMuscleMapper : BaseMapper<App.BLL.DTO.ExerciseMuscle, App.Public.DTO.v1.ExerciseMuscle>
{
    public ExerciseMuscleMapper(IMapper mapper) : base(mapper)
    {
    }

    public List<App.Public.DTO.v1.ExerciseMuscle> MapToPublicList(List<App.BLL.DTO.ExerciseMuscle> exerciseMuscles)
    {
        var list = new List<ExerciseMuscle>();

        exerciseMuscles.ForEach(e =>
        {
            var existingExerciseMuscle = list.Find(t => t.MuscleGroupId.Equals(e.MuscleGroupId));

            if (existingExerciseMuscle == null)
            {
                var n = new ExerciseMuscle()
                {
                    Id = e.Id,
                    MuscleGroupName = e.MuscleGroup!.MuscleName,
                    MuscleGroupId = e.MuscleGroup!.Id,
                    Exercises = new List<MuscleExercise>
                        {new MuscleExercise() {Id = e.Exercise!.Id, ExerciseName = e.Exercise!.ExerciseName}}
                };

                list.Add(n);
            }
            else
            {
                existingExerciseMuscle.Exercises.Add(new MuscleExercise()
                    {Id = e.Exercise!.Id, ExerciseName = e.Exercise!.ExerciseName});
            }
        });

        return list;
    }
}