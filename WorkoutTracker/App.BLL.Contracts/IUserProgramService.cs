using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IUserProgramService : IBaseRepository<App.BLL.DTO.UserProgram>,
    IUserProgramRepositoryCustom<App.BLL.DTO.UserProgram>
{
}