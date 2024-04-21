using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class RepMapper : BaseMapper<App.BLL.DTO.Rep, App.Public.DTO.v1.Rep>
{
    public RepMapper(IMapper mapper) : base(mapper)
    {
    }
}