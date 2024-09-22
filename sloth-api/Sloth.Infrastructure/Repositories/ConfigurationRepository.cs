using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class ConfigurationRepository(SlothDbContext dbContext) : IConfigurationRepository
{
    public async Task<SystemOption?> GetConfigurationAsync(string systemOption)
    {
        return await dbContext.SystemOption.SingleOrDefaultAsync(c => c.OptionName == systemOption);
    }

    public async Task<IEnumerable<SystemOption>> ListConfigurationAsync(IEnumerable<string> systemOptions)
    {
        return await dbContext.SystemOption.Where(c => systemOptions.Contains(c.OptionName)).ToListAsync();
    }
}
