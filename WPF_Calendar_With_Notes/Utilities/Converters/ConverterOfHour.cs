using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Data;
using System.Globalization;

namespace WPF_Calendar_With_Notes.Utilities.Converters
{
    class ConverterOfHour : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int val;
            var b = int.TryParse(value.ToString(), out val);

            if (!b)
            {
                //MessageBox.Show("This is not proper hour");
                return 0;
            }

            if (val < 0)
            {
                return 0;
            }

            if (val > 23)
            {
                return 23;
            }




            return val;
        }
    }
}
