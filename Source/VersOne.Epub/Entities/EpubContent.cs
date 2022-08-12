using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// A container for all content files within the EPUB book.
    /// </summary>
    public class EpubContent
    {
        /// <summary>
        /// Gets the content file for the cover image of the EPUB book or <c>null</c> if the book doesn't have a cover.
        /// </summary>
        public EpubByteContentFile Cover { get; internal set; }

        /// <summary>
        /// Gets the EPUB 3 navigation document of the EPUB book or <c>null</c> if the book doesn't have a EPUB 3 navigation document.
        /// </summary>
        public EpubTextContentFile NavigationHtmlFile { get; internal set; }

        /// <summary>
        /// Gets all HTML/XHTML content files of the EPUB book keyed by their relative file paths.
        /// </summary>
        public Dictionary<string, EpubTextContentFile> Html { get; internal set; }

        /// <summary>
        /// Gets all CSS files of the EPUB book keyed by their relative file paths.
        /// </summary>
        public Dictionary<string, EpubTextContentFile> Css { get; internal set; }

        /// <summary>
        /// Gets all image files of the EPUB book keyed by their relative file paths.
        /// </summary>
        public Dictionary<string, EpubByteContentFile> Images { get; internal set; }

        /// <summary>
        /// Gets all embedded font files of the EPUB book keyed by their relative file paths.
        /// </summary>
        public Dictionary<string, EpubByteContentFile> Fonts { get; internal set; }

        /// <summary>
        /// Gets all content files of the EPUB book keyed by their relative file paths.
        /// </summary>
        public Dictionary<string, EpubContentFile> AllFiles { get; internal set; }
    }
}
