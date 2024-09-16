using Sloth.Domain.Entities;

namespace Sloth.Domain.Repositories.Configuration;
public interface IConfigRepository
{
    Task<IEnumerable<string>?> ListConfigEndpointAsync();
    Task<IEnumerable<SystemOption>?> ListConfigSystemOptionAsync();
}