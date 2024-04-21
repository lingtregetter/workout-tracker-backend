using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class PersonalInformationMapper :
    BaseMapper<App.BLL.DTO.PersonalInformation, App.Public.DTO.v1.PersonalInformation>
{
    public PersonalInformationMapper(IMapper mapper) : base(mapper)
    {
    }
}