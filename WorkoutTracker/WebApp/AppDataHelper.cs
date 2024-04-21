using App.DAL.EF;
using App.DAL.EF.Seeding;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity;

namespace WebApp;

/// <summary>
/// App data helper class
/// </summary>
public static class AppDataHelper
{
    /// <summary>
    /// Automates dropping db, seeding data and identity, migration adding
    /// </summary>
    /// <param name="applicationBuilder"></param>
    /// <param name="webHostEnvironment"></param>
    /// <param name="configuration"></param>
    /// <exception cref="ApplicationException"></exception>
    public static void SetUpAppData(IApplicationBuilder applicationBuilder, IWebHostEnvironment webHostEnvironment,
        IConfiguration configuration)
    {
        using var serviceScope = applicationBuilder.ApplicationServices
            .GetRequiredService<IServiceScopeFactory>()
            .CreateScope();

        using var context = serviceScope.ServiceProvider
            .GetService<ApplicationDbContext>();

        if (context == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize DB Context");
        }

        using var userManager = serviceScope.ServiceProvider.GetService<UserManager<AppUser>>();
        using var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<AppRole>>();

        if (userManager == null || roleManager == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize UserManager or RoleManager");
        }

        var logger = serviceScope.ServiceProvider.GetService<ILogger<IApplicationBuilder>>();

        if (logger == null)
        {
            throw new ApplicationException("Problem in services. Can't initialize logger");
        }

        if (context.Database.ProviderName!.Contains("InMemory"))
        {
            return;
        }

        // TODO: wait for db connection

        // Drop?
        if (configuration.GetValue<bool>("DataInitialization:DropDatabase"))
        {
            logger.LogWarning("Dropping database");
            AppDataInitialization.DropDatabase(context);
        }

        // Migrate?
        if (configuration.GetValue<bool>("DataInitialization:MigrateDatabase"))
        {
            logger.LogInformation("Migrating database");
            AppDataInitialization.MigrateDatabase(context);
        }

        // Seed identity?
        if (configuration.GetValue<bool>("DataInitialization:SeedIdentity"))
        {
            logger.LogInformation("Seeding identity");
            AppDataInitialization.SeedIdentity(userManager, roleManager);
        }

        // Seed application data?
        if (configuration.GetValue<bool>("DataInitialization:SeedData"))
        {
            logger.LogInformation("Seed app data");
            AppDataInitialization.SeedAppData(context);
        }
    }
}