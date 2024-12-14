using Microsoft.Extensions.Configuration;
using Sloth.Application.Models;
using Sloth.Domain.Constants;

namespace Sloth.Infrastructure.Services;
internal class ConfigurationService : IConfigurationService
{
    public ConfigurationService(IConfiguration configuration)
    {
        var configurationSetup = configuration.GetSection(ConfigurationKeys.Configuration).Get<Configuration>()!;
        var seederOptions = configuration.GetSection(ConfigurationKeys.SeederOptions).Get<SeederOptions>()!;
        var allowedHosts = configuration.GetSection(ConfigurationKeys.AllowedHosts).Get<string>()!;

        appSettings = new AppSettings
        {
            Configuration = configurationSetup,
            SeederOptions = seederOptions,
            AllowedHosts = allowedHosts
        };
    }
    private readonly AppSettings appSettings;
    public AppSettings GetAppSettings
    {
        get { return appSettings; }
    }
}
