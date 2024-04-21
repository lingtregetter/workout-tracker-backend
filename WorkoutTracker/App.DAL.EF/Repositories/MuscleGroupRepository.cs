using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class MuscleGroupRepository : EFBaseRepository<MuscleGroup, ApplicationDbContext>, IMuscleGroupRepository
{
    public MuscleGroupRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}