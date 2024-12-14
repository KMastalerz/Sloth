using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Sloth.API.Middlewares;
using Sloth.Application.Models;
using Sloth.Domain.Constants;
using System.Text;

namespace Sloth.API.Extensions;

public static class WebApplicationBuilderExtensions
{
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


        /* READ CONFIG */
        builder.Host.UseSerilog((context, configuration) =>
        {
            configuration.ReadFrom.Configuration(context.Configuration);
        });


    }

    public static void AddAuthentication(this WebApplicationBuilder builder, IConfiguration configuration)
    {
        var secConfig = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;

        /* ADD AUTHENTICATION HERE */
        builder.Services.AddAuthentication(option =>
        {
            // Setup authentication options
            option.DefaultAuthenticateScheme = AuthScheme.Bearer;
            option.DefaultScheme = AuthScheme.Bearer;
            option.DefaultChallengeScheme = AuthScheme.Bearer;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = secConfig.TokenIssuer,
                ValidAudience = secConfig.TokenIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secConfig.TokenKey))
            };
        });
    }
}


