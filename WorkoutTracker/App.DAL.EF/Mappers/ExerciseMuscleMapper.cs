using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class ExerciseMuscleMapper: BaseMapper<App.Domain.ExerciseMuscle, App.DAL.DTO.ExerciseMuscle>
{
    public ExerciseMuscleMapper(IMapper mapper) : base(mapper)
    {
    }
}