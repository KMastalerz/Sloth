using Sloth.Designer.Pages;
using System.Windows.Controls;

namespace Sloth.Designer.Services;
internal class MainPageService(MainPageViewModel mainPageViewModel) : IMainPageService
{
    public void LoadPage(UserControl control)
    {
        mainPageViewModel.MainPageControl = control;
    }
}
