using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a EPUB book with all its content and metadata.
    /// </summary>
    public class EpubBook
    {
        /// <summary>
        /// Gets the path to the EPUB file.
        /// </summary>
        public string FilePath { get; internal set; }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// Gets a comma separated list of the book's authors.
        /// </summary>
        public string Author { get; internal set; }

        /// <summary>
        /// Gets a list of book's authors names.
        /// </summary>
        public List<string> AuthorList { get; internal set; }

        /// <summary>
        /// Gets the book's description or <c>null</c> if the description is not present in the book.
        /// </summary>
        public string Description { get; internal set; }

        /// <summary>
        /// Gets the book's cover image or <c>null</c> if there is no cover.
        /// </summary>
        public byte[] CoverImage { get; internal set; }

        /// <summary>
        /// Gets a list of text content files in the order of reading intended by the author.
        /// </summary>
        public List<EpubTextContentFile> ReadingOrder { get; internal set; }

        /// <summary>
        /// Gets a list of navigation elements of the book (typically the table of contents).
        /// </summary>
        public List<EpubNavigationItem> Navigation { get; internal set; }

        /// <summary>
        /// Gets the parsed EPUB schema of the book.
        /// </summary>
        public EpubSchema Schema { get; internal set; }

        /// <summary>
        /// Gets the collection of the book's content files.
        /// </summary>
        public EpubContent Content { get; internal set; }
    }
}
