using Sloth.Designer.Pages;
using System.Windows.Controls;

namespace Sloth.Designer.Services;
internal class WebPageEditService(WebPageEditViewModel webPageEditViewModel) : IWebPageEditService
{
    public void LoadPage(UserControl control)
    {
        webPageEditViewModel.EditControl = control;
    }
}
