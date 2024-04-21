using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using System.Security.Claims;
using System.Text.Json;
using App.Public.DTO.v1.Identity;
using Microsoft.AspNetCore.Mvc.Testing;
using Xunit.Abstractions;

namespace Tests.Integration.api.identity;

public class IdentityIntegrationTests : IClassFixture<CustomWebAppFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly CustomWebAppFactory<Program> _factory;
    private readonly ITestOutputHelper _testOutputHelper;

    private readonly JsonSerializerOptions _camelCaseJsonSerializerOptions = new JsonSerializerOptions()
    {
        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
    };
    
    public IdentityIntegrationTests(CustomWebAppFactory<Program> factory, ITestOutputHelper testOutputHelper)
    {
        _factory = factory;
        _testOutputHelper = testOutputHelper;
        _httpClient = factory.CreateClient(new WebApplicationFactoryClientOptions
        {
            AllowAutoRedirect = false
        });
    }

    [Fact]
    public async Task RegisterNewUserTest()
    {
        // Arrange
        const string url = "/api/v1/identity/account/register?expiresInSeconds=1";
        const string email = "register@test.com";
        const string firstName = "TestFirst";
        const string lastName = "TestLast";
        const string password = "Foo.bar.1";

        var registerData = new
        {
            Email = email,
            Password = password,
            FirstName = firstName,
            LastName = lastName
        };

        var data = JsonContent.Create(registerData);

        // Act
        var response = await _httpClient.PostAsync(url, data);

        // Assert
        var responseContent = await response.Content.ReadAsStringAsync();
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, firstName, lastName, DateTime.Now.AddSeconds(2).ToUniversalTime());
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

        // Act
        var response = await _httpClient.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);

        VerifyJwtContent(responseContent, email, firstName, lastName,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());

        return responseContent;
    }

    [Fact]
    public async Task LoginUserTest()
    {
        const string email = "login@test.com";
        const string firstName = "TestFirst";
        const string lastName = "TestLast";
        const string password = "Foo.bar.1";
        const int expiresInSeconds = 1;

        // Arrange
        await RegisterNewUser(email, password, firstName, lastName, expiresInSeconds);

        var url = "/api/v1/identity/account/login?expiresInSeconds=1";

        var loginData = new
        {
            Email = email,
            Password = password
        };

        var data = JsonContent.Create(loginData);

        // Act
        var response = await _httpClient.PostAsync(url, data);

        var responseContent = await response.Content.ReadAsStringAsync();

        // Assert
        Assert.True(response.IsSuccessStatusCode);
        VerifyJwtContent(responseContent, email, firstName, lastName,
            DateTime.Now.AddSeconds(expiresInSeconds + 1).ToUniversalTime());
    }

    [Fact]
    public async Task JwtExpired()
    {
        const string email = "expired@test.com";
        const string firstName = "TestFirst";
        const string lastName = "TestLast";
        const string password = "Foo.bar.1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/UserPrograms";
        
        // Arrange
        var jwt = await RegisterNewUser(email, password, firstName, lastName, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        
        // Act
        var response = await _httpClient.SendAsync(request);
        
        // Assert
        Assert.True(response.IsSuccessStatusCode);
        
        // Arrange
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request2 = new HttpRequestMessage(HttpMethod.Get, url);
        request2.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        
        // Act
        var response2 = await _httpClient.SendAsync(request2);
        
        // Assert
        Assert.False(response2.IsSuccessStatusCode);
    }

    [Fact]
    public async Task JwtRenewal()
    {
        const string email = "renewal@test.ee";
        const string firstName = "TestFirst";
        const string lastLame = "TestLast";
        const string password = "Foo.bar.1";
        const int expiresInSeconds = 3;

        const string url = "/api/v1/UserPrograms";
        
        // Arrange
        var jwt = await RegisterNewUser(email, password, firstName, lastLame, expiresInSeconds);
        var jwtResponse = JsonSerializer.Deserialize<JWTResponse>(jwt, _camelCaseJsonSerializerOptions);
        
        // let the jwt expire
        await Task.Delay((expiresInSeconds + 2) * 1000);

        var request = new HttpRequestMessage(HttpMethod.Get, url);
        request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        
        // Act
        var response = await _httpClient.SendAsync(request);
        
        // Assert
        Assert.False(response.IsSuccessStatusCode);
        
        // Arrange
        var refreshUrl = $"/api/v1/identity/account/refreshtoken?expiresInSeconds={expiresInSeconds}";

        var refreshData = new
        {
            JWT = jwtResponse.JWT,
            RefreshToken = jwtResponse.RefreshToken
        };

        var data = JsonContent.Create(refreshData);

        var response2 = await _httpClient.PostAsync(refreshUrl, data);
        var responseContent2 = await response2.Content.ReadAsStringAsync();
        
        Assert.True(response2.IsSuccessStatusCode);

        jwtResponse = JsonSerializer.Deserialize<JWTResponse>(responseContent2, _camelCaseJsonSerializerOptions);

        var request3 = new HttpRequestMessage(HttpMethod.Get, url);
        request3.Headers.Authorization = new AuthenticationHeaderValue("Bearer", jwtResponse!.JWT);
        
        // Act
        var response3 = await _httpClient.SendAsync(request3);
        
        // Assert
        Assert.True(response3.IsSuccessStatusCode);
    }
}