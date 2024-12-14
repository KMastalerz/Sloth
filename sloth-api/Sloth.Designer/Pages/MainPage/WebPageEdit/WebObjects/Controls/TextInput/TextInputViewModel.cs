using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using Sloth.Shared.Helpers;

namespace Sloth.Designer.Pages;
public class TextInputViewModel: BaseViewModel
{
    private readonly IWebPageStateService webPageStateService;
    public TextInputViewModel(IWebPageStateService webPageStateService)
    {
        this.webPageStateService = webPageStateService;
        webControl = webPageStateService.WebControl!;

        if (!string.IsNullOrEmpty(webPageStateService.WebControl!.Metadata))
        {
            var metadata = JsonHelper.TryConvert(webPageStateService.WebControl.Metadata, new TextInputMetadata());

            if (metadata != null)
            {
                MinLength = metadata.MinLength;
                MaxLength = metadata.MaxLength;
            }
        }
    }

    private WebControlItem webControl;
    public WebControlItem WebControl
    {
        get => webControl;
        set => SetProperty(ref webControl, value);
    }

    private int? minLength = null;
    public int? MinLength
    {
        get => minLength;
        set
        {
            if (SetProperty(ref minLength, value))
                UpdateMetaData();
        }
    }

    private int? maxLength = null;
    public int? MaxLength
    {
        get => maxLength;
        set
        {
            if (SetProperty(ref maxLength, value))
                UpdateMetaData();
        }
    }

    private void UpdateMetaData()
    {
        var metadata = new TextInputMetadata
        {
            MinLength = MinLength,
            MaxLength = MaxLength
        };

        webPageStateService.WebControl!.Metadata = metadata.SerializeToCamelCase();
    }
}
