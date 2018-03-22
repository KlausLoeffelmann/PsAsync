using System;
using Windows.UI;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Media;

namespace UwpClient.Converters
{
    public class RevenueRangeToColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, string language)
        {
            SolidColorBrush transparent;

            var revenue = value as decimal?;

            if (!revenue.HasValue)
            {
                transparent = new SolidColorBrush(Colors.Transparent);
            }
            else if (revenue.Value>100)
            {
                transparent = (revenue.Value>20000? new SolidColorBrush(Colors.Green) : new SolidColorBrush(Colors.YellowGreen));
            }
            else
            {
                transparent = new SolidColorBrush(Colors.Red);
            }
            return transparent;
        }

        public object ConvertBack(object value, Type targetType, object parameter, string language)
        {
            throw new NotImplementedException();
        }
    }
}
