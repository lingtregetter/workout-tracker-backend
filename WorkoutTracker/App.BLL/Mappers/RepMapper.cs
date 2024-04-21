using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class RepMapper : BaseMapper<App.BLL.DTO.Rep, App.Domain.Rep>
{
    public RepMapper(IMapper mapper) : base(mapper)
    {
    }
}