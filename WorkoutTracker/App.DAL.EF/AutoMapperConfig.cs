using AutoMapper;

namespace App.DAL.EF;

public class AutoMapperConfig : Profile
{
    public AutoMapperConfig()
    {
        CreateMap<App.Domain.PersonalInformation, App.DAL.DTO.PersonalInformation>().ReverseMap();
        CreateMap<App.Domain.UserProgram, App.DAL.DTO.UserProgram>().ReverseMap();
        CreateMap<App.Domain.TrainingProgram, App.DAL.DTO.TrainingProgram>().ReverseMap();
        CreateMap<App.Domain.TrainingBlock, App.DAL.DTO.TrainingBlock>().ReverseMap();
        CreateMap<App.Domain.Workout, App.DAL.DTO.Workout>().ReverseMap();
        CreateMap<App.Domain.WorkoutExercise, App.DAL.DTO.WorkoutExercise>().ReverseMap();
        CreateMap<App.Domain.Exercise, App.DAL.DTO.Exercise>().ReverseMap();
        CreateMap<App.Domain.ExerciseMuscle, App.DAL.DTO.ExerciseMuscle>().ReverseMap();
        CreateMap<App.Domain.MuscleGroup, App.DAL.DTO.MuscleGroup>().ReverseMap();
        CreateMap<App.Domain.WorkoutSet, App.DAL.DTO.WorkoutSet>().ReverseMap();
        CreateMap<App.Domain.Rep, App.DAL.DTO.Rep>().ReverseMap();
        CreateMap<App.Domain.Weight, App.DAL.DTO.Weight>().ReverseMap();
    }
}