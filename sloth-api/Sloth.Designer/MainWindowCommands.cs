using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Pages;
using Sloth.Designer.Services;

namespace Sloth.Designer;
public class MainWindowCommands
{
    public class SearchPages(IDesignerService designerService, IWebPageStateService webPageStateService, MainWindowViewModel viewModel) : AsyncCommand
    {
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            if (parameter is ListWebPageParam param)
            {
                webPageStateService.WebPages = await designerService.ListWebPageByID(param.AppID, param.PageID) ?? new();
                viewModel.UserControl = new WebPageList();
            }
        }
    }
}
