using App.DAL.Contracts;
using App.Domain;
using Base.DAL.EF;

namespace App.DAL.EF.Repositories;

public class ExerciseRepository : EFBaseRepository<Exercise, ApplicationDbContext>, IExerciseRepository
{
    public ExerciseRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }
}