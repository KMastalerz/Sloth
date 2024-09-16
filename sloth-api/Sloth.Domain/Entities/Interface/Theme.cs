
namespace Sloth.Domain.Entities;
/// <summary>
/// This table will define theme colors preferred by user.
/// Saves format in hsl. Fron-end app, will calculate shades and tints. 
/// </summary>
public class Theme
{
    public int ThemeID { get; set; }
    public string ThemeName { get; set; } = default!;
    public string PrimaryColor { get; set; } = default!; 
    public string SecondaryColor { get; set;} = default!;
    public string BackGroundColor { get; set; } = default!;
    public string NeutralColor { get; set; } = default!;
    public string ErrorColor { get; set; } = default!;
    public string WarningColor { get; set; } = default!;
    public string InformationColor { get; set; } = default!;
    public string SuccessColor { get; set; } = default!;

    public string FontSize { get; set; } = default!;
}
