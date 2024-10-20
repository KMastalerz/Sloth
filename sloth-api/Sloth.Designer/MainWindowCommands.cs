using Microsoft.Extensions.DependencyInjection;
using Sloth.Designer.Core;
using Sloth.Designer.Pages;
using Sloth.Designer.Services;

namespace Sloth.Designer;
public class MainWindowCommands
{

    public class SearchPages : AsyncCommand
    {
        private readonly IDesignerService designerService;
        private readonly IWebPageStateService webPageStateService;
        private readonly MainWindowViewModel viewModel;
        public SearchPages(MainWindowViewModel viewModel)
        {
            designerService = App.ServiceProvider.GetRequiredService<IDesignerService>();
            webPageStateService = App.ServiceProvider.GetRequiredService<IWebPageStateService>();
            this.viewModel = viewModel;
        }
        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            webPageStateService.WebPages = await designerService.ListWebPageByID(viewModel.PageID, viewModel.AppID) ?? new();
            viewModel.UserControl = new WebPageList();            
        }
    }
}
