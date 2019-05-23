using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes.CommonTypes
{
    public interface INotesContext<T> where T : class
    {
        IEnumerable<T> BrokerNotes { get; }

        void Add(T note);
        bool Remove(T note);

        int SaveChanges();
        void Dispose();
    }
}
