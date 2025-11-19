using Application.Options;
using Application.Options.Interfaces;
using Infrastructure.Data;
using Infrastructure.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;

namespace Infrastructure.IoC;

public static class InfrastructureServiceConfiguration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbUrl = configuration.GetConnectionString("mongoDb" );
        if (mongoDbUrl == null)
        {
            throw new ArgumentException("Cannot connect to mongoDb");
        }
        services.AddLogging(builder =>
        {
            builder.ClearProviders();
        });
        services.AddSerilog();
        Log.Logger = new LoggerConfiguration()
                .Enrich.FromLogContext()
                .WriteTo.MongoDBBson(mongoDbUrl, rollingInterval: Serilog.Sinks.MongoDB.RollingInterval.Day)
                .CreateLogger();

        services.AddDbContext<ApplicationDbContext>(options =>
        {
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"));
        });

        services
            .AddOptions<JwtOptions>()
            .Bind(configuration.GetSection("Jwt"))
            .ValidateDataAnnotations()
            .ValidateOnStart();

        services.AddSingleton<IJwtSettings, JwtSettings>();
    }

}