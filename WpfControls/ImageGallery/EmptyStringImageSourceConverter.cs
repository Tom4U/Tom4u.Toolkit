using System;
using System.Globalization;
using System.Windows;
using System.Windows.Data;

namespace Tom4u.Toolkit.WpfControls.Images
{
    public class EmptyStringImageSourceConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (!(value is string)) return value;

            return string.IsNullOrWhiteSpace(value.ToString()) ? DependencyProperty.UnsetValue : value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return Binding.DoNothing;
        }
    }
}
