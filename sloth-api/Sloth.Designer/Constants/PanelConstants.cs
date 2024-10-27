using Sloth.Designer.Enums;

namespace Sloth.Designer.Constants;

public static class PanelConstants
{
    public static List<PanelElement> PanelTypes = [
        new("Flex form", "flexForm", PanelSectionType.DynamicSections),
        new("Form", "form"),
        new("Grid form", "gridForm", PanelSectionType.DynamicSections),
        new("Header", "header"),
        new("Side nav", "sideNav", PanelSectionType.DynamicSections),
        // TODO: Remove test sections
        new("Test", "test", PanelSectionType.StaticSections, ["Section One", "Section Two"]),
        new("Test 2", "test2", PanelSectionType.StaticSections, ["Section One"])
    ];

    public static List<string> AddPanelOptions = ["Section", "Control"];
}

public static class PanelOption
{
    public static string Section = "Section";
    public static string Control = "Control";
}

public class PanelElement(string panelName, string panelType, PanelSectionType sectionType = PanelSectionType.NoSections, List<string>? panelSections = null)
{
    public string PanelName { get; set; } = panelName;
    public string PanelType { get; set; } = panelType;
    public PanelSectionType SectionType { get; set; } = sectionType;
    public List<string>? PanelSections { get; set; } = panelSections;
}



