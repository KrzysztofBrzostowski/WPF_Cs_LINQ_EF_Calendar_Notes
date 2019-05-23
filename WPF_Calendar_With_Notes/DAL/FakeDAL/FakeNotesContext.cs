using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Calendar_With_Notes.CommonTypes;

namespace WPF_Calendar_With_Notes.DAL
{
    public class FakeNotesContext : INotesContext<Note>
    {
        public FakeNotesContext()
        {
            m_BrokerNotes = new List<Note>();
        }

        private List<Note> m_BrokerNotes;
        public IEnumerable<Note> BrokerNotes
        {
            get { return m_BrokerNotes; }
        }

        public void Add(Note note) { m_BrokerNotes.Add(note); }

        public bool Remove(Note note) { var result = m_BrokerNotes.Remove(note); return result; }

        public int SaveChanges()
        {
            return 1;
        }

        public void Dispose()
        {
        }
    }
}
