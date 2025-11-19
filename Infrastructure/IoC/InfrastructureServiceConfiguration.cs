using Domain.Models;
using Infrastructure.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Serilog;
using Serilog.Sinks.MongoDB;

namespace Infrastructure.IoC;

public static class InfrastructureServiceConfiguration
{
    public static void ConfigureInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
    {
        var mongoDbUrl = configuration.GetValue<string>("mongoDbUrl");
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

    }

}