using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class WeightMapper : BaseMapper<App.BLL.DTO.Weight, App.Domain.Weight>
{
    public WeightMapper(IMapper mapper) : base(mapper)
    {
    }
}