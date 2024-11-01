using Sloth.Designer.Enums;

namespace Sloth.Designer.Constants;

public static class PanelConstants
{
    public static List<PanelElement> PanelTypes = [
        new("Flex form", "flexForm"),
        new("Form", "form"),
        new("Grid form", "gridForm"),
        new("Header", "header"),
        new("Side nav", "sideNav"),
    ];

    public static List<string> AddPanelOptions = ["Section", "Control"];
}

public static class PanelOption
{
    public static string Section = "Section";
    public static string Control = "Control";
}

public class PanelElement(string panelName, string panelType)
{
    public string PanelName { get; set; } = panelName;
    public string PanelType { get; set; } = panelType;
}



