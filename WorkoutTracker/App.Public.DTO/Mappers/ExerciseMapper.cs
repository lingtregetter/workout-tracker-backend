using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class ExerciseMapper : BaseMapper<App.BLL.DTO.Exercise, App.Public.DTO.v1.Exercise>
{
    public ExerciseMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.BLL.DTO.Exercise CreateMapToBll(App.Public.DTO.v1.CreateExercise exercise)
    {
        var res = new App.BLL.DTO.Exercise()
        {
            Id = exercise.Id,
            ExerciseDescription = exercise.ExerciseDescription,
            ExerciseName = exercise.ExerciseName
        };
        return res;
    }
}