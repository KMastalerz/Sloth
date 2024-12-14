using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;

namespace Sloth.Designer.Pages;

public static class AddPageCommands
{
    public class AddPageCommand(IWebPageStateService webPageStateService, MainPageViewModel mainPageViewModel, IWindowService windowService) : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null)
        {
            if(parameter is not AddPageViewModel addPageViewModel) return false;
            return !string.IsNullOrWhiteSpace(addPageViewModel.PageID) && !string.IsNullOrWhiteSpace(addPageViewModel.AppID);
        }

        protected override void ExecuteSync(object? parameter = null)
        {
            if (parameter is not AddPageViewModel addPageViewModel) return;

            var webPage = new WebPageItem
            {
                AppID = addPageViewModel.AppID!,
                PageID = addPageViewModel.PageID!
            };
            // Add to state
            webPageStateService.WebApplications = webPageStateService.WebApplications.Append(webPage.AppID!);
            webPageStateService.WebPages = webPageStateService.WebPages.Append(new ListWebPageItem { AppID = webPage.AppID, PageID = webPage.PageID });    
            webPageStateService.WebPage = webPage;

            // Redirect to edit page
            mainPageViewModel.MainPageControl = new WebPageEdit();
            windowService.CloseDialog();
        }
    }
}
