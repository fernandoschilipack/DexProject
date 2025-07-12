using DexApi.Controllers;
using DexApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);
builder.Configuration.AddJsonFile("appsettings.json");

ConfigureServicesBuilder(builder.Services);

// Configure Kestrel server options to handle large file uploads and concurrent connections
builder.WebHost.ConfigureKestrel(options =>
{
    options.Limits.MaxConcurrentConnections = 1000; // Set maximum concurrent connections
});


var app = builder.Build();
app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "DEX API V1");
});

// Map endpoint
app.MapPost("/vdi-dex", DexController.HandleUpload)
     .DisableAntiforgery(); // Disable CSRF protection for this endpoint since it's a file upload endpoint
app.Run();


#region SERVICES
/// Configures the services for the application.
static void ConfigureServicesBuilder(IServiceCollection services)
{
    // Configs
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(opt =>
    {
        opt.EnableAnnotations();
        opt.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "DEX API",
            Version = "v1",
            Description = "API for uploading and processing DEX files."
        });
        opt.AddSecurityDefinition("basic", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "basic",
            In = ParameterLocation.Header,
            Description = "Basic Authentication"
        });
        opt.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "basic"
                }
            },
            Array.Empty<string>()
        }
    });
    });
    services.AddScoped<DexService>();
    services.AddAntiforgery();

}
#endregion


