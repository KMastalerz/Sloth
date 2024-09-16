
namespace Sloth.Domain.Repositories;

public interface IUIElementsRepository
{
    Task<string?> GetSystemOptionAsync(string optionID);
    string? GetSystemOption(string optionID);
}