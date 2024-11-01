using Sloth.Designer.Core;
using Sloth.Designer.Services;
using System.Windows.Input;
using static Sloth.Designer.Pages.AddSectionCommands;

namespace Sloth.Designer.Pages;
public class AddSectionViewModel : BaseViewModel
{
    public AddSectionViewModel(IWebPageStateService webPageStateService, IWindowService windowService)
    {
        AddSectionCommand = new AddSectionCommand(webPageStateService, windowService);
    }

    private string? sectionID = null;
    public string? SectionID
    {
        get => sectionID;
        set => SetProperty(ref sectionID, value);
    }

    public ICommand AddSectionCommand { get; }
}
