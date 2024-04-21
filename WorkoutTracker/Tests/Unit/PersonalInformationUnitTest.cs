using App.BLL;
using App.BLL.Contracts;
using App.BLL.DTO;
using App.DAL.EF;
using App.Domain.Identity;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Xunit.Abstractions;

namespace Tests.Unit;

public class PersonalInformationUnitTest
{
    private readonly ITestOutputHelper _testOutputHelper;
    private readonly ApplicationDbContext _ctx;
    private readonly IAppBLL _appBll;
    
    // predefined id's
    private readonly Guid _userId = Guid.NewGuid();
    private readonly Guid _userId2 = Guid.NewGuid();

    public PersonalInformationUnitTest(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        
        // set up mock database - in memory
        var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();
        
        // use random guid as db instance id
        optionsBuilder.UseInMemoryDatabase(Guid.NewGuid().ToString());
        _ctx = new ApplicationDbContext(optionsBuilder.Options);
        
        // reset db
        _ctx.Database.EnsureDeleted();
        _ctx.Database.EnsureCreated();

        // mapper conf
        var testMapperConfiguration = new MapperConfiguration(config =>
            {
                config.CreateMap<App.BLL.DTO.PersonalInformation, App.Public.DTO.v1.PersonalInformation>().ReverseMap();
                config.CreateMap<App.BLL.DTO.PersonalInformation, App.Domain.PersonalInformation>().ReverseMap();
            }
        );

        _appBll = new AppBLL(new AppUnitOfWork(_ctx), new Mapper(testMapperConfiguration));
    }

    [Fact]
    public async Task TestGetUserPersonalInformationByUserId()
    {
        // test case is failed when an exception is thrown
        
        // arrange

        // seed data
        var testPersonalInformation = await SeedDataAsync();

        // act
        // test find with user that doesn't exist
        var testWrongUser = await _appBll.PersonalInformationService.FindAsync(Guid.NewGuid());
        
        Assert.Null(testWrongUser);

        // test if personal information belongs to right user
        var ownership = await _appBll.PersonalInformationService
            .IsOwnedByUserAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.True(ownership);
        
        // test get user personal information by user id
        var result = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.AppUserId);
        
        var personalInformation = new App.BLL.DTO.PersonalInformation()
        {
            Id = testPersonalInformation.Entity.Id,
            AppUserId = testPersonalInformation.Entity.AppUserId,
            Gender = "Male",
            Height = 180,
            Weight = 78,
        };

        Assert.NotNull(result);
        Assert.Equal(personalInformation.Id, result.Id);
        Assert.Equal(personalInformation.Gender, result.Gender);
        Assert.Equal(personalInformation.Height, result.Height);
        Assert.Equal(personalInformation.Weight, result.Weight);
    }

    [Fact]
    public async Task TestGetUserPersonalInformationByUserIdAndPersonalInformationId()
    {
        var testPersonalInformation = await SeedDataAsync();

        // test get user personal information by user id and personal information id
        var result = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);

        var checkPersonalInformation = new PersonalInformation()
        {
            Id = testPersonalInformation.Entity.Id,
            AppUserId = testPersonalInformation.Entity.AppUserId,
            Gender = "Male",
            Height = 180,
            Weight = 78,
        };
        
        Assert.NotNull(result);
        Assert.Equal(checkPersonalInformation.AppUserId, result.AppUserId);
        Assert.Equal(checkPersonalInformation.Gender, result.Gender);
        Assert.Equal(checkPersonalInformation.Height, result.Height);
        Assert.Equal(checkPersonalInformation.Weight, result.Weight);
    }

    [Fact]
    public async Task TestEditUserPersonalInformation()
    {
        var testPersonalInformation = await SeedDataAsync();

        var result = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        testPersonalInformation.State = EntityState.Detached;

        result!.Weight = 74;

        _appBll.PersonalInformationService.Update(result);
        await _appBll.SaveChangesAsync();

        var updatedResult =
            await _appBll.PersonalInformationService.FindAsync(testPersonalInformation.Entity.AppUserId);
        
        Assert.NotNull(result);
        Assert.NotNull(updatedResult);
        Assert.Equal(result.Id, updatedResult.Id);
        Assert.Equal(result.AppUserId, updatedResult.AppUserId);
        Assert.Equal(result.Gender, updatedResult.Gender);
        Assert.Equal(result.Height, updatedResult.Height);
        Assert.Equal(result.Weight, updatedResult.Weight);
    }

    [Fact]
    public async Task TestDeleteUserPersonalInformationByIds()
    {
        var testPersonalInformation = await SeedDataAsync();
        
        var ownership = await _appBll.PersonalInformationService
            .IsOwnedByUserAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.True(ownership);

        await _appBll.PersonalInformationService
            .RemoveAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        await _appBll.SaveChangesAsync();
        
        var result = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.Null(result);
    }
    
    [Fact]
    public async Task TestDeleteUserPersonalInformationByPersonalInformation()
    {
        var testPersonalInformation = await SeedDataAsync();
        
        var ownership = await _appBll.PersonalInformationService
            .IsOwnedByUserAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.True(ownership);
        
        var informationToBeDeleted = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.NotNull(informationToBeDeleted);
        
        testPersonalInformation.State = EntityState.Detached;

        _appBll.PersonalInformationService.Remove(informationToBeDeleted);
        await _appBll.SaveChangesAsync();
        
        var result = await _appBll.PersonalInformationService
            .FindAsync(testPersonalInformation.Entity.Id, testPersonalInformation.Entity.AppUserId);
        
        Assert.Null(result);
    }

    [Fact]
    public async Task TestAddPersonalInformationToUserWhoDontHaveOneYet()
    {
        var user2 = new AppUser()
        {
            Id = _userId2,
            Email = "test2@app.com",
            UserName = "test2@app.com",
            FirstName = "Test2",
            LastName = "App2"
        };
        _ctx.Users.Add(user2);

        var noPersonalInformation = await _appBll.PersonalInformationService.FindAsync(_userId2);
        
        Assert.Null(noPersonalInformation);

        var newPersonalInformation = new App.BLL.DTO.PersonalInformation()
        {
            Gender = "Female",
            Height = 170,
            Weight = 69,
            CreatedAt = DateTime.UtcNow,
            AppUserId = _userId2,
            Id = Guid.NewGuid()
        };
        
        _appBll.PersonalInformationService.Add(newPersonalInformation);
        await _appBll.SaveChangesAsync();

        var personalInformation = await _appBll.PersonalInformationService.FindAsync(_userId2);
        
        Assert.NotNull(personalInformation);
        Assert.Equal(_userId2,personalInformation.AppUserId);
        Assert.Equal(newPersonalInformation.Gender, personalInformation.Gender);
        Assert.Equal(newPersonalInformation.Height, personalInformation.Height);
        Assert.Equal(newPersonalInformation.Weight, personalInformation.Weight);
    }

    private async Task<EntityEntry<App.Domain.PersonalInformation>> SeedDataAsync()
    {
        var testPersonalInformationId = Guid.NewGuid();
        
        var user = new AppUser()
        {
            Id = _userId,
            Email = "test@app.com",
            UserName = "test@app.com",
            FirstName = "Test",
            LastName = "App"
        };
        _ctx.Users.Add(user);

        var testPersonalInformation = _ctx.PersonalInformations.Add(new App.Domain.PersonalInformation()
        {
            Gender = "Male",
            Height = 180,
            Weight = 78,
            CreatedAt = DateTime.UtcNow,
            AppUserId = _userId,
            Id = testPersonalInformationId
        });

        await _ctx.SaveChangesAsync();

        return testPersonalInformation;
    }
}