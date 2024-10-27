namespace Sloth.Application.DTO;
public class AppSettings
{
    public Configuration Configuration { get; set; } = default!;
    public SeederOptions SeederOptions { get; set; } = default!;
    public string AllowedHosts { get; set; } = default!;
}
