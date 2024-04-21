using App.DAL.Contracts;
using App.Public.DTO.v1;
using Base.DAL.EF;
using Microsoft.EntityFrameworkCore;
using ExerciseMuscle = App.Domain.ExerciseMuscle;

namespace App.DAL.EF.Repositories;

public class ExerciseMuscleRepository : EFBaseRepository<ExerciseMuscle, ApplicationDbContext>,
    IExerciseMuscleRepository
{
    public ExerciseMuscleRepository(ApplicationDbContext dataContext) : base(dataContext)
    {
    }

    public override async Task<IEnumerable<ExerciseMuscle>> AllAsync()
    {
        return await RepositoryDbSet
            .Include(e => e.Exercise)
            .Include(e => e.MuscleGroup)
            .OrderBy(e => e.MuscleGroup!.MuscleName)
            .ToListAsync();
    }
}