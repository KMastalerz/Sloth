using Sloth.Domain.DTO;
using Sloth.Domain.Entities;

namespace Sloth.Domain.Services;
public interface IConfigurationService
{
    SecurityConfig SecurityConfig { get; }
    SystemOption? GetOption(string optionID);
    IEnumerable<SystemOption> GetOptions(IEnumerable<string> options);
}