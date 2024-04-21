using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WorkoutSetMapper : BaseMapper<App.BLL.DTO.WorkoutSet, App.Domain.WorkoutSet>
{
    public WorkoutSetMapper(IMapper mapper) : base(mapper)
    {
    }
}