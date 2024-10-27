using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddSectionCommands;

namespace Sloth.Designer.Pages;
public class AddSectionViewModel: BaseViewModel
{
    private readonly IWebPageStateService webPageStateService;
    public AddSectionViewModel(IWebPageStateService webPageStateService)
    {
        this.webPageStateService = webPageStateService;

        var panelType = webPageStateService.WebPanel?.PanelType;
        var panelSectionType = PanelConstants.PanelTypes.FirstOrDefault(p => p.PanelType == panelType)?.SectionType;

        ShowSectionSelect = panelSectionType == PanelSectionType.StaticSections;
        ShowSectionInsert = !ShowSectionSelect;

        AddSectionCommand = new AddSectionCommand(webPageStateService);
    }

    public bool ShowSectionSelect { get; } = false;
    public bool ShowSectionInsert { get; } = false;

    private List<string> sectionTypes = [];
    public List<string> SectionTypes
    {
        get => sectionTypes;
        set => SetProperty(ref sectionTypes, value);
    }

    private string? sectionID = null;
    public string? SectionID
    {
        get => sectionID;
        set => SetProperty(ref sectionID, value);
    }

    public ICommand AddSectionCommand { get; }
}
