using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkoutExerciseMapper : BaseMapper<App.BLL.DTO.WorkoutExercise, App.Domain.WorkoutExercise>
{
    public WorkoutExerciseMapper(IMapper mapper) : base(mapper)
    {
    }
}