using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.Entities
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
