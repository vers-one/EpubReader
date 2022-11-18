using System;
using VersOne.Epub.Internal;

namespace VersOne.Epub
{
    /// <summary>
    /// Navigation link representing a pointer to a specific place in the book's content.
    /// </summary>
    public class EpubNavigationItemLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemLink" /> class using a specified content file URL and a specified base directory path.
        /// </summary>
        /// <param name="contentFileUrl">Relative file path or a URL of the content file with an optional anchor (as it is specified in the EPUB manifest).</param>
        /// <param name="baseDirectoryPath">The path of the directory within the EPUB archive which acts as a base directory for a relative content file path.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentFileUrl"/> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="baseDirectoryPath"/> parameter is <c>null</c>.</exception>
        public EpubNavigationItemLink(string contentFileUrl, string baseDirectoryPath)
        {
            if (contentFileUrl == null)
            {
                throw new ArgumentNullException(nameof(contentFileUrl));
            }
            if (baseDirectoryPath == null)
            {
                throw new ArgumentNullException(nameof(baseDirectoryPath));
            }
            UrlParser urlParser = new(contentFileUrl);
            ContentFileName = urlParser.Path;
            ContentFilePath = ZipPathUtils.Combine(baseDirectoryPath, ContentFileName);
            Anchor = urlParser.Anchor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemLink" /> class with specified content file name, content file path in EPUB archive, and anchor.
        /// </summary>
        /// <param name="contentFileName">The file path portion of the content file URL.</param>
        /// <param name="contentFilePath">The file path portion of the content file URL converted to the absolute file path within the EPUB archive.</param>
        /// <param name="anchor">The anchor portion of the content file URL.</param>
        public EpubNavigationItemLink(string contentFileName, string contentFilePath, string? anchor)
        {
            ContentFileName = contentFileName ?? throw new ArgumentNullException(nameof(contentFileName));
            ContentFilePath = contentFilePath ?? throw new ArgumentNullException(nameof(contentFilePath));
            Anchor = anchor;
        }

        /// <summary>
        /// Gets the file path portion of the content file URL.
        /// For example, if the content file URL is '../content/chapter1.html#section1', then the value of this property will be '../content/chapter1.html'.
        /// </summary>
        public string ContentFileName { get; }

        /// <summary>
        /// Gets the file path portion of the content file URL converted to the absolute file path within the EPUB archive.
        /// For example, if the content file URL is '../content/chapter1.html#section1' and the base directory path is 'OPS/toc',
        /// then the value of this property will be 'OPS/content/chapter1.html'.
        /// </summary>
        public string ContentFilePath { get; }

        /// <summary>
        /// Gets the anchor portion of the content file URL.
        /// For example, if the content file URL is '../content/chapter1.html#section1', then the value of this property will be 'section1'.
        /// </summary>
        public string? Anchor { get; }
    }
}
