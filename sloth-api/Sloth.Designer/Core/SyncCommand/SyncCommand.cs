
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Windows.Input;

namespace Sloth.Designer.Core;

public abstract class SyncCommand : ICommand
{
    private readonly ObservableCollection<bool> runningTasks;

    protected SyncCommand()
    {
        runningTasks = new ObservableCollection<bool>();
        runningTasks.CollectionChanged += OnRunningTasksChanged;
    }

    private void OnRunningTasksChanged(object? sender, NotifyCollectionChangedEventArgs e)
    {
        CommandManager.InvalidateRequerySuggested();
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public bool IsRunning => runningTasks.Count > 0;

    public bool CanExecute(object? parameter)
    {
        return !IsRunning && CanExecuteSync(parameter);
    }

    public void Execute(object? parameter)
    {
        runningTasks.Add(true);
        try
        {
            ExecuteSync(parameter);
        }
        finally
        {
            runningTasks.Remove(true);
        }
    }

    protected abstract bool CanExecuteSync(object? parameter = null);
    protected abstract void ExecuteSync(object? parameter = null);
}
