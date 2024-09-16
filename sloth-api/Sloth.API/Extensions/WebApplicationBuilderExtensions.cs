using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Serilog;
using Sloth.Infrastructure.DTO;
using Sloth.Shared.Helpers;

namespace Sloth.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    { 
        builder.Services.AddAuthentication();
        builder.Services.AddControllers();
        builder.Services.AddSwaggerGen(c =>
        {
            //this config adds authorize to swagger
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = "Bearer"
            });

            c.AddSecurityRequirement(new OpenApiSecurityRequirement
            {
                {
                    new OpenApiSecurityScheme
                    {
                        Reference = new OpenApiReference{
                            Type = ReferenceType.SecurityScheme,
                            Id = "bearerAuth"
                        }
                    },
                    []
                }
            });
        });

        /* REGISTER MIDDLEWARE HERE */

        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }
    public static void SetTokenConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<BearerTokenOptions>(IdentityConstants.BearerScheme).Configure<IServiceProvider>((options, serviceProvider) =>
        {
            var appConfig = serviceProvider.GetRequiredService<AppConfig>();

            options.BearerTokenExpiration = TimeSpan.FromMinutes(appConfig.TokenLifetime);
            options.RefreshTokenExpiration = TimeSpan.FromMinutes(appConfig.RefreshTokenLifetime);
        });
    }
    public static void SetIdentityConfig(this WebApplicationBuilder builder)
    {
        builder.Services.AddOptions<IdentityOptions>(IdentityConstants.ApplicationScheme).Configure<IServiceProvider>((options, serviceProvider) =>
        {
            var appConfig = serviceProvider.GetRequiredService<AppConfig>();
            // Convert and set password complexity options
            var passwordOptions = appConfig.PasswordComplexity.TryConvert(new PasswordOptions());
            options.Password = passwordOptions;

            options.User.RequireUniqueEmail = true;
        });
    }
}
