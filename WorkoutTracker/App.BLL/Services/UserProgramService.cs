using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class UserProgramService :
    BaseEntityService<App.BLL.DTO.UserProgram, App.Domain.UserProgram, IUserProgramRepository>,
    IUserProgramService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public UserProgramService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.UserProgram, App.Domain.UserProgram> mapper) :
        base(appUnitOfWork.UserProgramRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<IEnumerable<App.BLL.DTO.UserProgram>> AllAsync(Guid userId)
    {
        return (await AppUnitOfWork.UserProgramRepository.AllAsync(userId))
            .Select(e => Mapper.Map(e)!);
    }

    public async Task<App.BLL.DTO.UserProgram?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.UserProgramRepository.FindAsync(id, userId));
    }

    public Task<App.BLL.DTO.UserProgram?> RemoveAsync(Guid id, Guid userId)
    {
        throw new NotImplementedException();
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await AppUnitOfWork.UserProgramRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<UserProgram?> FindAsyncByProgramId(Guid programId, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.UserProgramRepository.FindAsyncByProgramId(programId, userId));
    }

    public async Task<UserProgram?> RemoveAsyncByProgramId(Guid programId, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.UserProgramRepository.RemoveAsyncByProgramId(programId, userId));
    }
}