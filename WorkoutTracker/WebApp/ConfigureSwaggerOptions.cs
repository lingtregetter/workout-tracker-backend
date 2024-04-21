using System.Reflection;
using Asp.Versioning.ApiExplorer;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.SwaggerGen;

namespace WebApp;

/// <summary>
/// 
/// </summary>
public class ConfigureSwaggerOptions : IConfigureOptions<SwaggerGenOptions>
{
    private readonly IApiVersionDescriptionProvider _descriptionProvider;

    /// <summary>
    /// 
    /// </summary>
    /// <param name="descriptionProvider"></param>
    public ConfigureSwaggerOptions(IApiVersionDescriptionProvider descriptionProvider)
    {
        _descriptionProvider = descriptionProvider;
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="options"></param>
    public void Configure(SwaggerGenOptions options)
    {
        foreach (var apiVersionDescription in _descriptionProvider.ApiVersionDescriptions)
        {
            options.SwaggerDoc(
                apiVersionDescription.GroupName,
                new OpenApiInfo()
                {
                    Title = $"API {apiVersionDescription.ApiVersion}",
                    Version = apiVersionDescription.ApiVersion.ToString()
                }
            );
        }

        // use fully qualified name for dto descriptions
        options.CustomSchemaIds(t => t.FullName);

        // include xml comments (enable creation in csproj file)
        var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
        options.IncludeXmlComments(xmlPath);

        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
        {
            Description = "foo bar",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement()
        {
            {
                new OpenApiSecurityScheme()
                {
                    Reference = new OpenApiReference()
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    },
                    Scheme = "oauth2",
                    Name = "Bearer",
                    In = ParameterLocation.Header
                },
                new List<string>()
            }
        });
    }
}