using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;
public static class AddSectionCommands
{
    public class AddSectionCommand(IWebPageStateService webPageStateService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null)
        {
            if(parameter is AddSectionViewModel addSectionViewModel)
            {
                return !string.IsNullOrEmpty(addSectionViewModel.SectionID);
            }
            return false;
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if(parameter is not AddSectionViewModel addSectionViewModel) return;
            
            var newSection = new NewSection(addSectionViewModel.SectionID);

            webPageStateService.AddSection(newSection);
        }
    }
}
