using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF;

public class ApplicationDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<AppRefreshToken> AppRefreshTokens { get; set; } = default!;
    public DbSet<TrainingProgram> TrainingPrograms { get; set; } = default!;
    public DbSet<UserProgram> UserPrograms { get; set; } = default!;
    public DbSet<TrainingBlock> TrainingBlocks { get; set; } = default!;
    public DbSet<Workout> Workouts { get; set; } = default!;
    public DbSet<MuscleGroup> MuscleGroups { get; set; } = default!;
    public DbSet<Exercise> Exercises { get; set; } = default!;
    public DbSet<ExerciseMuscle> ExerciseMuscles { get; set; } = default!;
    public DbSet<PersonalInformation> PersonalInformations { get; set; } = default!;
    public DbSet<WorkoutExercise> WorkoutExercises { get; set; } = default!;
    public DbSet<WorkoutSet> WorkoutSets { get; set; } = default!;
    public DbSet<Rep> Reps { get; set; } = default!;
    public DbSet<Weight> Weights { get; set; } = default!;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // let the initial stuff run
        base.OnModelCreating(modelBuilder);
    }
}