namespace VersOne.Epub.WpfDemo.Entities
{
    /// <summary>
    /// A class representing a single book in the library. It contains the information about a book that gets stored in the application settings.
    /// </summary>
    public class Book
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book in the library.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the absolute file path of the book file.
        /// </summary>
        public string FilePath { get; set; }

        /// <summary>
        /// Gets or sets the book's title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the book has a cover image.
        /// </summary>
        public bool HasCover { get; set; }
    }
}
