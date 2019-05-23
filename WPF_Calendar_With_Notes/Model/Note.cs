using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes
{
    public class Note
    {
        public int ID {get;set;}
        public DateTime Date { get; set; }
        public string Message { get; set; }
        //public string Title { get; set; }
        public string User { get; set; }
    }
}
