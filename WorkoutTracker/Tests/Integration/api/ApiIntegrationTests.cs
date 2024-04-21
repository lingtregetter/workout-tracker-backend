using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using App.Public.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Newtonsoft.Json.Linq;
using Xunit.Abstractions;

namespace Tests.Integration.api;

public class ApiIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };

    public ApiIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task GetUserPersonalInformationIsUnauthorizedTest()
    {
        var response = await _httpClient.GetAsync("/api/v1/PersonalInformations");

        var isUnauthorized = response.StatusCode == HttpStatusCode.Unauthorized;
        Assert.True(isUnauthorized);

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostUserPersonalInformationTest()
    {
        var jwtResponse = await JwtResponse("test3@test.com", "Test3First", "Test3Last",
            "Foo.bar.1", 10);

        const string url = "/api/v1/PersonalInformations";
        const string gender = "Test";
        const decimal height = 170;
        const decimal weight = 67;

        var personalInformationData = new
        {
            Id = Guid.NewGuid(),
            Gender = gender,
            Height = height,
            Weight = weight
        };

        var data = JsonContent.Create(personalInformationData);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task GetUserProgramsIsUnauthorizedTest()
    {
        var response = await _httpClient.GetAsync("/api/v1/UserPrograms");

        var isUnauthorized = response.StatusCode == HttpStatusCode.Unauthorized;
        Assert.True(isUnauthorized);

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostTrainingProgramWithBlocksWithoutAuthorizationTest()
    {
        const string url = "/api/v1/TrainingPrograms";
        const string programName = "Test program";
        const string programDescription = "Test description";

        var programData = new
        {
            ProgramName = programName,
            ProgramDescription = programDescription,
            Blocks = new List<string>() {"Test block"}
        };

        var data = JsonContent.Create(programData);

        var postResponse = await _httpClient.PostAsync(url, data);

        var isUnauthorized = postResponse.StatusCode == HttpStatusCode.Unauthorized;
        Assert.True(isUnauthorized);

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostTrainingProgramWithBlocksTest()
    {
        var jwtResponse = await JwtResponse("test2@test.com", "Test2First", "Test2Last",
            "Foo.bar.1", 10);

        const string url = "/api/v1/TrainingPrograms";
        const string programName = "Test program";
        const string programDescription = "Test description";

        var programData = new
        {
            ProgramName = programName,
            ProgramDescription = programDescription,
            Blocks = new List<string>()
            {
                "Test block 1",
                "Test block 2"
            },
        };

        var data = JsonContent.Create(programData);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task DeleteTrainingProgramTest()
    {
        var jwtResponse = await JwtResponse("test@test.com", "TestFirst", "TestLast",
            "Foo.bar.1", 10);

        const string url = "/api/v1/TrainingPrograms";
        const string programName = "Test program";
        const string programDescription = "Test description";

        var programData = new
        {
            ProgramName = programName,
            ProgramDescription = programDescription,
            Blocks = new List<string>()
            {
                "Test block 1",
                "Test block 2"
            },
        };

        var data = JsonContent.Create(programData);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        var trainingProgramId = JObject.Parse(postResponse.Content.ReadAsStringAsync().Result)["id"]!.ToString();

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());

        var deleteUrl = $"/api/v1/TrainingPrograms/{trainingProgramId}";

        var deleteResponse = await _httpClient.DeleteAsync(deleteUrl);

        deleteResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await deleteResponse.Content.ReadAsStringAsync());
        
        var programResponse = await _httpClient.GetAsync($"/api/v1/TrainingPrograms/{trainingProgramId}");
        
        var noProgram = programResponse.StatusCode == HttpStatusCode.NotFound;
        Assert.True(noProgram);

        _testOutputHelper.WriteLine(await programResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostWorkoutWithExercisesTest()
    {
        var jwtResponse = await JwtResponse("test4@test.com", "Test4First", "Test4Last",
            "Foo.bar.1", 10);
        
        const string url = "/api/v1/Workouts";
        const string workoutName = "Test workout";
        const int avPerformanceTime = 60;

        var workoutData = new
        {
            WorkoutName = workoutName,
            AvPerformanceTime = avPerformanceTime,
            TrainingBlockId = Guid.NewGuid(),
            ExerciseIds = new List<string>()
            {
                "f911afed-5843-4470-87c7-ebfd2cf87aa3",
                "f911afed-5843-4470-87c7-ebfd2cf86aa4"
            }
        };
        
        var data = JsonContent.Create(workoutData);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);

        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }
    
    [Fact]
    public async Task PostAddNewTrainingBlocksToTrainingProgramTest()
    {
        var jwtResponse = await JwtResponse("test5@test.com", "Test5First", "Test5Last",
            "Foo.bar.1", 10);
        
        const string blockUrl = "/api/v1/TrainingBlocks";

        const string programUrl = "/api/v1/TrainingPrograms";
        const string programName = "Test program";
        const string programDescription = "Test description";

        var programData = new
        {
            ProgramName = programName,
            ProgramDescription = programDescription,
            Blocks = new List<string>()
            {
                "Test 1 block",
                "Test 2 block"
            },
        };

        var data = JsonContent.Create(programData);

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        
        var postResponse = await _httpClient.PostAsync(programUrl, data);
        
        var trainingProgramId = JObject.Parse(postResponse.Content.ReadAsStringAsync().Result)["id"]!.ToString();
        var trainingBlocks = JObject.Parse(postResponse.Content.ReadAsStringAsync().Result)["blocks"]!.ToList();
        
        postResponse.EnsureSuccessStatusCode();
        Assert.Equal(2, trainingBlocks.Count);

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
        
        var blockData = new
        {
            TrainingProgramId = trainingProgramId,
            Blocks = new List<string>()
            {
                "Block test 1",
                "Block test 2"
            },
        };
        
        var dataBlock = JsonContent.Create(blockData);

        var blockPostResponse = await _httpClient.PostAsync(blockUrl, dataBlock);
        
        blockPostResponse.EnsureSuccessStatusCode();

        var programResponse = await _httpClient.GetAsync($"/api/v1/TrainingPrograms/{trainingProgramId}");

        programResponse.EnsureSuccessStatusCode();
        
        _testOutputHelper.WriteLine(await programResponse.Content.ReadAsStringAsync());

        var trainingBlocks2 = JObject.Parse(programResponse.Content.ReadAsStringAsync().Result)["trainingBlocks"]!.ToList();
        Assert.Equal(4, trainingBlocks2.Count);
    }

    [Fact]
    public async Task GetAllMuscleGroupsPageLoadsTest()
    {
        var response = await _httpClient.GetAsync("/api/v1/MuscleGroups");

        response.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await response.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostExerciseWithMuscleGroupIdsTest()
    {
        const string url = "/api/v1/Exercises";
        const string exerciseName = "Test exercise";
        const string exerciseDescription = "Test description";

        var exerciseData = new
        {
            ExerciseName = exerciseName,
            ExerciseDescription = exerciseDescription,
            MuscleGroupIds = new List<string>() {"f911afed-5843-4470-87c7-ebfd2cf87aa3"}
        };

        var data = JsonContent.Create(exerciseData);

        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostExerciseWithoutMuscleGroupIdsTest()
    {
        const string url = "/api/v1/Exercises";
        const string exerciseName = "Test exercise";
        const string exerciseDescription = "Test description";

        var exerciseData = new
        {
            ExerciseName = exerciseName,
            ExerciseDescription = exerciseDescription,
        };

        var data = JsonContent.Create(exerciseData);

        var postResponse = await _httpClient.PostAsync(url, data);

        var withoutBlocks = postResponse.StatusCode == HttpStatusCode.BadRequest;
        Assert.True(withoutBlocks);

        _testOutputHelper.WriteLine(await postResponse.Content.ReadAsStringAsync());
    }

    [Fact]
    public async Task PostWorkoutSetToWorkoutExerciseTest()
    {
        const string url = "/api/v1/workoutSets";
        const string workoutExerciseId = "15e7a86c-1411-4cf4-bca4-9ec9fa82fdb6";
        const double usedWeight = 22.5;
        const int repNumber = 12;

        var setData = new
        {
           WorkoutExerciseId = workoutExerciseId,
           UsedWeight = usedWeight,
           RepNumber = repNumber
        };
        
        var data = JsonContent.Create(setData);
        
        var postResponse = await _httpClient.PostAsync(url, data);

        postResponse.EnsureSuccessStatusCode();

        const string urlGet = $"/api/v1/workoutSets/{workoutExerciseId}";

        var getResponse = await _httpClient.GetAsync(urlGet);
        
        getResponse.EnsureSuccessStatusCode();

        _testOutputHelper.WriteLine(await getResponse.Content.ReadAsStringAsync());
    }

    private void VerifyJwtContent(string jwt, string email, string firstName, string lastName,
        DateTime validToIsSmallerThan)
    {
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        Assert.NotNull(jwtResponse);
        Assert.NotNull(jwtResponse.RefreshToken);
        Assert.NotNull(jwtResponse.JWT);

        // verify the actual JWT
        var jwtToken = new JwtSecurityTokenHandler().ReadJwtToken(jwtResponse.JWT);
        Assert.Equal(email, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value);
        Assert.Equal(firstName, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.GivenName)?.Value);
        Assert.Equal(lastName, jwtToken.Claims.FirstOrDefault(c => c.Type == ClaimTypes.Surname)?.Value);
        Assert.True(jwtToken.ValidTo < validToIsSmallerThan);
    }

    private async Task<string> RegisterNewUser(string email, string password, string firstName, string lastName,
        int expiresInSeconds = 1)
    {
        var url = $"/api/v1/identity/account/register?expiresInSeconds={expiresInSeconds}";

        var registerData = new
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName,
        };

        var data = JsonContent.Create(registerData);

        var response = await _httpClient.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        Assert.True(response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email, firstName, lastName,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }
    
    private async Task<JWTResponse?> JwtResponse(string email, string firstName, string lastName, string password,
        int expiresInSeconds)
    {
        var jwt = await RegisterNewUser(email, password, firstName, lastName, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        return jwtResponse;
    }
}