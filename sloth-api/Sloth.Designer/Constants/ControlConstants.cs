namespace Sloth.Designer.Constants;
public static class ControlConstants
{
    public static List<ControlElement> ControlTypes = [
        new("Button", "button"),
        new("Input", "input"),
        new("Link", "link"),
        new("Password", "password")
    ];
}

public class ControlElement(string controlName, string controlType)
{
    public string ControlName { get; set; } = controlName;
    public string ControlType { get; set; } = controlType;
}

public static class ControlInnerTypes
{
    public static Dictionary<string, string> Button = new()
    {
        { "Icon", "icon" },
        { "textIcon", "textIcon" },
        { "Text", "text" }
    };
    public static Dictionary<string, string> Link = new()
    {
        { "Header Nav", "headerNav" },
        { "Side Nav", "sideNav" },
        { "Simple Link", "simpleLink" }
    };
}

public static class ControlStyles
{
    public static Dictionary<string, string> Button = new()
    {
        { "Flat", "flat" },
        { "Outlined", "outlined" },
        { "Primary", "primary" },
        { "Secondary", "secondary" }
    };
}

public static class ControlSizes
{
    public static Dictionary<string, string> Button = new()
    {
        { "Small", "small" },
        { "Medium", "medium" },
        { "Large", "large" }
    };
}
