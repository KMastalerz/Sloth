namespace Sloth.Designer.Constants;
public static class PageConstants
{
    public static Dictionary<string, string?> Orientation = new()
    {
        { "Default", null },
        { "Horizonal", "horizontal" },
        { "Vertical", "vertical" }
    };

    public static Dictionary<string, string?> Background = new()
    {
        { "Default", null },
        { "Primary", "primary" },
        { "Standard", "standard" }
    };

    public static Dictionary<string, string?> Position = new()
    {
        { "Default", null },
        { "Center", "center" }
    };
}
