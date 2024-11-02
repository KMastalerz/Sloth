using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;

namespace Sloth.Designer.Core;
public class BaseViewModel: INotifyPropertyChanged
{
    // The PropertyChanged event is raised whenever a property is changed
    public event PropertyChangedEventHandler? PropertyChanged;

    // This method is called to raise the PropertyChanged event
    protected void OnPropertyChanged([CallerMemberName] string? propertyName = null)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    // This method sets the value of a property and notifies the UI if it changes
    protected bool SetProperty<T>(ref T backingField, T value, [CallerMemberName] string? propertyName = null)
    {
        if (!Equals(backingField, value))
        {
            backingField = value;
            OnPropertyChanged(propertyName);
            return true;
        }
        return false;
    }

    protected bool ValidateGridTemplate(ref string backingField, string newValue, string oldValue)
    {
        newValue = newValue.Replace(" ", "");
        string unitPattern = @"^\d+(rem|px|%)?$";  // Matches 10rem, 10px, 10%
        string autoPattern = @"^auto$";             // Matches "auto"
        string numberPattern = @"^\d+$";            // Matches numbers like 1, 2, etc.

        if (Regex.IsMatch(newValue, unitPattern) ||
            Regex.IsMatch(newValue, autoPattern) ||
            Regex.IsMatch(newValue, numberPattern))
            return SetProperty(ref backingField, newValue);

        return SetProperty(ref backingField, oldValue);
    }
}
