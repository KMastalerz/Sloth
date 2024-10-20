using System.Windows;
using System.Windows.Input;

namespace Sloth.Designer.Core;
public class FocusKeyBinding : KeyBinding
{
    public string TargetElementName { get; set; } = default!;

    private void OnExecuted(object sender, ExecutedRoutedEventArgs e)
    {
        // Check if the focused element matches the target element by name
        if (Keyboard.FocusedElement is FrameworkElement focusedElement &&
            focusedElement.Name == TargetElementName)
        {
            if (Command != null && Command.CanExecute(CommandParameter))
            {
                Command.Execute(CommandParameter);
            }
        }

        // Optionally, you can mark the event as handled so that it doesn't bubble further
        e.Handled = true;
    }
}
