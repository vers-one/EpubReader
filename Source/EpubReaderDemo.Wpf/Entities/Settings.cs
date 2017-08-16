using System.Collections.Generic;

namespace EpubReaderDemo.Wpf.Entities
{
    public class Settings
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
