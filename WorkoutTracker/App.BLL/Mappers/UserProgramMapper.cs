using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class UserProgramMapper : BaseMapper<App.BLL.DTO.UserProgram, App.Domain.UserProgram>
{
    public UserProgramMapper(IMapper mapper) : base(mapper)
    {
    }
}