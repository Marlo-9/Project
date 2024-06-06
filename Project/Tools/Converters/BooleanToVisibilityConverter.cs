using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Project.Tools.Converters;

[ValueConversion(typeof(bool), typeof(Visibility))]
public class BooleanToVisibilityConverter : IValueConverter
{
    enum Parameters
    {
        Normal, 
        Inverted
    }

    public object Convert(object value, Type targetType,
        object? parameter, CultureInfo culture)
    {
        var boolValue = (bool)value;

        if(parameter != null && Enum.TryParse((string)parameter, out Parameters direction) && direction == Parameters.Inverted)
            return !boolValue? Visibility.Visible : Visibility.Collapsed;

        return boolValue? Visibility.Visible : Visibility.Collapsed;
    }

    public object ConvertBack(object value, Type targetType,
        object? parameter, CultureInfo culture)
    {
        return null;
    }
}