# Generate db migration

```bash
# install or update
dotnet tool install --global dotnet-ef
dotnet tool update --global dotnet-ef

# create migration
dotnet ef migrations add Initial --project App.DAL.EF --startup-project WebApp --context ApplicationDbContext
dotnet ef migrations add Casc --project App.DAL.EF --startup-project WebApp --context ApplicationDbContext

# apply migration
dotnet ef database update --project App.DAL.EF --startup-project WebApp --context ApplicationDbContext
```

# generate rest controllers

```bash
# install tooling
dotnet tool install -g dotnet-aspnet-codegenerator
dotnet tool update -g dotnet-aspnet-codegenerator

# SCAFFOLDING
# Web controllers
cd WebApp
dotnet aspnet-codegenerator controller -m TrainingProgram -name TrainingProgramsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m UserProgram -name UserProgramsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m TrainingBlock -name TrainingBlocksController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Workout -name WorkoutsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Exercise -name ExercisesController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m MuscleGroup -name MuscleGroupsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m ExerciseMuscle -name ExerciseMusclesController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m PersonalInformation -name PersonalInformationsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m WorkoutExercise -name WorkoutExercisesController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Rep -name RepsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m Weight -name WeightsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f
dotnet aspnet-codegenerator controller -m WorkoutSet -name WorkoutSetsController -outDir Controllers -dc ApplicationDbContext -udl --referenceScriptLibraries -f

# Rest API
dotnet aspnet-codegenerator controller -m PersonalInformation -name PersonalInformationsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m UserProgram -name UserProgramsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m App.Domain.TrainingProgram -name TrainingProgramsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m App.Domain.TrainingBlock -name TrainingBlocksController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m Workout -name WorkoutsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m Exercise -name ExercisesController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m MuscleGroup -name MuscleGroupsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m ExerciseMuscle -name ExerciseMusclesController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m WorkoutExercise -name WorkoutExercisesController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m Rep -name RepsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m Weight -name WeightsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
dotnet aspnet-codegenerator controller -m WorkoutSet -name WorkoutSetsController -outDir ApiControllers -api -dc ApplicationDbContext -udl -f
```

# necessary nuget packages

```bash
# WebApp
- Microsoft.VisualStudio.Web.CodeGeneration.Design
- Microsoft.EntityFrameworkCore.SqlServer
- Npqsql.EntityFrameworkCore.PostgreSQL
- Microsoft.AspNetCore.Authentication.JwtBearer
- AutoMapper.Extensions.Microsoft.DependencyInjection
- Asp.Versioning.Mvc.ApiExplorer
- Swashbuckle.AspNetCore

# App.DAL.EF
- Microsoft.EntityFrameworkCore.Sqlite
- Npqsql.EntityFrameworkCore.PostgreSQL

# Base.DAL
- AutoMapper.Extensions.Microsoft.DependencyInjection

# Tests
- Microsoft.EntityFrameworkCore.InMemory
- Microsoft.AspNetCore.Mvc.Testing
- AngleSharp
```

# generate Identity UI

~~~bash
cd WebApp
dotnet aspnet-codegenerator identity -dc App.DAL.EF.ApplicationDbContext --userClass AppUser -f
~~~
