using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.WebPageEditCommands;

namespace Sloth.Designer.Pages;
public class WebPageEditViewModel: BaseViewModel
{

    public WebPageEditViewModel(IDesignerService designerService, IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel, IMainWindowService mainWindowService)
    {
        GoBack = new GoBack(webPageStateService, mainPageViewModel);
        MoveSectionControl = new MoveSectionControl(this);
        DragSectionControl = new DragSectionControl(this);
        DropSectionControl = new DropSectionControl(this);
        MovePanelControl = new MovePanelControl(this);
        DragPanelControl = new DragPanelControl(this);
        DropPanelControl = new DropPanelControl(this);
        MoveSection = new MoveSection(this);
        DragSection = new DragSection(this);
        DropSection = new DropSection(this);
        MovePanel = new MovePanel();
        DragPanel = new DragPanel(this);
        DropPanel = new DropPanel();

        AddPanelCommand = new AddPanelCommand(mainWindowService, webPageStateService);
        AddPanelChildrenCommand = new AddPanelChildrenCommand(mainWindowService, webPageStateService);
        AddSectionControlCommand = new AddSectionControlCommand(mainWindowService, webPageStateService);

        DeletePanelCommand = new DeletePanelCommand(webPageStateService);
        DeleteSectionCommand = new DeleteSectionCommand(webPageStateService);
        DeletePanelControlCommand = new DeletePanelControlCommand(webPageStateService);
        DeleteSectionControlCommand = new DeleteSectionControlCommand(webPageStateService);

        WebPage = webPageStateService.WebPage;
    }

    private WebPageItem? webPage; 
    public WebPageItem? WebPage
    {
        get => webPage;
        set => SetProperty(ref webPage, value);
    }

    private WebSectionItem? parentSection;
    public WebSectionItem? ParentSection
    {
        get => parentSection;
        set => SetProperty(ref parentSection, value);
    }

    private WebPanelItem? parentPanel;
    public WebPanelItem? ParentPanel
    {
        get => parentPanel;
        set => SetProperty(ref parentPanel, value);
    }

    private UserControl? editControl = null;
    public UserControl? EditControl
    {
        get => editControl;
        set => SetProperty(ref editControl, value);
    }

    public ICommand GoBack { get; }
    public ICommand MoveSectionControl { get; }
    public ICommand DragSectionControl { get; }
    public ICommand DropSectionControl { get; }
    public ICommand MovePanelControl { get; }
    public ICommand DragPanelControl { get; }
    public ICommand DropPanelControl { get; }
    public ICommand MoveSection { get; }
    public ICommand DragSection { get; }
    public ICommand DropSection { get; }
    public ICommand MovePanel { get; }
    public ICommand DragPanel { get; }
    public ICommand DropPanel { get; }
    public ICommand AddPanelCommand { get; }
    public ICommand AddPanelChildrenCommand { get; }
    public ICommand AddSectionControlCommand { get; }
    public ICommand DeletePanelCommand { get; }
    public ICommand DeleteSectionCommand { get; }
    public ICommand DeletePanelControlCommand { get; }
    public ICommand DeleteSectionControlCommand { get; }
}
