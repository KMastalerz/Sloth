using Sloth.Designer.Constants;
using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddControlCommands;

namespace Sloth.Designer.Pages;
public class AddControlViewModel:BaseViewModel
{
    public AddControlViewModel(IWebPageStateService webPageStateService)
    {
        AddControlCommand = new AddControlCommand(webPageStateService);
    }

    private string? controlName = null;
    public string? ControlName
    {
        get => controlName;
        set => SetProperty(ref controlName, value);
    }

    private List<ControlElement> controlTypes = ControlConstants.ControlTypes;
    public List<ControlElement> ControlTypes
    {
        get => controlTypes;
        set => SetProperty(ref controlTypes, value);
    }

    private string? controlType = null;
    public string? ControlType
    {
        get => controlType;
        set => SetProperty(ref controlType, value);
    }

    public ICommand AddControlCommand { get; }
}
