using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows;

namespace WPF_Calendar_With_Notes.Utilities.Converters
{
    class ConverterOfNote : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            string val = value.ToString();

            if (val.Length > 498)
            {
                MessageBox.Show("Note is too long");
                return val.Remove(498);
            }


            if (string.IsNullOrWhiteSpace(val) == true)
            {
                val = "";
            }



            return val;
        }
    }

}
