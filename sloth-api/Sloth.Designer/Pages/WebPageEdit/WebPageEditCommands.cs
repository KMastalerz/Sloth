using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Sloth.Designer.Pages;
public class WebPageEditCommands
{
    //IDesignerService designerService, 
    public class GoBack(IWebPageStateService webPageStateService, MainWindowViewModel mainWindowViewModel) : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter = null) => true;

        public void Execute(object? parameter = null)
        {
            webPageStateService.WebPage = null;
            mainWindowViewModel.UserControl = new WebPageList();
        }
    }

    private static object? draggedItem = null; // Keep track of dragged item

    // ICommand for handling drag
    public class OnDrag : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter = null) => true;

        public void Execute(object? parameter = null)
        {
            draggedItem = parameter; // Store the dragged item
        }
    }

    // ICommand for handling drop
    public class OnDrop : ICommand
    {
        private readonly WebPageItem? _webPage;

        public OnDrop(WebPageItem? webPage)
        {
            _webPage = webPage;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object? parameter = null) => true;

        public void Execute(object? parameter = null)
        {
            if (draggedItem is WebPanelItem draggedPanel && parameter is WebPanelItem targetPanel && _webPage != null)
            {
                // Reorder panels within WebPanels collection
                if (_webPage.WebPanels != null && _webPage.WebPanels.Contains(draggedPanel) && _webPage.WebPanels.Contains(targetPanel))
                {
                    ObservableCollection<WebPanelItem> newPanelOrder = new(_webPage.WebPanels);
                    int oldIndex = newPanelOrder.IndexOf(draggedPanel);
                    int newIndex = newPanelOrder.IndexOf(targetPanel);
                    newPanelOrder.Move(oldIndex, newIndex);
                    _webPage.WebPanels = newPanelOrder.AsEnumerable();
                }
            }
            else if (draggedItem is WebSectionItem draggedSection && parameter is WebSectionItem targetSection && _webPage != null)
            {
                // Handle reordering of sections within the panel
                var parentPanel = _webPage.WebPanels?.FirstOrDefault(panel => panel.WebSections.Contains(draggedSection));

                if (parentPanel != null && parentPanel.WebSections.Contains(draggedSection) && parentPanel.WebSections.Contains(targetSection))
                {
                    ObservableCollection<WebSectionItem> newSectionOrder = new(parentPanel.WebSections);
                    int oldIndex = newSectionOrder.IndexOf(draggedSection);
                    int newIndex = newSectionOrder.IndexOf(targetSection);
                    newSectionOrder.Move(oldIndex, newIndex);
                    parentPanel.WebSections = newSectionOrder.AsEnumerable();
                }
            }
            else if (draggedItem is WebControlItem draggedControl && parameter is WebControlItem targetControl && _webPage != null)
            {
                // Handle reordering of controls within a section
                var parentSection = _webPage.WebPanels?.SelectMany(panel => panel.WebSections)
                    .FirstOrDefault(section => section.WebControls.Contains(draggedControl));

                if (parentSection != null && parentSection.WebControls.Contains(draggedControl) && parentSection.WebControls.Contains(targetControl))
                {
                    ObservableCollection<WebControlItem> newControlsOrder = new(parentSection.WebControls);
                    int oldIndex = newControlsOrder.IndexOf(draggedControl);
                    int newIndex = newControlsOrder.IndexOf(targetControl);
                    newControlsOrder.Move(oldIndex, newIndex);
                    parentSection.WebControls = newControlsOrder.AsEnumerable();
                }
            }

            // Clear the dragged item after drop
            draggedItem = null;
        }
    }
}
