using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WorkoutMapper: BaseMapper<App.Domain.Workout, App.DAL.DTO.Workout>
{
    public WorkoutMapper(IMapper mapper) : base(mapper)
    {
    }
}