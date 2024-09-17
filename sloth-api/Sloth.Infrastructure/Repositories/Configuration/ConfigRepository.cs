using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Constants;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories.Configuration;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories.Configuration;

internal class ConfigRepository(SlothDbContext dbContext) : IConfigRepository
{
    public async Task<IEnumerable<SystemOption>?> ListConfigSystemOptionAsync()
    {
        List<string> systemOptionList = [
            ConfigSystemOptions.TokenLifetime,
            ConfigSystemOptions.RefreshTokenLifetime,
            ConfigSystemOptions.PasswordComplexity
            ];
        return await dbContext.SystemOption.Where(so => systemOptionList.Any(ol => ol == so.OptionID)).ToListAsync();
    }

    public async Task<IEnumerable<string>?> ListConfigEndpointAsync()
    {
        return await dbContext.EndpointConfiguration.Where(e => e.Active == true).Select(e => e.EndpointID).ToListAsync();
    }
}
