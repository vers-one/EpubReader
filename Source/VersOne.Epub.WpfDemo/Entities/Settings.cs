using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.Entities
{
    public class Settings
    {
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

        public List<Book> Books { get; set; }
    }
}
