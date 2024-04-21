using AutoMapper;
using Base.DAL;

namespace App.DAL.EF.Mappers;

public class PersonalInformationMapper: BaseMapper<App.Domain.PersonalInformation, App.DAL.DTO.PersonalInformation>
{
    public PersonalInformationMapper(IMapper mapper) : base(mapper)
    {
    }
}