using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Sloth.Domain.Constants;
using Sloth.Domain.DTO;
using Sloth.Domain.Entities;
using Sloth.Domain.Services;
using Sloth.Infrastructure.DatabaseContext;
namespace Sloth.Infrastructure.Services;
internal class ConfigurationService : IConfigurationService
{
    // Configuration service is resposible for delivery of SystemOptions. It's a singleton servics. 
    // It can be updated thru controllers, but to get new values service must be reinitialized.
    public ConfigurationService(IServiceCollection services, IConfiguration configuration)
    {
        // Get default configuration from appsettings.json
        DefaultConfig = configuration.GetSection(ConfigurationKeys.DefaultConfiguration).Get<DefaultConfig>()!;
        // initialize the service
        InitializeAsync(services.BuildServiceProvider().CreateScope());
    }
    private DefaultConfig DefaultConfig { get; set; }
    private List<SystemOption> SystemOptions { get; set; } = [];
    public SecurityConfig SecurityConfig { get; private set; } = default!;
    public SystemOption? GetOption(string optionID)
    {
        return SystemOptions.FirstOrDefault(o => o.OptionID == optionID);
    }
    public IEnumerable<SystemOption> GetOptions(IEnumerable<string> options)
    {
        return SystemOptions.Where(o => options.Contains(o.OptionID));
    }
    private void InitializeAsync(IServiceScope scope)
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<SlothDbContext>();
        {
            if (dbContext.Database.CanConnect())
            {
                SystemOptions = dbContext.SystemOption.ToList();

                var tokenLifetime = DefaultConfig.TokenLifetime;
                var refreshTokenLifetime = DefaultConfig.RefreshTokenLifetime;

                int.TryParse(SystemOptions?.FirstOrDefault(o => o.OptionID == Config.TokenLifetime)?.OptionValue, out tokenLifetime);
                int.TryParse(SystemOptions?.FirstOrDefault(o => o.OptionID == Config.RefreshTokenLifetime)?.OptionValue, out refreshTokenLifetime);

                var tokenIssuer = SystemOptions?.FirstOrDefault(o => o.OptionID == Config.TokenIssuer)?.OptionValue ?? DefaultConfig.TokenIssuer;
                var passwordOptions = SystemOptions?.FirstOrDefault(o => o.OptionID == Config.PasswordOptions)?.OptionValue ?? DefaultConfig.PasswordOptions;

                SecurityConfig = new SecurityConfig(DefaultConfig)
                {
                    TokenLifetime = tokenLifetime,
                    RefreshTokenLifetime = refreshTokenLifetime,
                    TokenIssuer = tokenIssuer,
                    PasswordOptions = passwordOptions
                };
            }
        }
    }
}
