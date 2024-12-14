using Sloth.Application.Models;

namespace Sloth.Infrastructure.Services;
public interface IConfigurationService
{
    AppSettings GetAppSettings { get; }
}