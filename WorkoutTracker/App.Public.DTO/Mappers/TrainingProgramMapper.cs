using App.BLL.DTO;
using AutoMapper;
using Base.DAL;

namespace App.Public.DTO.Mappers;

public class TrainingProgramMapper : BaseMapper<App.BLL.DTO.TrainingProgram, App.Public.DTO.v1.TrainingProgram>
{
    public TrainingProgramMapper(IMapper mapper) : base(mapper)
    {
    }

    public App.Public.DTO.v1.TrainingProgram MapToPublic(App.BLL.DTO.TrainingProgram trainingProgram)
    {
        var blocks = new List<App.Public.DTO.v1.TrainingProgramBlock>();
        var orderedBlocks = trainingProgram.TrainingBlocks?
            .OrderBy(e => e.CreatedAt).ToList();

        orderedBlocks?.ForEach(block =>
        {
            var dtoBlock = new App.Public.DTO.v1.TrainingProgramBlock()
            {
                Id = block.Id,
                BlockName = block.BlockName,
            };
            blocks.Add(dtoBlock);
        });

        var res = new App.Public.DTO.v1.TrainingProgram()
        {
            Id = trainingProgram.Id,
            ProgramDescription = trainingProgram.ProgramDescription,
            ProgramName = trainingProgram.ProgramName,
            TrainingBlocks = blocks
        };
        return res;
    }

    public App.BLL.DTO.TrainingProgram CreateMapToBll(App.Public.DTO.v1.CreateTrainingProgram trainingProgram,
        Guid userId)
    {
        var trainingBlocks = new List<TrainingBlock>() {};
        
        trainingProgram.Blocks.ForEach(block =>
        {
            var trainingBlock = new App.BLL.DTO.TrainingBlock()
            {
                BlockName = block,
                TrainingProgramId = trainingProgram.Id,
                AppUserId = userId
            };
            
            trainingBlocks.Add(trainingBlock);
        });

        var res = new App.BLL.DTO.TrainingProgram()
        {
            Id = trainingProgram.Id,
            ProgramDescription = trainingProgram.ProgramDescription,
            ProgramName = trainingProgram.ProgramName,
            AppUserId = userId,
            TrainingBlocks = trainingBlocks,
            UserPrograms = new List<UserProgram>(){new UserProgram()
            {
                AppUserId = userId,
                TrainingProgramId = trainingProgram.Id
            }}
        };

        return res;
    }
}