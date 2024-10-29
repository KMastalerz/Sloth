using System.Globalization;
using System.Windows.Data;
using Sloth.Designer.Core;

namespace Sloth.Designer.Converters;
public class TwoLevelBindingConverter : IMultiValueConverter
{
    public object? Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
    {
        if (values[0] != null && values[1] != null)
        {
            return new TwoLevelBindingParameter(values[0], values[1]);
        }
        return null;
    }

    public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
    {
        return [];
    }
}