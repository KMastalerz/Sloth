namespace Sloth.Designer.Constants;
public static class PageConstants
{

    public static Dictionary<string, string?> Background = new()
    {
        { "Default", null },
        { "Primary", "primary" },
        { "Standard", "standard" }
    };

    public static Dictionary<string, string> AreaTypes = new()
    {
        { "Column", "column" },
        { "Row", "row" }
    };
}
