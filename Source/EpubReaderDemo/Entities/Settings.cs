using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EpubReaderDemo.Entities
{
    internal class Settings
    {
        public List<Book> Books { get; set; }

        public static Settings Empty
        {
            get
            {
                return new Settings
                {
                    Books = new List<Book>()
                };
            }
        }
    }
}
