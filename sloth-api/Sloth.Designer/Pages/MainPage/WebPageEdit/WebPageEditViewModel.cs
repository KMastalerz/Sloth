﻿using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using static Sloth.Designer.Pages.WebPageEditCommands;

namespace Sloth.Designer.Pages;
public class WebPageEditViewModel : BaseViewModel
{

    public WebPageEditViewModel(IDesignerService designerService, IWebPageStateService webPageStateService, IWindowService windowService, MainPageViewModel mainPageViewModel)
    {
        GoBack = new GoBack(webPageStateService, mainPageViewModel);
        SaveWebPage = new SaveWebPage(webPageStateService, designerService, windowService);
        DeleteWebPage = new DeleteWebPage(webPageStateService, designerService, windowService, mainPageViewModel);
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

        AddPanelCommand = new AddPanelCommand(windowService, webPageStateService);
        AddSectionCommand = new AddSectionCommand(windowService, webPageStateService);
        AddControlCommand = new AddControlCommand(windowService, webPageStateService);

        DeletePanelCommand = new DeletePanelCommand(webPageStateService);
        DeleteSectionCommand = new DeleteSectionCommand(webPageStateService);
        DeleteControlCommand = new DeleteControlCommand(webPageStateService);

        EditPageCommand = new EditPageCommand(this);
        EditPanelCommand = new EditPanelCommand(webPageStateService, this);
        EditSectionCommand = new EditSectionCommand(webPageStateService, this);
        EditControlCommand = new EditControlCommand(webPageStateService, this);

        WebPage = webPageStateService.WebPage;
        webPageStateService.RegisterCallback<WebPageItem>(OnWebPageChange);
    }

    private void OnWebPageChange(WebPageItem webPage)
    {
        Application.Current.Dispatcher.Invoke(() =>
        {
            WebPage = webPage;
        });
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
    public IAsyncCommand SaveWebPage { get; }
    public IAsyncCommand DeleteWebPage { get; }
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
    public ICommand AddSectionCommand { get; }
    public ICommand AddControlCommand { get; }
    public ICommand DeletePanelCommand { get; }
    public ICommand DeleteSectionCommand { get; }
    public ICommand DeleteControlCommand { get; }
    public ICommand EditPageCommand { get; }
    public ICommand EditPanelCommand { get; }
    public ICommand EditSectionCommand { get; }
    public ICommand EditControlCommand { get; }
}
