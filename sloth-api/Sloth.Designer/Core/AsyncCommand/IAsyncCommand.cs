using System.Windows.Input;

namespace Sloth.Designer.Core;
public interface IAsyncCommand : ICommand
{
    IEnumerable<Task> RunningTasks { get; }
    new bool CanExecute(object? parameter = null);
    Task ExecuteAsync(object? parameter = null);
}
