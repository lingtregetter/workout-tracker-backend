using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.Contracts;
using Base.BLL;
using Base.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Services;

public class TrainingProgramService : BaseEntityService<App.BLL.DTO.TrainingProgram, App.Domain.TrainingProgram,
        ITrainingProgramRepository>,
    ITrainingProgramService
{
    protected IAppUnitOfWork AppUnitOfWork;

    public TrainingProgramService(IAppUnitOfWork appUnitOfWork,
        IMapper<App.BLL.DTO.TrainingProgram, App.Domain.TrainingProgram> mapper) :
        base(appUnitOfWork.TrainingProgramRepository, mapper)
    {
        AppUnitOfWork = appUnitOfWork;
    }

    public async Task<bool> IsOwnedByUserAsync(Guid id, Guid userId)
    {
        return await AppUnitOfWork.TrainingProgramRepository.IsOwnedByUserAsync(id, userId);
    }

    public async Task<TrainingProgram?> FindAsync(Guid id, Guid userId)
    {
        return Mapper.Map(await AppUnitOfWork.TrainingProgramRepository.FindAsync(id, userId));
    }

    public new App.BLL.DTO.TrainingProgram Add(TrainingProgram entity)
    {
        var trainingProgram = AppUnitOfWork.TrainingProgramRepository.Add(Mapper.Map(entity)!);
        
        foreach (var testTrainingBlock in trainingProgram.TrainingBlocks!)
        {
            AppUnitOfWork.TrainingBlockRepository.Add(testTrainingBlock);
        }

        AppUnitOfWork.UserProgramRepository.Add(trainingProgram.UserPrograms!.FirstOrDefault()!);

        return Mapper.Map(trainingProgram)!;
    }
}