using Microsoft.EntityFrameworkCore;
using Sloth.Domain.Entities;
using Sloth.Domain.Exceptions;
using Sloth.Domain.Repositories;
using Sloth.Infrastructure.DatabaseContext;

namespace Sloth.Infrastructure.Repositories;
internal class UIElementsRepository(SlothDbContext dbContext) : IUIElementsRepository
{
    public string? GetSystemOption(string optionID)
    {
        var option = dbContext.SystemOption.FirstOrDefault(so => so.OptionID == optionID) ??
            throw new NotFoundException(nameof(SystemOption), optionID);

        return option.OptionValue;
    }

    public async Task<string?> GetSystemOptionAsync(string optionID)
    {
        var option = await dbContext.SystemOption.FirstOrDefaultAsync(so => so.OptionID == optionID) ??
            throw new NotFoundException(nameof(SystemOption), optionID);

        return option.OptionValue;
    }
}
