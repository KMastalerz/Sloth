using Sloth.Designer.Core;

namespace Sloth.Designer.Pages;

public class WebPageListCommands
{
    public class EditPage : AsyncCommand
    {
        public EditPage()
        {
            //webPageStateService = App.ServiceProvider.GetRequiredService<IWebPageStateService>();
            //mainWindowViewModel = App.ServiceProvider.GetRequiredService<MainWindowViewModel>();
            //this.webPageListViewModel = webPageListViewModel;
        }

        public override bool CanExecute(object? parameter = null) => true;
        public override async Task ExecuteAsync(object? parameter = null)
        {
            // webPageStateService.WebPage = webPageListViewModel.SelectedWebPage;
            // TODO: download full web page details: webpage, webcontrols, webpanels, websections and bind it to WebPage
            // TODO: display designer web page editor
        }
    }
}
