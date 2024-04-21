using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ExerciseMapper: BaseMapper<App.Domain.Exercise, App.DAL.DTO.Exercise>
{
    public ExerciseMapper(IMapper mapper) : base(mapper)
    {
    }
}