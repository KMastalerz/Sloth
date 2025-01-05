using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using sloth.API.Constants;
using sloth.API.Middleware;
using sloth.Application.Models.Configuration;
using sloth.Utilities.Constants;

namespace sloth.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

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
        var appSettings = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;

        /* ADD AUTHENTICATION HERE */
        builder.Services.AddAuthentication(option =>
        {
            // Setup authentication options
            option.DefaultAuthenticateScheme = ApiConstants.Bearer;
            option.DefaultScheme = ApiConstants.Bearer;
            option.DefaultChallengeScheme = ApiConstants.Bearer;
        }).AddJwtBearer(config =>
        {
            config.RequireHttpsMetadata = false;
            config.SaveToken = true;
            config.TokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = appSettings.TokenConfiguration.TokenIssuer,
                ValidAudience = appSettings.TokenConfiguration.TokenIssuer,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.TokenConfiguration.TokenKey))
            };
        });
    }
}
