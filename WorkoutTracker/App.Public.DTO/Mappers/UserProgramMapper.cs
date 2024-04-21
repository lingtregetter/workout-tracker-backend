using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class UserProgramMapper : BaseMapper<App.BLL.DTO.UserProgram, App.Public.DTO.v1.UserProgram>
{
    public UserProgramMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.UserProgram? MapToPublic(App.BLL.DTO.UserProgram? userProgram)
    {
        if (userProgram == null) return null;
        var res = new App.Public.DTO.v1.UserProgram()
        {
            Id = userProgram.Id,
            TrainingProgramName = userProgram.TrainingProgram!.ProgramName,
            TrainingProgramDescription = userProgram.TrainingProgram!.ProgramDescription,
            TrainingProgramId = userProgram.TrainingProgram.Id
        };
        return res;
    }
}