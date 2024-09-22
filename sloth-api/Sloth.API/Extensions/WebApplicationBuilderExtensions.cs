using Microsoft.OpenApi.Models;
using Serilog;
using Sloth.API.Middlewares;
using Sloth.Domain.Constants;

namespace Sloth.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void ReadConfiguration(this WebApplicationBuilder builder)
    {
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });
    }

    public static void AddPresentation(this WebApplicationBuilder builder)
    { 
        builder.Services.AddControllers();

        builder.Services.AddSwaggerGen(c =>
        {
            // This config adds authorize to swagger
            c.AddSecurityDefinition("bearerAuth", new OpenApiSecurityScheme
            {
                Type = SecuritySchemeType.Http,
                Scheme = AuthScheme.Bearer
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
        builder.Services.AddScoped<ErrorHandlingMiddleware>();
    }
}
