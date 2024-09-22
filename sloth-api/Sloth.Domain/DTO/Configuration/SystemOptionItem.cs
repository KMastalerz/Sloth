using Sloth.Domain.Entities;

namespace Sloth.Domain.DTO;
public class SystemOptionItem (SystemOption option)
{
    public string OptionID { get; } = option.OptionID;
    public string? OptionValue { get; } = option.OptionValue;
}
