namespace Sloth.Designer.Constants;

public static class PanelConstants
{
    public static List<PanelElement> Panels = [
        new("Form", "form", false),
        new("Header", "header", false),
        new("Side Nav", "sideNav", true)
    ];

    public static List<string> PanelChildrenTypes = ["Section", "Control"];
    public static string PanelChildrenLabel = "Item type";

    public static string PanelTypesLabel = "Panel Type";
    public static string PanelLabel = "Panel ID";
}

public class PanelElement(string panelName, string panelType, bool hasSections = false)
{
    public string PanelName { get; set; } = panelName;
    public string PanelType { get; set; } = panelType;
    public bool HasSections { get; set; } = hasSections;
}



