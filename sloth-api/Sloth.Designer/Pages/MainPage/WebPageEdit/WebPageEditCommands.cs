﻿using Sloth.Designer.Services;
using System.Windows;
using System.Windows.Input;
using Sloth.Designer.Enums;
using Sloth.Designer.Core;
using Sloth.Designer.Models;

namespace Sloth.Designer.Pages;
public class WebPageEditCommands
{
    public class GoBack(IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.WebPage = null;
            mainPageViewModel.MainPageControl = new WebPageSearch();
        }
    }
    public class SaveWebPage(IWebPageStateService webPageStateService, IDesignerService designerService, IWindowService windowService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => webPageStateService.WebPage is not null;

        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (webPageStateService.WebPage is null) return;
            if (parameter is not WebPageEditViewModel webPageEditViewModel) return;
            var webPage = webPageStateService.WebPage;

            webPage.Panels = string.Join(',', webPage.WebPanels.Select(p => p.PanelID));

            foreach (var panel in webPage.WebPanels)
            {
                panel.Sections = string.Join(',', panel.WebSections.Select(s => s.SectionID));

                foreach (var section in panel.WebSections)
                {
                    section.Controls = string.Join(',', section.WebControls.Select(c => c.ControlID));
                }
            }

            var response = await designerService.SaveFullWebPage(webPage);

            if (response?.Success ?? false)
            {
                webPageStateService.WebPage = response.Data;
                await windowService.ShowSuccessAsync("Page saved successfully!");
            }
            else
            {
                await windowService.ShowErrorAsync("Cannot save page!");
            }
        }
    }
    public class DeleteWebPage(IWebPageStateService webPageStateService, IDesignerService designerService, IWindowService windowService, MainPageViewModel mainPageViewModel) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => webPageStateService.WebPage is not null;

        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (webPageStateService.WebPage is null) return;
            var webPage = webPageStateService.WebPage;

            var response = await designerService.DeleteWebPage(webPage.AppID, webPage.PageID);

            if (response?.Success ?? false)
            {
                await Application.Current.Dispatcher.InvokeAsync(async () =>
                {
                    mainPageViewModel.MainPageControl = new WebPageSearch();

                    var parms = (mainPageViewModel.MainPageControl.DataContext as WebPageSearchViewModel)!;


                    var response = await designerService.ListWebPageByID(parms.AppID, parms.PageID);

                    if (response?.Success == true)
                    {
                        webPageStateService.WebPages = response.Data!;
                    }
                    else
                    {
                        webPageStateService.WebPages = [];
                    }
                });

                await windowService.ShowSuccessAsync("Page removed");
            }
            else
            {
                await windowService.ShowErrorAsync("Cannot delete page!");
            }

            await webPageStateService.RefreshWebApplications();
        }
    }

    #region [Section Control]
    public class MoveSectionControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var control = parameters.Item1 as WebControlItem;
                var mouseEventArgs = parameters.Item2 as MouseEventArgs;

                if (mouseEventArgs?.LeftButton == MouseButtonState.Pressed)
                {
                    // Get the framework element that initiated the drag (the control's UI element)
                    var frameworkElement = mouseEventArgs.Source as FrameworkElement;

                    if (frameworkElement != null)
                    {
                        webPageEditViewModel.ParentSection = webPageEditViewModel.WebPage?.WebPanels.FirstOrDefault(p => p.PanelID == control!.PanelID)?.WebSections.FirstOrDefault(s => s.SectionID == control!.SectionID);
                        // Initiate drag-and-drop operation
                        DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, control), DragDropEffects.Move);
                    }
                }
            }
        }
    }
    public class DragSectionControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (webPageEditViewModel.ParentSection is null) return;
            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var control = parameters.Item1 as WebControlItem;
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                // if different type of event return
                if (dragEventArgs is null) return;


                // if the data is not a WebControlItem return
                if (dragEventArgs?.Data.GetData(DataFormats.Serializable).GetType() != typeof(WebControlItem))
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }

                var movedControl = dragEventArgs?.Data.GetData(DataFormats.Serializable) as WebControlItem;

                // if any data is null return
                if (control is null || movedControl is null) return;

                // if SectionID is different return
                if (control.SectionID != movedControl.SectionID)
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }


                dragEventArgs!.Effects = DragDropEffects.Move;
                if (dragEventArgs != null)
                {
                    if (control != movedControl)
                    {

                        var observableControlList = webPageEditViewModel.ParentSection.WebControls;

                        var indexMoved = observableControlList.IndexOf(movedControl);
                        var indexOver = observableControlList.IndexOf(control);

                        // replace the control in the list
                        observableControlList.Remove(movedControl);
                        observableControlList.Remove(control);

                        if (indexOver < indexMoved)
                        {
                            observableControlList.Insert(indexOver, movedControl);
                            observableControlList.Insert(indexMoved, control);
                        }
                        else
                        {
                            observableControlList.Insert(indexMoved, control);
                            observableControlList.Insert(indexOver, movedControl);
                        }
                    }

                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.Move;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    public class DropSectionControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                webPageEditViewModel.ParentSection = null;

                if (dragEventArgs is not null)
                {
                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.None;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    #endregion

    #region [Panel Control]
    public class MovePanelControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var control = parameters.Item1 as WebControlItem;
                var mouseEventArgs = parameters.Item2 as MouseEventArgs;

                if (mouseEventArgs?.LeftButton == MouseButtonState.Pressed)
                {
                    // Get the framework element that initiated the drag (the control's UI element)
                    var frameworkElement = mouseEventArgs.Source as FrameworkElement;

                    if (frameworkElement != null)
                    {
                        webPageEditViewModel.ParentPanel = webPageEditViewModel.WebPage?.WebPanels.FirstOrDefault(p => p.PanelID == control!.PanelID);
                        // Initiate drag-and-drop operation
                        DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, control), DragDropEffects.Move);
                    }
                }
            }
        }
    }
    public class DragPanelControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (webPageEditViewModel.ParentPanel is null) return;
            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var control = parameters.Item1 as WebControlItem;
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                // if different type of event return
                if (dragEventArgs is null) return;


                // if the data is not a WebControlItem return
                if (dragEventArgs?.Data.GetData(DataFormats.Serializable).GetType() != typeof(WebControlItem))
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }

                var movedControl = dragEventArgs?.Data.GetData(DataFormats.Serializable) as WebControlItem;

                // if any data is null return
                if (control is null || movedControl is null) return;

                // if SectionID is different return
                if (control.PanelID != movedControl.PanelID || control.SectionID != movedControl.SectionID)
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }
            }
        }
    }
    public class DropPanelControl(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                webPageEditViewModel.ParentPanel = null;

                if (dragEventArgs is not null)
                {
                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.None;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    #endregion

    #region [Section]
    public class MoveSection(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var section = parameters.Item1 as WebSectionItem;
                var mouseEventArgs = parameters.Item2 as MouseEventArgs;

                if (mouseEventArgs?.LeftButton == MouseButtonState.Pressed)
                {
                    // Get the framework element that initiated the drag (the control's UI element)
                    var frameworkElement = mouseEventArgs.Source as FrameworkElement;

                    if (frameworkElement != null)
                    {
                        webPageEditViewModel.ParentPanel = webPageEditViewModel.WebPage?.WebPanels.FirstOrDefault(p => p.PanelID == section!.PanelID);
                        // Initiate drag-and-drop operation
                        DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, section), DragDropEffects.Move);
                    }
                }
            }
        }
    }
    public class DragSection(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (webPageEditViewModel.ParentPanel is null) return;
            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var section = parameters.Item1 as WebSectionItem;
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                // if different type of event return
                if (dragEventArgs is null) return;


                // if the data is not a WebSectionItem return
                if (dragEventArgs?.Data.GetData(DataFormats.Serializable).GetType() != typeof(WebSectionItem))
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }

                var movedSection = dragEventArgs?.Data.GetData(DataFormats.Serializable) as WebSectionItem;

                // if any data is null return
                if (section is null || movedSection is null) return;

                // if SectionID is different return
                if (section.PanelID != movedSection.PanelID)
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }


                dragEventArgs!.Effects = DragDropEffects.Move;
                if (dragEventArgs != null)
                {
                    if (section != movedSection)
                    {

                        var observableSectionList = webPageEditViewModel.ParentPanel.WebSections;

                        var indexMoved = observableSectionList.IndexOf(movedSection);
                        var indexOver = observableSectionList.IndexOf(section);

                        // replace the control in the list
                        observableSectionList.Remove(movedSection);
                        observableSectionList.Remove(section);

                        if (indexOver < indexMoved)
                        {
                            observableSectionList.Insert(indexOver, movedSection);
                            observableSectionList.Insert(indexMoved, section);
                        }
                        else
                        {
                            observableSectionList.Insert(indexMoved, section);
                            observableSectionList.Insert(indexOver, movedSection);
                        }
                    }

                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.Move;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    public class DropSection(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                webPageEditViewModel.ParentPanel = null;

                if (dragEventArgs is not null)
                {
                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.None;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    #endregion

    #region [Panel]
    public class MovePanel() : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var panel = parameters.Item1 as WebPanelItem;
                var mouseEventArgs = parameters.Item2 as MouseEventArgs;

                if (mouseEventArgs?.LeftButton == MouseButtonState.Pressed)
                {
                    // Get the framework element that initiated the drag (the control's UI element)
                    var frameworkElement = mouseEventArgs.Source as FrameworkElement;

                    if (frameworkElement != null)
                    {
                        // Initiate drag-and-drop operation
                        DragDrop.DoDragDrop(frameworkElement, new DataObject(DataFormats.Serializable, panel), DragDropEffects.Move);
                    }
                }
            }
        }
    }
    public class DragPanel(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (webPageEditViewModel.WebPage is null) return;
            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var panel = parameters.Item1 as WebPanelItem;
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                // if different type of event return
                if (dragEventArgs is null) return;


                // if the data is not a WebPanelItem return
                if (dragEventArgs?.Data.GetData(DataFormats.Serializable).GetType() != typeof(WebPanelItem))
                {
                    dragEventArgs!.Effects = DragDropEffects.None;
                    return;
                }

                var movedPanel = dragEventArgs?.Data.GetData(DataFormats.Serializable) as WebPanelItem;

                // if any data is null return
                if (panel is null || movedPanel is null) return;

                dragEventArgs!.Effects = DragDropEffects.Move;
                if (dragEventArgs != null)
                {
                    if (panel != movedPanel)
                    {

                        var observablePanelList = webPageEditViewModel.WebPage!.WebPanels;

                        var indexMoved = observablePanelList.IndexOf(movedPanel);
                        var indexOver = observablePanelList.IndexOf(panel);

                        // replace the control in the list
                        observablePanelList.Remove(movedPanel);
                        observablePanelList.Remove(panel);

                        if (indexOver < indexMoved)
                        {
                            observablePanelList.Insert(indexOver, movedPanel);
                            observablePanelList.Insert(indexMoved, panel);
                        }
                        else
                        {
                            observablePanelList.Insert(indexMoved, panel);
                            observablePanelList.Insert(indexOver, movedPanel);
                        }
                    }

                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.Move;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    public class DropPanel() : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if (parameter is Tuple<object, EventArgs> parameters)
            {
                var dragEventArgs = parameters.Item2 as DragEventArgs;

                if (dragEventArgs is not null)
                {
                    // Set the effect for the drag operation
                    dragEventArgs.Effects = DragDropEffects.None;

                    // Optionally, you can handle hover logic here (e.g., showing a visual indicator)
                    dragEventArgs.Handled = true;
                }
            }
        }
    }
    #endregion

    #region [Add Items]
    public class AddPanelCommand(IWindowService windowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.AddElementType = AddElementType.Panel;
            windowService.ShowDialog(new AddPanel());
        }
    }
    public class AddSectionCommand(IWindowService windowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not WebPanelItem panel) return;

            webPageStateService.AddElementType = AddElementType.Section;
            webPageStateService.WebPanel = panel;
            windowService.ShowDialog(new AddSection());
        }
    }
    public class AddControlCommand(IWindowService windowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {

            if(parameter is not TwoLevelBindingParameter parms) return;
            if(parms.Child is not WebSectionItem section) return;

            webPageStateService.AddElementType = AddElementType.Control;
            webPageStateService.WebSection = section;
            windowService.ShowDialog(new AddControl());
        }
    }
    #endregion

    #region [Edit Items]
    public class EditControlCommand(IWebPageStateService webPageStateService, WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not WebControlItem control) return;

            webPageStateService.WebControl = control;
            webPageEditViewModel.EditControl = new BaseControl();
        }
    }
    public class EditSectionCommand(IWebPageStateService webPageStateService, WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not TwoLevelBindingParameter parms) return;

            webPageStateService.WebSection = parms.Child as WebSectionItem;
            webPageEditViewModel.EditControl = new BaseSection();
        }
    }
    public class EditPanelCommand(IWebPageStateService webPageStateService, WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not WebPanelItem panel) return;

            webPageStateService.WebPanel = panel;
            webPageEditViewModel.EditControl = new BasePanel();
        }
    }
    public class EditPageCommand(WebPageEditViewModel webPageEditViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;
        protected override void ExecuteSync(object? parameter = null)
        {
            webPageEditViewModel.EditControl = new BasePage();
        }
    }
    #endregion

    #region [Delete Items]
    public class DeletePanelCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is WebPanelItem panel)
                webPageStateService.DeletePanel(panel);
        }
    }
    public class DeleteSectionCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not TwoLevelBindingParameter parms) return;
            if (parms.Child is not WebSectionItem section) return;

            webPageStateService.DeleteSection(section);
        }
    }
    public class DeleteControlCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is WebControlItem panel)
                webPageStateService.DeleteControl(panel);
        }
    }
    #endregion
}

