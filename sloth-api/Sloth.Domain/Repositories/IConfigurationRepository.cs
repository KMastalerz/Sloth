using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories;
public interface IConfigurationRepository
{
    Task<SystemOption?> GetConfigurationAsync(string systemOption);
    Task<IEnumerable<SystemOption>> ListConfigurationAsync(IEnumerable<string> systemOptions);
}