using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Sloth.Designer.Converters;

public class StringToVisibilityConverter : IValueConverter
{
    public bool Invert { get; set; } = false;

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string typeString && parameter is string expectedType)
        {
            bool isEqual = typeString.Equals(expectedType, StringComparison.OrdinalIgnoreCase);

            // Apply inversion if Invert is true
            if (Invert)
            {
                return isEqual ? Visibility.Collapsed : Visibility.Visible;
            }
            else
            {
                return isEqual ? Visibility.Visible : Visibility.Collapsed;
            }
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        return Invert == true ? Visibility.Collapsed : Visibility.Visible;
    }
}
