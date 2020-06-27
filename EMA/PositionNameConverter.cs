using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;
using System.Windows.Data;

namespace EMA
{
    public class PositionNameConverter : IValueConverter
    {
        const char DELIMITER = ';';
        char[] symbols = { '.', '-' };

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = System.Convert.ToString(value);

            char[] chars = str.ToCharArray();

            int j = 0;
            for (var i = 0; i < str.Length && j < symbols.Length; i++)
            {
                if (str[i] == DELIMITER)
                    chars[i] = symbols[j++];
            }
            return new string(chars);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
