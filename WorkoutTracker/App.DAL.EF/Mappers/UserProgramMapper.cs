using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class UserProgramMapper: BaseMapper<App.Domain.UserProgram, App.DAL.DTO.UserProgram>
{
    public UserProgramMapper(IMapper mapper) : base(mapper)
    {
    }
}