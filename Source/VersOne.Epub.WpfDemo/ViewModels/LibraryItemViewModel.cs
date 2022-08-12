namespace VersOne.Epub.WpfDemo.ViewModels
{
    /// <summary>
    /// View model for a library item (i.e. a book that is displayed in the library view).
    /// </summary>
    public class LibraryItemViewModel
    {
        /// <summary>
        /// Gets or sets the unique identifier of the book in the library view. Contains the same value as <see cref="Entities.Book.Id" />.
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the title of the book in the library view.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets the content of the cover image of the book in the library view.
        /// </summary>
        public byte[] CoverImage { get; set; }
    }
}
