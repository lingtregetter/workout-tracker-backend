using AutoMapper;

namespace App.Public.DTO;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.PersonalInformation, App.Public.DTO.v1.PersonalInformation>().ReverseMap();
        CreateMap<App.BLL.DTO.UserProgram, App.Public.DTO.v1.UserProgram>().ReverseMap();
        CreateMap<App.BLL.DTO.TrainingProgram, App.Public.DTO.v1.TrainingProgram>().ReverseMap();
        CreateMap<App.BLL.DTO.TrainingBlock, App.Public.DTO.v1.TrainingBlock>().ReverseMap();
        CreateMap<App.BLL.DTO.Workout, App.Public.DTO.v1.Workout>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkoutExercise, App.Public.DTO.v1.WorkoutExercise>().ReverseMap();
        CreateMap<App.BLL.DTO.Exercise, App.Public.DTO.v1.Exercise>().ReverseMap();
        CreateMap<App.BLL.DTO.ExerciseMuscle, App.Public.DTO.v1.ExerciseMuscle>().ReverseMap();
        CreateMap<App.BLL.DTO.MuscleGroup, App.Public.DTO.v1.MuscleGroup>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkoutSet, App.Public.DTO.v1.WorkoutSet>().ReverseMap();
        CreateMap<App.BLL.DTO.Rep, App.Public.DTO.v1.Rep>().ReverseMap();
        CreateMap<App.BLL.DTO.Weight, App.Public.DTO.v1.Weight>().ReverseMap();
    }
}