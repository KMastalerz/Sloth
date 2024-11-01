using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Helpers;
using Sloth.Shared.Models;

namespace Sloth.Designer.Pages;
public class ButtonViewModel : BaseViewModel
{
    private readonly IWebPageStateService webPageStateService;
    public ButtonViewModel(IWebPageStateService webPageStateService)
    {
        this.webPageStateService = webPageStateService;
        webControl = webPageStateService.WebControl!;

        if (!string.IsNullOrEmpty(webPageStateService.WebControl!.MetaData))
        {
            var metaData = JsonHelper.TryConvert(webPageStateService.WebControl.MetaData, new ButtonMetadata());

            if (metaData != null)
            {
                CounterSubject = metaData.CounterSubject;
                WarningCount = metaData.WarningCount;
                ErrorCount = metaData.ErrorCount;
            }
        }
    }

    private WebControlItem webControl;
    public WebControlItem WebControl
    {
        get => webControl;
        set => SetProperty(ref webControl, value);
    }

    private string? counterSubject = null;
    public string? CounterSubject
    {
        get => counterSubject;
        set
        {
            var newValue = string.IsNullOrWhiteSpace(value) ? null : value;
            if (SetProperty(ref counterSubject, newValue))
                UpdateMetaData();
        }
    }

    private int? warningCount = null;
    public int? WarningCount
    {
        get => warningCount;
        set
        {
            if (SetProperty(ref warningCount, value))
                UpdateMetaData();
        }
    }

    private int? errorCount = null;
    public int? ErrorCount
    {
        get => errorCount;
        set
        {
            if (SetProperty(ref errorCount, value))
                UpdateMetaData();
        }
    }

    private void UpdateMetaData()
    {
        var metaData = new ButtonMetadata
        {
            CounterSubject = CounterSubject,
            WarningCount = WarningCount,
            ErrorCount = ErrorCount
        };

        webPageStateService.WebControl!.MetaData = metaData.SerializeToCamelCase();
    }
}
