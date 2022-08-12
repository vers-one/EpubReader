using System.Collections.Generic;

namespace VersOne.Epub.WpfDemo.Entities
{
    /// <summary>
    /// Application settings. Contains the list of the books in the library.
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Gets a new empty instance of the <see cref="Settings" /> class.
        /// </summary>
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

        /// <summary>
        /// Gets or sets a list of books in the library.
        /// </summary>
        public List<Book> Books { get; set; }
    }
}
