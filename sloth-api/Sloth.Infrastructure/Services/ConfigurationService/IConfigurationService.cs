using Sloth.Application.DTO;

namespace Sloth.Infrastructure.Services;
public interface IConfigurationService
{
    AppSettings GetAppSettings { get; }
}