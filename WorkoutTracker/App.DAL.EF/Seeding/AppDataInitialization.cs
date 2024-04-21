using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace App.DAL.EF.Seeding;

public static class AppDataInitialization
{
    private static Guid adminId = Guid.Parse("b038cb8b-a6e4-4591-b2a1-c96b8ff5f287");

    public static void MigrateDatabase(ApplicationDbContext applicationDbContext)
    {
        applicationDbContext.Database.Migrate();
    }

    public static void DropDatabase(ApplicationDbContext applicationDbContext)
    {
        applicationDbContext.Database.EnsureDeleted();
    }

    public static void SeedIdentity(UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
    {
        (Guid id, string email, string password) userData = (adminId, "admin@app.com", "Foo.bar.1");

        var user = userManager.FindByEmailAsync(userData.email).Result;

        if (user == null)
        {
            user = new AppUser()
            {
                Id = userData.id,
                Email = userData.email,
                UserName = userData.email,
                FirstName = "Admin",
                LastName = "App",
                EmailConfirmed = true,
            };

            var result = userManager.CreateAsync(user, userData.password).Result;

            if (!result.Succeeded)
            {
                throw new ApplicationException($"Cannot seed users, {result.ToString()}");
            }
        }
    }

    public static void SeedAppData(ApplicationDbContext applicationDbContext)
    {
        SeedAppDataMuscleGroups(applicationDbContext);

        applicationDbContext.SaveChanges();
    }

    private static void SeedAppDataMuscleGroups(ApplicationDbContext applicationDbContext)
    {
        var muscleGroups = new List<string>() {"Chest", "Back", "Arms", "Abdominals", "Legs", "Shoulders"};
        
        if (applicationDbContext.MuscleGroups.Any()) return;

        foreach (var muscleGroup in muscleGroups)
        {
            applicationDbContext.MuscleGroups.Add(new MuscleGroup()
            {
                MuscleName = muscleGroup
            });
        }
    }
}