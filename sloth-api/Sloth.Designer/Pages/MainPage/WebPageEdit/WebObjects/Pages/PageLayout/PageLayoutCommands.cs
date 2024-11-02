using Sloth.Designer.Core;

namespace Sloth.Designer.Pages;
public static class PageLayoutCommands
{
    public class AddArea : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            throw new NotImplementedException();
        }
    }

    public class DeleteArea : SyncCommand
    {
        protected override bool CanExecuteSync(object? parameter = null) => true;

        protected override void ExecuteSync(object? parameter = null)
        {
            throw new NotImplementedException();
        }
    }
}
