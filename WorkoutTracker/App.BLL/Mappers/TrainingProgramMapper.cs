using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TrainingProgramMapper : BaseMapper<App.BLL.DTO.TrainingProgram, App.Domain.TrainingProgram>
{
    public TrainingProgramMapper(IMapper mapper) : base(mapper)
    {
    }
}