using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WorkoutExerciseMapper: BaseMapper<App.Domain.WorkoutExercise, App.DAL.DTO.WorkoutExercise>
{
    public WorkoutExerciseMapper(IMapper mapper) : base(mapper)
    {
    }
}