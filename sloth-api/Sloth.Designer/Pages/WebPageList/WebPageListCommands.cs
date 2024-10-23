using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;

namespace Sloth.Designer.Pages;
public class WebPageListCommands
{
    public class EditPage(IDesignerService designerService, IWebPageStateService webPageStateService, MainWindowViewModel mainWindowViewModel) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            var parms = parameter as ListWebPageItem;
            if (parms is null) return;

            webPageStateService.WebPage = await designerService.GetFullWebPage(parms.AppID, parms.PageID);

            mainWindowViewModel.UserControl = new WebPageEdit();
        }
    }
}
