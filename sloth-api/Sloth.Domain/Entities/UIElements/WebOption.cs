namespace Sloth.Domain.Entities;
public class WebOption
{
    public string Functionality { get; set; } = default!;
    public string OptionKey { get; set; } = default!;
    public string OptionValue { get; set; } = default!;
    public string OptionDescription { get; set; } = default!;
    public DateTime ChangeDate { get; set; } = DateTime.UtcNow!;
    public Guid ChangeUser { get; set; } = default!;
}
