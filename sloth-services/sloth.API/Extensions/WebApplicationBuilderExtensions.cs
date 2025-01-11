using Microsoft.IdentityModel.Tokens;
using Serilog;
using System.Text;
using sloth.API.Constants;
using sloth.API.Middleware;
using sloth.Application.Models.Configuration;
using sloth.Utilities.Constants;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.AspNetCore.Http.Features;

namespace sloth.API.Extensions;

public static class WebApplicationBuilderExtensions
{
    public static void AddPresentation(this WebApplicationBuilder builder)
    {
        builder.Services.AddControllers();

        // Increase max request body size
        builder.Services.Configure<KestrelServerOptions>(options =>
        {
            options.Limits.MaxRequestBodySize = 110_100_000; // ~105 MB
        });

        // Configure FormOptions to handle large file uploads
        builder.Services.Configure<FormOptions>(options =>
        {
            options.MultipartBodyLengthLimit = 104_857_600; // 100 MB
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
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSettings.TokenConfiguration.TokenKey)),
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });
    }
}
