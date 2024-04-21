using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IPersonalInformationService : IBaseRepository<App.BLL.DTO.PersonalInformation>,
    IPersonalInformationRepositoryCustom<App.BLL.DTO.PersonalInformation>
{
    
}