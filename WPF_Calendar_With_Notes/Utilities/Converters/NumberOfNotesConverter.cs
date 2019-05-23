using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace WPF_Calendar_With_Notes.Utilities.Converters
{
    public class NumberOfNotesConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            int val;
            var b = int.TryParse(value.ToString(), out val);
            if (b)
            {
                if (val == 0) return string.Format("{0}: {1}", Properties.Resources.NumberOfPositionDescription, Properties.Resources.None);

                return string.Format("{0}: {1}", Properties.Resources.NumberOfPositionDescription, val);
            }
            else
                throw new ArgumentOutOfRangeException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
