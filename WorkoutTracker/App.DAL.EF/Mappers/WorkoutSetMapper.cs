using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WorkoutSetMapper : BaseMapper<App.Domain.WorkoutSet, App.DAL.DTO.WorkoutSet>
{
    public WorkoutSetMapper(IMapper mapper) : base(mapper)
    {
    }
}