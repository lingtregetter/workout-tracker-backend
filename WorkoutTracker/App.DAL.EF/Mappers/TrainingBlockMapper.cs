using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class TrainingBlockMapper: BaseMapper<App.Domain.TrainingBlock, App.DAL.DTO.TrainingBlock>
{
    public TrainingBlockMapper(IMapper mapper) : base(mapper)
    {
    }
}