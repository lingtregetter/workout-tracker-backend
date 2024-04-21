using App.BLL.DTO;
using App.DAL.Contracts;
using Base.DAL.Contracts;

namespace App.BLL.Contracts;

public interface ITrainingProgramService : IBaseRepository<App.BLL.DTO.TrainingProgram>,
    ITrainingProgramRepositoryCustom<App.BLL.DTO.TrainingProgram>
{
    new App.BLL.DTO.TrainingProgram Add(TrainingProgram entity);
}