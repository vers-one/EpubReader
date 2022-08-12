using VersOne.Epub.Internal;

namespace VersOne.Epub
{
    /// <summary>
    /// Navigation link representing a pointer to a specific place in the book's content.
    /// </summary>
    public class EpubNavigationItemLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemLink" /> class with a specified content file URL and a specified base directory path.
        /// </summary>
        /// <param name="contentFileUrl">Relative file path or a URL of the content file with an optional anchor (as it is specified in the EPUB manifest).</param>
        /// <param name="baseDirectoryPath">The path of the directory within the EPUB archive which acts as a base directory for a relative content file path.</param>
        public EpubNavigationItemLink(string contentFileUrl, string baseDirectoryPath)
        {
            UrlParser urlParser = new UrlParser(contentFileUrl);
            ContentFileName = urlParser.Path;
            ContentFilePathInEpubArchive = ZipPathUtils.Combine(baseDirectoryPath, ContentFileName);
            Anchor = urlParser.Anchor;
        }

        /// <summary>
        /// Gets the file path portion of the content file URL.
        /// For example, if the content file URL is '../content/chapter1.html#section1', then the value of this property will be '../content/chapter1.html'.
        /// </summary>
        public string ContentFileName { get; internal set; }

        /// <summary>
        /// Gets the file path portion of the content file URL converted to the absolute file path within the EPUB arcive.
        /// For example, if the content file URL is '../content/chapter1.html#section1' and the base directory path is 'OPS/toc',
        /// then the value of this property will be 'OPS/content/chapter1.html'.
        /// </summary>
        public string ContentFilePathInEpubArchive { get; internal set; }

        /// <summary>
        /// Gets the anchor portion of the content file URL.
        /// For example, if the content file URL is '../content/chapter1.html#section1', then the value of this property will be 'section1'.
        /// </summary>
        public string Anchor { get; internal set; }
    }
}
