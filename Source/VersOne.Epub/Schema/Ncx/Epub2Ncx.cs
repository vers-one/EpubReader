using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the EPUB 2 NCX (Navigation Center eXtended, also known as Navigation Control file for XML applications) document of the EPUB book.
    /// NCX document exposes the hierarchical structure of the book to allow the user to navigate through it.
    /// </para>
    /// <para>
    /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" />,
    /// <see href="https://daisy.org/activities/standards/daisy/daisy-3/z39-86-2005-r2012-specifications-for-the-digital-talking-book/#NCX" />,
    /// and <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.
    /// </para>
    /// </summary>
    public class Epub2Ncx
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Epub2Ncx" /> class.
        /// </summary>
        /// <param name="filePath">The absolute path of the EPUB 2 NCX document file in the EPUB archive.</param>
        /// <param name="head">The NCX document head which contains all NCX metadata.</param>
        /// <param name="docTitle">The title of the EPUB book or <c>null</c> if the title is not provided.</param>
        /// <param name="docAuthors">The list of authors of the EPUB book.</param>
        /// <param name="navMap">
        /// The NCX navigation map which acts as a container for one or more NCX navigation points for the primary navigation within the book (e.g. table of contents).
        /// </param>
        /// <param name="pageList">
        /// The NCX page list containing one or more NCX page targets which provide pagination information
        /// or <c>null</c> if the NCX document doesn't have a page list.
        /// </param>
        /// <param name="navLists">
        /// A list of NCX navigation lists which contain distinct, flat sets of navigable elements for the secondary navigation within the book
        /// (e.g., lists of notes, figures, tables, etc.).
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="filePath" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="head" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="navMap" /> parameter is <c>null</c>.</exception>
        public Epub2Ncx(string filePath, Epub2NcxHead head, string? docTitle, List<string>? docAuthors, Epub2NcxNavigationMap navMap,
            Epub2NcxPageList? pageList = null, List<Epub2NcxNavigationList>? navLists = null)
        {
            FilePath = filePath ?? throw new ArgumentNullException(nameof(filePath));
            Head = head ?? throw new ArgumentNullException(nameof(head));
            DocTitle = docTitle;
            DocAuthors = docAuthors ?? new List<string>();
            NavMap = navMap ?? throw new ArgumentNullException(nameof(navMap));
            PageList = pageList;
            NavLists = navLists ?? new List<Epub2NcxNavigationList>();
        }

        /// <summary>
        /// Gets the absolute path of the EPUB 2 NCX document file in the EPUB archive.
        /// </summary>
        public string FilePath { get; }

        /// <summary>
        /// <para>Gets the NCX document head which contains all NCX metadata.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxHead Head { get; }

        /// <summary>
        /// <para>Gets the title of the EPUB book or <c>null</c> if the title is not provided.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public string? DocTitle { get; }

        /// <summary>
        /// <para>Gets the list of authors of the EPUB book.</para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<string> DocAuthors { get; }

        /// <summary>
        /// <para>
        /// Gets the NCX navigation map which acts as a container for one or more NCX navigation points for the primary navigation within the book (e.g. table of contents).
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxNavigationMap NavMap { get; }

        /// <summary>
        /// <para>
        /// Gets the NCX page list containing one or more NCX page targets which provide pagination information
        /// or <c>null</c> if the NCX document doesn't have a page list.
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public Epub2NcxPageList? PageList { get; }

        /// <summary>
        /// <para>
        /// Gets a list of NCX navigation lists which contain distinct, flat sets of navigable elements for the secondary navigation within the book
        /// (e.g., lists of notes, figures, tables, etc.).
        /// </para>
        /// <para>See <see href="http://www.daisy.org/z3986/2005/ncx-2005-1.dtd" /> for more information.</para>
        /// </summary>
        public List<Epub2NcxNavigationList> NavLists { get; }
    }
}
