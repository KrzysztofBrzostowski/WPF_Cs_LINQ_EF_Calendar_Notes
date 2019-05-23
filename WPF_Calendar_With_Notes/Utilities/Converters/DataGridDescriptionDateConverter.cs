using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Data;

namespace WPF_Calendar_With_Notes.Utilities.Converters
{
    class DataGridDescriptionDateConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value != null)
            {
                DateTime dt = (DateTime)value;

                string res = dt.ToString().Remove(10);

                res = string.Format("{0}: {1}", Properties.Resources.EditedDay, res);

                res += " (" + dt.ToString("dddd") + ")";

                return res;
            }
            else
                return value;

        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }

    }
}
