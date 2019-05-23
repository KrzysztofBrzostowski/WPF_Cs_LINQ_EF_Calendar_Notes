using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes
{
    public class FieldsOfDataGrid
    {
        private string _User = "";
        public string User
        {
            get { return _User; }
            set { _User = value; }
        }

        private string _Note = "";
        public string Note
        {
            get { return _Note; }
            set { _Note = value; }
        }

        private short _Hour = 0;
        public short Hour
        {
            get { return _Hour; }
            set { _Hour = value; }
        }

        private short _Minute = 0;
        public short Minute
        {
            get { return _Minute; }
            set { _Minute = value; }
        }
    }
}
