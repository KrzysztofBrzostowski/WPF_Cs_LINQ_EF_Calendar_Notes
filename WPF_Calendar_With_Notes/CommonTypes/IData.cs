using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes.CommonTypes
{
    public interface IData
    {
        List<DateTime> DtList { get; }
    }
}
