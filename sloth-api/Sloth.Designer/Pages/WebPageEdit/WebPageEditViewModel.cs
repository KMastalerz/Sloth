using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows.Input;
using static Sloth.Designer.Pages.WebPageEditCommands;

namespace Sloth.Designer.Pages;
public class WebPageEditViewModel: BaseViewModel
{

    public WebPageEditViewModel(IDesignerService designerService, IWebPageStateService webPageStateService, MainWindowViewModel mainWindowViewModel)
    {
        GoBack = new GoBack(webPageStateService, mainWindowViewModel);
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




    public ICommand GoBack { get; private set; }
    public ICommand MoveSectionControl { get; private set; }
    public ICommand DragSectionControl { get; private set; }
    public ICommand DropSectionControl { get; private set; }
    public ICommand MovePanelControl { get; private set; }
    public ICommand DragPanelControl { get; private set; }
    public ICommand DropPanelControl { get; private set; }
    public ICommand MoveSection { get; private set; }
    public ICommand DragSection { get; private set; }
    public ICommand DropSection { get; private set; }
    public ICommand MovePanel { get; private set; }
    public ICommand DragPanel { get; private set; }
    public ICommand DropPanel { get; private set; }
}
