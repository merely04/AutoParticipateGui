using System;
using System.Diagnostics;
using System.Globalization;
using System.Windows.Data;

namespace AutoParticipateGui.Converters
{
    public class NavigationConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value?.GetType() == parameter as Type;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}