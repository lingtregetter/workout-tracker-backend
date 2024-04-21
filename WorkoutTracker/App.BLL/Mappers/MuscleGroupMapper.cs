using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class MuscleGroupMapper : BaseMapper<App.BLL.DTO.MuscleGroup, App.Domain.MuscleGroup>
{
    public MuscleGroupMapper(IMapper mapper) : base(mapper)
    {
    }
}