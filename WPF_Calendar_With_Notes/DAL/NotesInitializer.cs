using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;
using WPF_Calendar_With_Notes.DAL;

namespace WPF_Calendar_With_Notes.DAL
{
    public class NotesInitializer : System.Data.Entity.DropCreateDatabaseIfModelChanges<RealNotesContext>
    {
        protected override void Seed(RealNotesContext context)
        {
            var notes = new List<Note>
            {
                new Note { Date=DateTime.Now, Message="Plan", User=Environment.UserName }
            };
            notes.ForEach(n => context.Add(n));
            context.SaveChanges();
        }
    }
}
