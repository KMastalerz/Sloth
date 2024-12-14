using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Models;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.BaseControlCommands;

namespace Sloth.Designer.Pages;
public class BaseControlViewModel : BaseViewModel
{
    public BaseControlViewModel(IWebPageStateService webPageStateService, IWindowService windowService)
    {
        // set default values 
        webControl = webPageStateService.WebControl!;
        Types = ControlConstants.ControlTypes;
        SetMetadata = new SetMetadata(windowService);

        SetVisibilities(webControl.ControlType ?? string.Empty);
        SetComboboxes(webControl.ControlType ?? string.Empty);
    }


    private WebControlItem webControl = default!;
    public WebControlItem WebControl
    {
        get => webControl;
        set => SetProperty(ref webControl, value);
    }

    #region [Type]

    private List<ControlElement> types = default!;
    public List<ControlElement> Types
    {
        get => types;
        set => SetProperty(ref types, value);
    }

    public string Type
    {
        get => WebControl.ControlType;
        set
        {
            WebControl.ControlType = value;
            SetVisibilities(value);
            SetComboboxes(value);
        }
    }

    #endregion

    #region [Inner Type]

    private bool showInnerType = default!;
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

    #endregion

    #region [Style]

    private bool showStyle = default!;
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

    #endregion

    #region [Size]

    private bool showSize = default!;
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

    #endregion

    #region [Routing]

    private bool showRouting = default!;
    public bool ShowRouting
    {
        get => showRouting;
        set => SetProperty(ref showRouting, value);
    }

    #endregion

    #region [Tooltip]

    private Dictionary<string, string> tooltipPositions = default!;
    public Dictionary<string, string> TooltipPositions
    {
        get => tooltipPositions;
        set => SetProperty(ref tooltipPositions, value);
    }

    #endregion
    public ICommand SetMetadata { get; }

    private void SetVisibilities(string type)
    {
        // set visibility of controls based on ControlType
        ShowInnerType = ((List<string>)[ControlTypes.Button, ControlTypes.Link]).Contains(type);
        ShowStyle = ((List<string>)[ControlTypes.Button]).Contains(type);
        ShowSize = ((List<string>)[ControlTypes.Button]).Contains(type);
        ShowRouting = ((List<string>)[ControlTypes.Link]).Contains(type);
    }

    private void SetComboboxes(string type)
    {
        TooltipPositions = ControlConstants.TooltipPositions;
        // set comboboxes based on ControlType
        switch (type)
        {
            case ControlTypes.Button:
                InnerTypes = ControlInnerTypes.Button;
                Styles = ControlStyles.Button;
                Sizes = ControlSizes.Button;
                break;
            case ControlTypes.Link:
                InnerTypes = ControlInnerTypes.Link;
                Styles = null;
                Sizes = null;
                break;
            default:
                InnerTypes = null;
                Styles = null;
                Sizes = null;
                break;
        }
    }
}
