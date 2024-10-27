using Sloth.Designer.Core;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
public class BaseControlViewModel : BaseViewModel
{

    private UserControl? metaDataControlForm = null;

    public UserControl? MetaDataControlForm
    {
        get => metaDataControlForm;
        set => SetProperty(ref metaDataControlForm, value);
    }
}
