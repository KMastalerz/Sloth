namespace Sloth.Application.DTO.UIElements;
public class GetWebPageControlDTO
{
    public string ControlID { get; set; } = default!;
    public string ControlType { get; set; } = default!;
    public string ControlLabel { get; set; } = default!;
    public string ControlPlaceholder { get; set; } = default!;
    public string? Route { get; set; } = null;
    public string? RoutePageID { get; set; } = null;
    public bool? IsHidden { get; set; } = null;
    public bool? IsReadOnly { get; set; } = null;
    public bool? IsDisabled { get; set; } = null;
}
