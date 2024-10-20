using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace Sloth.Designer.Converters;
public class PascalCaseToHeaderConverter : IValueConverter
{
    public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
    {
        if (value is string propertyName)
        {
            // Use regex to insert spaces before capital letters
            return Regex.Replace(propertyName, "(\\B[A-Z])", " $1");
        }
        return value;
    }

    public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
    {
        throw new NotImplementedException();
    }
}
