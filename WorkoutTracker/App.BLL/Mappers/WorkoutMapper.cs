using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkoutMapper : BaseMapper<App.BLL.DTO.Workout, App.Domain.Workout>
{
    public WorkoutMapper(IMapper mapper) : base(mapper)
    {
    }
}