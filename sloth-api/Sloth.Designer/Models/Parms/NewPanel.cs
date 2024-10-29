namespace Sloth.Designer.Models;
public class NewPanel(string panelID, string panelType, List<string>? sections)
{
    public string PanelID { get; set; } = panelID;
    public string PanelType { get; set; } = panelType;
    public List<string>? Sections { get; set; } = sections;
}
