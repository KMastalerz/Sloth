using System.Globalization;
using System.Windows.Data;
using System.Windows;
using System.Collections;

namespace Sloth.Designer.Converters;
public class EmptyCollectionToVisibilityConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        var collection = value as IEnumerable;
        if (collection != null && collection.GetEnumerator().MoveNext())
        {
            return Visibility.Visible;
        }
        return Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
