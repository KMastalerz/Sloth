using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
public class BaseSectionViewModel: BaseViewModel
{
    public BaseSectionViewModel(IWebPageStateService webPageStateService)
    {
        WebSection = webPageStateService.WebSection!;
    }

    private WebSectionItem webSection = default!;
    public WebSectionItem WebSection
    {
        get => webSection;
        set => SetProperty(ref webSection, value);
    }

    private UserControl? metadataSection = null;

    public UserControl? MetadataSection
    {
        get => metadataSection;
        set => SetProperty(ref metadataSection, value);
    }
}
