using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddElementCommands;

namespace Sloth.Designer.Pages;

public class AddElementViewModel: BaseViewModel
{
    private readonly IWebPageStateService webPageStateService;

    public AddElementViewModel(IWebPageStateService webPageStateService)
    {
        this.webPageStateService = webPageStateService;
        Initialize();

        AddNewElement = new AddNewElement(webPageStateService);
    }



    private void Initialize()
    {
        switch (webPageStateService.AddElementType)
        {
            case AddElementType.Panel:
                DisplayAsPanel();
                break;
            case AddElementType.PanelChildren:
                var panelType = webPageStateService.WebPanel!.PanelType;
                var hasSections = PanelConstants.Panels.FirstOrDefault(p => p.PanelType == panelType)?.HasSections ?? false;

                ShowPanelItemTypeSelection = hasSections;

                if (hasSections) DisplayAsSection();
                else DisplayAsControl();

                break;
            case AddElementType.SectionControl:
                DisplayAsControl();
                break;
        }
    }
    private void PanelItemTypeChanged()
    {
        if (childType == "Section")
        {
            ElementLabel = SectionConstants.SectionLabel;
            ShowElementTypeSelection = false;
        }
        else
        {
            ElementLabel = ControlConstants.ControlLabel;
            ElementTypes = ControlConstants.Controls.Select(c => c.ControlName).ToList();
            ElementTypesLabel = ControlConstants.ControlTypesLabel;
            ShowElementTypeSelection = true;
        }

    }

    private void DisplayAsPanel()
    {
        ShowElementTypeSelection = true;
        ElementLabel = PanelConstants.PanelLabel;
        ElementTypes = PanelConstants.Panels.Select(p => p.PanelName).ToList();
        ElementTypesLabel = PanelConstants.PanelTypesLabel;
    }
    private void DisplayAsControl()
    {
        ShowElementTypeSelection = true;
        ElementLabel = ControlConstants.ControlLabel;
        ElementTypes = ControlConstants.Controls.Select(c => c.ControlName).ToList();
        ElementTypesLabel = ControlConstants.ControlTypesLabel;
    }
    private void DisplayAsSection()
    {
        ShowElementTypeSelection = false;
        ChildrenTypes = PanelConstants.PanelChildrenTypes;
        ChildrenTypesLabel = PanelConstants.PanelChildrenLabel;
    }

    private bool showPanelItemTypeSelection = false;
    public bool ShowPanelItemTypeSelection
    {
        get => showPanelItemTypeSelection;
        set => SetProperty(ref showPanelItemTypeSelection, value);
    }

    private bool showElementTypeSelection = false;
    public bool ShowElementTypeSelection
    {
        get => showElementTypeSelection;
        set => SetProperty(ref showElementTypeSelection, value);
    }


    private string elementLabel = string.Empty;
    public string ElementLabel
    {
        get => elementLabel;
        set => SetProperty(ref elementLabel, value);
    }

    private string elementID = string.Empty;
    public string ElementID
    {
        get => elementID;
        set => SetProperty(ref elementID, value);
    }


    private string elementType = string.Empty;
    public string ElementType
    {
        get => elementType;
        set => SetProperty(ref elementType, value);
    }

    private List<string> elementTypes = [];
    public List<string> ElementTypes
    {
        get => elementTypes;
        set => SetProperty(ref elementTypes, value);
    }

    private string elementTypesLabel = string.Empty;
    public string ElementTypesLabel
    {
        get => elementTypesLabel;
        set => SetProperty(ref elementTypesLabel, value);
    }


    private string childType = string.Empty;
    public string ChildType
    {
        get => childType;
        set
        {
            SetProperty(ref childType, value);
            PanelItemTypeChanged();
        }
    }

    private List<string> childrenTypes = [];
    public List<string> ChildrenTypes
    {
        get => childrenTypes;
        set => SetProperty(ref childrenTypes, value);
    }

    private string childrenTypesLabel = string.Empty;
    public string ChildrenTypesLabel
    {
        get => childrenTypesLabel;
        set => SetProperty(ref childrenTypesLabel, value);
    }



    public ICommand AddNewElement { get; }


}
