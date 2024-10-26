using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Enums;
using Sloth.Designer.Models;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public class AddElementCommands
{

    public class AddNewElement(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null)
        {
            if (parameter is AddElementViewModel viewModel)
            {
                if (string.IsNullOrEmpty(viewModel.ElementID)) return false;

                if (viewModel.ShowElementTypeSelection) 
                    return !string.IsNullOrEmpty(viewModel.ElementType);

                return true;
            }
            else return false;
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is AddElementViewModel viewModel)
            {

                switch (webPageStateService.AddElementType)
                {
                    case AddElementType.Panel:
                        var panel = new NewElementItem
                        {
                            ElementID = viewModel.ElementID,
                            ElementType = PanelConstants.Panels.First(p => p.PanelName == viewModel.ElementType).PanelType
                        };
                        webPageStateService.AddPanel(panel);
                        break;
                    case AddElementType.PanelChildren:
                        if (viewModel.ChildType == "Section")
                        {
                            var section = new NewElementItem
                            {
                                ElementID = viewModel.ElementID
                            };
                            webPageStateService.AddSection(section);
                        }
                        else
                        {
                            var panelControl = new NewElementItem
                            {
                                ElementID = viewModel.ElementID,
                                ElementType = ControlConstants.Controls.First(p => p.ControlName == viewModel.ElementType).ControlType
                            };
                            webPageStateService.AddPanelControl(panelControl);
                        }

        
                        break;
                    case AddElementType.SectionControl:
                        var sectionControl = new NewElementItem
                        {
                            ElementID = viewModel.ElementID,
                            ElementType = ControlConstants.Controls.First(p => p.ControlName == viewModel.ElementType).ControlType
                        };
                        webPageStateService.AddSectionControl(sectionControl);
                        break;
                }
            }

        }
    }
}
