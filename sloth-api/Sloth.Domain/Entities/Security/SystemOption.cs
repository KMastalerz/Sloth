namespace Sloth.Domain.Entities;
/// <summary>
/// Table represents set of options on application level
/// Those options will define how application behave in various areas
/// As well will allow to point to some resources like API_KEYS
/// </summary>
public class SystemOption
{
    public string OptionID { get; set; } = default!;
    public string OptionName { get; set;} = default!;
    public string? OptionValue { get; set; } = null;
    public string Description {  get; set; } = default!;
}
