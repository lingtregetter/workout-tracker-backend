using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class WeightMapper: BaseMapper<App.Domain.Weight, App.DAL.DTO.Weight>
{
    public WeightMapper(IMapper mapper) : base(mapper)
    {
    }
}