using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class ExerciseMuscleMapper : BaseMapper<App.BLL.DTO.ExerciseMuscle, App.Domain.ExerciseMuscle>
{
    public ExerciseMuscleMapper(IMapper mapper) : base(mapper)
    {
    }
}