using System;
using System.Globalization;
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
            for (var i = 0; i < str.Length; i++)
            {
                if (str[i] == DELIMITER)
                {
                    chars[i] = symbols[j];
                    if (j < symbols.Length - 1)
                        j++;
                }
            }
            return new string(chars);
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string str = System.Convert.ToString(value);

            char[] chars = str.ToCharArray();

            for (int i = 0; i < chars.Length; i++)
            {
                for (int j = 0; j < symbols.Length; j++)
                {
                    if (chars[i] == symbols[j])
                        chars[i] = DELIMITER;
                }
            }

            return new string(chars);
        }
    }
}
