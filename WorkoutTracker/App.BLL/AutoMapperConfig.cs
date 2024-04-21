using AutoMapper;

namespace App.BLL;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.BLL.DTO.PersonalInformation, App.Domain.PersonalInformation>().ReverseMap();
        CreateMap<App.BLL.DTO.ExerciseMuscle, App.Domain.ExerciseMuscle>().ReverseMap();
        CreateMap<App.BLL.DTO.Exercise, App.Domain.Exercise>().ReverseMap();
        CreateMap<App.BLL.DTO.MuscleGroup, App.Domain.MuscleGroup>().ReverseMap();
        CreateMap<App.BLL.DTO.Rep, App.Domain.Rep>().ReverseMap();
        CreateMap<App.BLL.DTO.TrainingBlock, App.Domain.TrainingBlock>().ReverseMap();
        CreateMap<App.BLL.DTO.TrainingProgram, App.Domain.TrainingProgram>().ReverseMap();
        CreateMap<App.BLL.DTO.UserProgram, App.Domain.UserProgram>().ReverseMap();
        CreateMap<App.BLL.DTO.Weight, App.Domain.Weight>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkoutExercise, App.Domain.WorkoutExercise>().ReverseMap();
        CreateMap<App.BLL.DTO.Workout, App.Domain.Workout>().ReverseMap();
        CreateMap<App.BLL.DTO.WorkoutSet, App.Domain.WorkoutSet>().ReverseMap();
    }
}