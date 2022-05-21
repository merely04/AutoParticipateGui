using System;
using System.Globalization;
using System.Windows.Data;
using AutoParticipateGui.Stores;

namespace AutoParticipateGui.Converters
{
    public class ScriptStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is ScriptStatus scriptStatus)
            {
                var text = "";
                
                switch (scriptStatus)
                {
                    case ScriptStatus.Idling:
                        text = "Запустить";
                        break;
                    case ScriptStatus.Working:
                        text = "Остановить";
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                return text;
            }
            
            return Binding.DoNothing;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}