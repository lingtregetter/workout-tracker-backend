using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class TrainingBlockMapper : BaseMapper<App.BLL.DTO.TrainingBlock, App.Domain.TrainingBlock>
{
    public TrainingBlockMapper(IMapper mapper) : base(mapper)
    {
    }
}