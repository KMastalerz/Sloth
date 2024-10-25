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
                var param = new NewElementItem
                {
                    ElementID = viewModel.ElementID,
                    ElementType = viewModel.ElementType
                };
                switch (webPageStateService.AddElementType)
                {
                    case AddElementType.Panel:
                        webPageStateService.AddPanel(param);
                        break;
                    case AddElementType.PanelChildren:
                        if(viewModel.ChildType == "Section")
                            webPageStateService.AddSection(param);
                        else
                            webPageStateService.AddPanelControl(param);
                        break;
                    case AddElementType.SectionControl:
                        webPageStateService.AddSectionControl(param);
                        break;
                }
            }

        }
    }
}
