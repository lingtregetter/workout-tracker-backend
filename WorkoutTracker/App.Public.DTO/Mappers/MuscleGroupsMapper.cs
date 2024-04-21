using App.Public.DTO.v1;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class MuscleGroupsMapper : BaseMapper<App.BLL.DTO.MuscleGroup, App.Public.DTO.v1.MuscleGroup>
{
    public MuscleGroupsMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.MuscleGroup MapToPublic(App.BLL.DTO.MuscleGroup muscleGroup)
    {
        var res = new App.Public.DTO.v1.MuscleGroup()
        {
            Id = muscleGroup.Id,
            MuscleName = muscleGroup.MuscleName
        };
        return res;
    }
}