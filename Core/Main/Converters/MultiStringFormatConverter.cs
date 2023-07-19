using System;
using System.Globalization;
using System.Linq;
using Xamarin.Forms;

namespace Recommender.Converters
{
    public class MultiStringFormatConverter : IMultiValueConverter
    {
        public object Convert(object[] values, Type targetType, object parameter, CultureInfo culture)
        {
            if (values.Any(v => v == null)) return string.Empty;

            var primaryValue = values[0].ToString();
            var secondaryValue = values[1].ToString();
            var format = parameter as string;

            return string.Format(format, primaryValue, secondaryValue);
        }

        public object[] ConvertBack(object value, Type[] targetTypes, object parameter, CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
