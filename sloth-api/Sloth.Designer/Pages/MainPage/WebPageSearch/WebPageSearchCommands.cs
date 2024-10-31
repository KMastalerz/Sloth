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
                var response = await designerService.ListWebPageByID(param.AppID, param.PageID);

                if (response?.Success == true)
                {
                    webPageStateService.WebPages = response.Data!;
                }
                else
                {
                    webPageStateService.WebPages = [];
                }
            }
        }
    }
    public class EditPage(IDesignerService designerService, IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            var parms = parameter as ListWebPageItem;
            if (parms is null) return;

            var response = await designerService.GetFullWebPage(parms.AppID, parms.PageID);

            if (response?.Success == true)
            {
                webPageStateService.WebPage = response.Data!;
                mainPageViewModel.MainPageControl = new WebPageEdit();
            }
            else
            {
                webPageStateService.WebPage = null;
                mainPageViewModel.MainPageControl = null;
            }
        }
    }
}
