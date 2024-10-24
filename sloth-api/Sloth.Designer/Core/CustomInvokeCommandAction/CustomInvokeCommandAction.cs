using Microsoft.Xaml.Behaviors;
using System.Windows.Input;
using System.Windows;

namespace Sloth.Designer.Core;

public class CustomInvokeCommandAction : TriggerAction<DependencyObject>
{
    // DependencyProperty for Command (same as in InvokeCommandAction)
    public static readonly DependencyProperty CommandProperty =
        DependencyProperty.Register(nameof(Command), typeof(ICommand), typeof(CustomInvokeCommandAction));

    // DependencyProperty for CommandParameter (optional)
    public static readonly DependencyProperty CommandParameterProperty =
        DependencyProperty.Register(nameof(CommandParameter), typeof(object), typeof(CustomInvokeCommandAction));

    /// <summary>
    /// Gets or sets the ICommand that will be executed when the event is triggered.
    /// </summary>
    public ICommand Command
    {
        get => (ICommand)GetValue(CommandProperty);
        set => SetValue(CommandProperty, value);
    }

    /// <summary>
    /// Optional parameter that is passed to the ICommand.
    /// </summary>
    public object CommandParameter
    {
        get => GetValue(CommandParameterProperty);
        set => SetValue(CommandParameterProperty, value);
    }

    protected override void Invoke(object parameter)
    {
        // Ensure the command is set
        if (Command == null)
        {
            return;
        }

        // Get the event arguments (MouseEventArgs, DragEventArgs, etc.)
        var eventArgs = parameter as EventArgs;

        // Determine the command parameter to pass to the command
        object commandParameter = CommandParameter;

        // If CommandParameter is not explicitly set, pass the EventArgs as the command parameter
        if (commandParameter == null)
        {
            commandParameter = eventArgs;
        }
        else if (eventArgs != null)
        {
            // If both CommandParameter and eventArgs exist, pass them as a Tuple
            commandParameter = new Tuple<object, EventArgs>(commandParameter, eventArgs);
        }

        // Check if the command can execute and execute it
        if (Command.CanExecute(commandParameter))
        {
            Command.Execute(commandParameter);
        }
    }
}