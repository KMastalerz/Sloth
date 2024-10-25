using System.Globalization;
using System.Windows.Data;
using System.Windows;

namespace Sloth.Designer.Converters;
public class BooleanThicknessConverter : IValueConverter
{
    public Thickness TrueValue { get; set; }
    public Thickness FalseValue { get; set; }

    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is bool booleanValue)
        {
            return booleanValue ? TrueValue : FalseValue;
        }
        return FalseValue;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
