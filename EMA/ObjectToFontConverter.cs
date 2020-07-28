using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EMA
{
    public class ObjectToFontConverter : IValueConverter
    {
        const double REGULAR_FONT = 12, CAPITAL_FONT = 14;

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value == null ? REGULAR_FONT : CAPITAL_FONT; 
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
