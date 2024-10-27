using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;

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
            }
        }
    }
    public class EditPage(IDesignerService designerService, IWebPageStateService webPageStateService, IMainPageService mainPageService) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            var parms = parameter as ListWebPageItem;
            if (parms is null) return;

            webPageStateService.WebPage = await designerService.GetFullWebPage(parms.AppID, parms.PageID);

            mainPageService.LoadPage(new WebPageEdit());
        }
    }
}
