using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface IWeightService : IBaseRepository<App.BLL.DTO.Weight>,
    IWeightRepositoryCustom<App.BLL.DTO.Weight>
{
    void Add(Guid workoutSetId, decimal usedWeight);
}