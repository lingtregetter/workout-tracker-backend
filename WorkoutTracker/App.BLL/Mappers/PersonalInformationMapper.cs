using AutoMapper;
using Base.DAL;

namespace App.BLL.Mappers;

public class PersonalInformationMapper : BaseMapper<App.BLL.DTO.PersonalInformation, App.Domain.PersonalInformation>
{
    public PersonalInformationMapper(IMapper mapper) : base(mapper)
    {
    }
}