using System.Text;
using System.Text.Json.Serialization;
using Api.ExceptionHandler;
using Infrastructure.Data;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scalar.AspNetCore;
using Serilog;

namespace Api.IoC;

public static class ApiServiceConfigurations
{
    public static void ConfigureApiServices(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddExceptionHandler<DefaultExceptionHandler>();
        services.AddControllers().AddJsonOptions(options =>
            {
                options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
            }
        );
        services.AddOpenApi();
        services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
        ).AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(
                        configuration.GetSection("JwtSettings")["JWT_SECRET_KEY"] ??
                        throw new Exception("The JWT_SECRET_KEY is null"))),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            }
        );
        services.AddAuthorization();
        services.AddCors(options =>
            {
                options.AddPolicy("AllowAllOrigins",
                    builder => builder.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            }
        );
        services.AddProblemDetails();
    }

    public static void UseApiServices(this WebApplication app)
    {
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.MapScalarApiReference(x =>
            {
                x.DarkMode = true;
                x.Theme = ScalarTheme.Kepler;
            });
            using var scope = app.Services.CreateScope();

            var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
            dbContext.Database.Migrate();
        }

        app.UseAuthentication();
        app.UseAuthorization();
        app.UseHttpsRedirection();
        app.UseCors(builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }
        );
        app.MapControllers();
        app.UseSerilogRequestLogging();
        app.UseExceptionHandler(_ => {});
    }
}
