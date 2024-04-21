using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class MuscleGroupMapper: BaseMapper<App.Domain.MuscleGroup, App.DAL.DTO.MuscleGroup>
{
    public MuscleGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}