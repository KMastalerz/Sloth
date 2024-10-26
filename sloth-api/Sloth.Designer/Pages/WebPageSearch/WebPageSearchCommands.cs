using Sloth.Designer.Core;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public class WebPageSearchCommands
{
    public class SearchPages(IDesignerService designerService, IWebPageStateService webPageStateService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (parameter is WebPageSearchViewModel param)
            {
                webPageStateService.WebPages = await designerService.ListWebPageByID(param.AppID, param.PageID) ?? new();
                param.UserControl = new WebPageList();
            }
        }
    }
}
