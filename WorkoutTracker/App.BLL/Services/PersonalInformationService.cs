using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;

namespace App.BLL.Services;

public class PersonalInformationService :
    BaseEntityService<App.BLL.DTO.PersonalInformation, App.Domain.PersonalInformation, IPersonalInformationRepository>, 
    IPersonalInformationService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public PersonalInformationService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.PersonalInformation, App.Domain.PersonalInformation> mapper) :
        base(appUnitOfWork.PersonalInformationRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<IEnumerable<PersonalInformation>> AllAsync(Guid userId)
    {
        return (await AppUnitOfWork.PersonalInformationRepository.AllAsync(userId))
            .Select(e => Mapper.Map(e)!);
    }

    public async Task<PersonalInformation?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.PersonalInformationRepository.FindAsync(id, userId));
    }

    public async Task<PersonalInformation?> RemoveAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.PersonalInformationRepository.RemoveAsync(id, userId));
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await AppUnitOfWork.PersonalInformationRepository.IsOwnedByUserAsync(id, userId);
    }
}