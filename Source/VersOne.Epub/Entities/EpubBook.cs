using System;
using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a EPUB book with all its content and metadata.
    /// </summary>
    public class EpubBook
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubBook" /> class.
        /// </summary>
        /// <param name="filePath">The path to the EPUB file or <c>null</c> if the EPUB file is being loaded from a stream.</param>
        /// <param name="title">The title of the book.</param>
        /// <param name="author">A comma separated list of the book's authors.</param>
        /// <param name="authorList">A list of book's authors names.</param>
        /// <param name="description">The book's description or <c>null</c> if the description is not present in the book.</param>
        /// <param name="coverImage">The book's cover image or <c>null</c> if there is no cover.</param>
        /// <param name="readingOrder">A list of text content files in the order of reading intended by the author.</param>
        /// <param name="navigation">A list of navigation elements of the book (typically the table of contents) or <c>null</c> if the book doesn't have navigation information.</param>
        /// <param name="schema">The parsed EPUB schema of the book.</param>
        /// <param name="content">The collection of the book's content files.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="title"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="author"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="schema"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> parameter is <c>null</c>.</exception>
        public EpubBook(string? filePath, string title, string author, List<string>? authorList, string? description, byte[]? coverImage,
            List<EpubLocalTextContentFile>? readingOrder, List<EpubNavigationItem>? navigation, EpubSchema schema, EpubContent content)
        {
            FilePath = filePath;
            Title = title ?? throw new ArgumentNullException(nameof(title));
            Author = author ?? throw new ArgumentNullException(nameof(author));
            AuthorList = authorList ?? new List<string>();
            Description = description;
            CoverImage = coverImage;
            ReadingOrder = readingOrder ?? new List<EpubLocalTextContentFile>();
            Navigation = navigation;
            Schema = schema ?? throw new ArgumentNullException(nameof(schema));
            Content = content ?? throw new ArgumentNullException(nameof(content));
        }

        /// <summary>
        /// Gets the path to the EPUB file or <c>null</c> if the EPUB file was loaded from a stream.
        /// </summary>
        public string? FilePath { get; }

        /// <summary>
        /// Gets the title of the book.
        /// </summary>
        public string Title { get; }

        /// <summary>
        /// Gets a comma separated list of the book's authors.
        /// </summary>
        public string Author { get; }

        /// <summary>
        /// Gets a list of book's authors names.
        /// </summary>
        public List<string> AuthorList { get; }

        /// <summary>
        /// Gets the book's description or <c>null</c> if the description is not present in the book.
        /// </summary>
        public string? Description { get; }

        /// <summary>
        /// Gets the book's cover image or <c>null</c> if there is no cover.
        /// </summary>
        public byte[]? CoverImage { get; }

        /// <summary>
        /// Gets a list of text content files in the order of reading intended by the author.
        /// </summary>
        public List<EpubLocalTextContentFile> ReadingOrder { get; }

        /// <summary>
        /// Gets a list of navigation elements of the book (typically the table of contents) or <c>null</c> if the book doesn't have navigation information.
        /// </summary>
        public List<EpubNavigationItem>? Navigation { get; }

        /// <summary>
        /// Gets the parsed EPUB schema of the book.
        /// </summary>
        public EpubSchema Schema { get; }

        /// <summary>
        /// Gets the collection of the book's content files.
        /// </summary>
        public EpubContent Content { get; }
    }
}
