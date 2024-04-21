using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class WeightMapper : BaseMapper<App.BLL.DTO.Weight, App.Public.DTO.v1.Weight>
{
    public WeightMapper(IMapper mapper) : base(mapper)
    {
    }
}