using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class RepMapper: BaseMapper<App.Domain.Rep, App.DAL.DTO.Rep>
{
    public RepMapper(IMapper mapper) : base(mapper)
    {
    }
}