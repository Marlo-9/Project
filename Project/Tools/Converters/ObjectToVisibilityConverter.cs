using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Project.Tools.Converters;

[ValueConversion(typeof(object), typeof(Visibility))]
public class ObjectToVisibilityConverter : IValueConverter
{
    public object Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return value is null ? Visibility.Collapsed : Visibility.Visible;
    }

    public object ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        return null;
    }
}