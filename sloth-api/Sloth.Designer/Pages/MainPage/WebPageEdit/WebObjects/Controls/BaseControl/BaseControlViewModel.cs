using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using Sloth.Shared.Models;
using System.Windows.Controls;

namespace Sloth.Designer.Pages;
public class BaseControlViewModel : BaseViewModel
{
    public BaseControlViewModel(IWebPageStateService webPageStateService)
    {
        // set default values 
        WebControl = webPageStateService.WebControl!;
        Types = ControlConstants.ControlTypes;
        Type = webPageStateService.WebControl!.ControlType;
        InnerType = webPageStateService.WebControl!.InnerType;
        Style = webPageStateService.WebControl!.Style;
        Size = webPageStateService.WebControl!.Size;
        SetVisibilities(type ?? string.Empty);
    }

    private List<ControlElement> types;
    public List<ControlElement> Types
    {
        get => types;
        set => SetProperty(ref types, value);
    }


    private WebControlItem WebControl { get; set; }

    private string type;
    public string Type
    {
        get => type;
        set 
        {
            SetProperty(ref type, value);
            WebControl.ControlType = value;

            // Set InnerTypes based on ControlType
            InnerTypes = value switch
            {
                ControlTypes.Button => ControlInnerTypes.Button,
                ControlTypes.Link => ControlInnerTypes.Link,
                _ => null
            };

            // Set Styles based on ControlType
            Styles = value switch
            {
                ControlTypes.Button => ControlStyles.Button,
                _ => null
            };

            // Set Styles based on ControlType
            Sizes = value switch
            {
                ControlTypes.Button => ControlSizes.Button,
                _ => null
            };

            SetVisibilities(value);
        }
    }

    #region [Inner Type]

    private bool showInnerType;
    public bool ShowInnerType
    {
        get => showInnerType;
        set => SetProperty(ref showInnerType, value);
    }

    private Dictionary<string, string>? innerTypes = null;
    public Dictionary<string, string>? InnerTypes
    {
        get => innerTypes;
        set => SetProperty(ref innerTypes, value);
    }

    private string? innerType;
    public string? InnerType
    {
        get => innerType;
        set
        {
            SetProperty(ref innerType, value);
            WebControl.InnerType = value;
        }
    }

    #endregion

    #region [Style]

    private bool showStyle;
    public bool ShowStyle
    {
        get => showStyle;
        set => SetProperty(ref showStyle, value);
    }

    private Dictionary<string, string>? styles = null;
    public Dictionary<string, string>? Styles
    {
        get => styles;
        set => SetProperty(ref styles, value);
    }

    private string? style;
    public string? Style
    {
        get => style;
        set
        {
            SetProperty(ref style, value);
            WebControl.Style = value;
        }
    }

    #endregion

    #region [Size]

    private bool showSize;
    public bool ShowSize
    {
        get => showSize;
        set => SetProperty(ref showSize, value);
    }

    private Dictionary<string, string>? sizes = null;
    public Dictionary<string, string>? Sizes
    {
        get => sizes;
        set => SetProperty(ref sizes, value);
    }

    private string? size;
    public string? Size
    {
        get => size;
        set
        {
            SetProperty(ref size, value);
            WebControl.Size = value;
        }
    }

    #endregion

    private UserControl? metadataControl = null;

    public UserControl? MetadataControl
    {
        get => metadataControl;
        set => SetProperty(ref metadataControl, value);
    }

    private void SetVisibilities(string type)
    {
        // set visibility of controls based on ControlType
        ShowInnerType = ((List<string>)[ControlTypes.Button, ControlTypes.Link]).Contains(type);
        ShowStyle = ((List<string>)[ControlTypes.Button]).Contains(type);
        ShowSize = ((List<string>)[ControlTypes.Button]).Contains(type);
    }

}
