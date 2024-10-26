using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows;
using System.Windows.Input;
using Sloth.Designer.Enums;
using Sloth.Designer.Core;

namespace Sloth.Designer.Pages;
public class WebPageEditCommands
{
    public class GoBack(IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.WebPage = null;
            mainPageViewModel.UserControl = new WebPageList();
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
                    if(control != movedControl)
                    {

                        var observableControlList = webPageEditViewModel.ParentSection.WebControls;

                        var indexMoved = observableControlList.IndexOf(movedControl);
                        var indexOver = observableControlList.IndexOf(control);

                        // replace the control in the list
                        observableControlList.Remove(movedControl);
                        observableControlList.Remove(control);

                        if(indexOver < indexMoved)
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

                if(dragEventArgs is not null)
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


                dragEventArgs!.Effects = DragDropEffects.Move;
                if (dragEventArgs != null)
                {
                    if (control != movedControl)
                    {

                        var observableControlList = webPageEditViewModel.ParentPanel.WebControls;

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
    public class AddPanelCommand(IMainWindowService mainWindowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.AddElementType = AddElementType.Panel;
            mainWindowService.ShowDialog(new AddElement());
        }
    }
    public class AddPanelChildrenCommand(IMainWindowService mainWindowService, IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.AddElementType = AddElementType.PanelChildren;
            webPageStateService.WebPanel = parameter as WebPanelItem;
            mainWindowService.ShowDialog(new AddElement());
        }
    }
    public class AddSectionControlCommand(IMainWindowService mainWindowService, IWebPageStateService webPageStateService): SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            webPageStateService.AddElementType = AddElementType.SectionControl;
            webPageStateService.WebSection = parameter as WebSectionItem;
            mainWindowService.ShowDialog(new AddElement());
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
            if (parameter is WebSectionItem panel)
                webPageStateService.DeleteSection(panel);
        }
    }
    public class DeletePanelControlCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is WebControlItem panel)
                webPageStateService.DeletePanelControl(panel);
        }
    }
    public class DeleteSectionControlCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is WebControlItem panel)
                webPageStateService.DeleteSectionControl(panel);
        }
    }
    #endregion
}

