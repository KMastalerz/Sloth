using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Helpers;
using Sloth.Shared.Models;

namespace Sloth.Designer.Pages;
public class LinkViewModel: BaseViewModel
{
    private readonly IWebPageStateService webPageStateService;
    public LinkViewModel(IWebPageStateService webPageStateService)
    {
        this.webPageStateService = webPageStateService;
        webControl = webPageStateService.WebControl!;

        if (!string.IsNullOrEmpty(webPageStateService.WebControl!.Metadata))
        {
            var metadata = JsonHelper.TryConvert(webPageStateService.WebControl.Metadata, new ButtonMetadata());

            if (metadata != null)
            {
                CounterSubject = metadata.CounterSubject;
                WarningCount = metadata.WarningCount;
                ErrorCount = metadata.ErrorCount;
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
                UpdateMetadata();
        }
    }

    private int? warningCount = null;
    public int? WarningCount
    {
        get => warningCount;
        set
        {
            if (SetProperty(ref warningCount, value))
                UpdateMetadata();
        }
    }

    private int? errorCount = null;
    public int? ErrorCount
    {
        get => errorCount;
        set
        {
            if (SetProperty(ref errorCount, value))
                UpdateMetadata();
        }
    }

    private void UpdateMetadata()
    {
        var metadata = new ButtonMetadata
        {
            CounterSubject = CounterSubject,
            WarningCount = WarningCount,
            ErrorCount = ErrorCount
        };

        webPageStateService.WebControl!.Metadata = metadata.SerializeToCamelCase();
    }
}
