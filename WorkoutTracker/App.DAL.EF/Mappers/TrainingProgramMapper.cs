using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TrainingProgramMapper: BaseMapper<App.Domain.TrainingProgram, App.DAL.DTO.TrainingProgram>
{
    public TrainingProgramMapper(IMapper mapper) : base(mapper)
    {
    }
}