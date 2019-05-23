using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPF_Calendar_With_Notes.CommonTypes;

namespace WPF_Calendar_With_Notes.DAL
{
    public class RealNotesContext : DbContext, INotesContext<Note>
    {
        public RealNotesContext()
            : base("RealNotesContext")
        {
            //Database.SetInitializer<NotesContext>(new CreateDatabaseIfNotExists<NotesContext>());
            //Database.SetInitializer<NotesContext>(new DropCreateDatabaseIfModelChanges<NotesContext>());
            //Database.SetInitializer<NotesContext>(new DropCreateDatabaseAlways<NotesContext>());
            //Database.SetInitializer<NotesContext>(new NotesInitializer());        
        }

        public DbSet<Note> Notes { get; set; }

        public IEnumerable<Note> BrokerNotes
        {
            get { return Notes; }
        }

        public void Add(Note note)
        {
            Notes.Add(note);
        }

        public bool Remove(Note note)
        {
            var result = Notes.Remove(note);

            if (result != null) return true;
            else
                return false;
        }

    }
}
