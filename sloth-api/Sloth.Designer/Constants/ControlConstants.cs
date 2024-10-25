namespace Sloth.Designer.Constants;
public static class ControlConstants
{
    public static List<ControlElement> Controls = [
        new("Button", "button"),
        new("Input", "input"),
        new("Link", "link"),
        new("Password", "password")
    ];

    public static string ControlLabel = "Control ID";
    public static string ControlTypesLabel = "Control Type";
}

public class ControlElement(string controlName, string controlType)
{
    public string ControlName { get; set; } = controlName;
    public string ControlType { get; set; } = controlType;
}