using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPF_Calendar_With_Notes.DAL
{
    public class DBSingleton<T> where T: new()
    {
        private static readonly Lazy<T> _instancja = new Lazy<T>(() => new T());

        private DBSingleton() { }

        public static T Instancja
        {
            get
            {
                return _instancja.Value;
            }
        }
    }
}
