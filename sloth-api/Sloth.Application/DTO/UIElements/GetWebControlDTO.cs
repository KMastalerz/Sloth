namespace Sloth.Application.DTO;
public class GetWebControlDTO
{
    public string ControlID { get; set; } = default!;
    public string ControlType { get; set; } = default!;
    public string ControlLabel { get; set; } = default!;
    public string ControlPlaceholder { get; set; } = default!;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public string? MetaData { get; set; } = null;
    public bool IsHidden { get; set; } = false;
    public bool IsReadOnly { get; set; } = false;
    public bool IsDisabled { get; set; } = false;
}
