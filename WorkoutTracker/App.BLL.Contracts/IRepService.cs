using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IRepService : IBaseRepository<App.BLL.DTO.Rep>,
    IRepRepositoryCustom<App.BLL.DTO.Rep>
{
}