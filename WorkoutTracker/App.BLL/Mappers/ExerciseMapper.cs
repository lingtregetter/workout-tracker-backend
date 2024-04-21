using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ExerciseMapper : BaseMapper<App.BLL.DTO.Exercise, App.Domain.Exercise>
{
    public ExerciseMapper(IMapper mapper) : base(mapper)
    {
    }
}