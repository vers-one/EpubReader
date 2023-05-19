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
        /// Initializes a new instance of the <see cref="EpubNavigationItemLink" /> class using a specified content file URL with anchor and a specified base directory path.
        /// </summary>
        /// <param name="contentFileUrlWithAnchor">Relative file path of the content file with an optional anchor (as it is specified in the EPUB manifest).</param>
        /// <param name="baseDirectoryPath">The path of the directory within the EPUB archive which acts as a base directory for a relative content file path.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentFileUrlWithAnchor" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="baseDirectoryPath" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="contentFileUrlWithAnchor" /> parameter points to a remote URL.</exception>
        public EpubNavigationItemLink(string contentFileUrlWithAnchor, string baseDirectoryPath)
        {
            if (contentFileUrlWithAnchor == null)
            {
                throw new ArgumentNullException(nameof(contentFileUrlWithAnchor));
            }
            if (baseDirectoryPath == null)
            {
                throw new ArgumentNullException(nameof(baseDirectoryPath));
            }
            if (!ContentPathUtils.IsLocalPath(contentFileUrlWithAnchor))
            {
                throw new ArgumentException($"\"{contentFileUrlWithAnchor}\" points to a remote resource.");
            }
            UrlParser urlParser = new(contentFileUrlWithAnchor);
            ContentFileUrl = urlParser.Path;
            ContentFilePath = ContentPathUtils.Combine(baseDirectoryPath, ContentFileUrl);
            Anchor = urlParser.Anchor;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNavigationItemLink" /> class with specified content file URL without anchor, content file path, and anchor.
        /// </summary>
        /// <param name="contentFileUrlWithoutAnchor">The file path portion of the content file URL.</param>
        /// <param name="contentFilePath">The file path portion of the content file URL converted to the absolute file path within the EPUB archive.</param>
        /// <param name="anchor">The anchor portion of the content file URL.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contentFileUrlWithoutAnchor" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="contentFilePath" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentException">The <paramref name="contentFileUrlWithoutAnchor" /> parameter points to a remote URL.</exception>
        public EpubNavigationItemLink(string contentFileUrlWithoutAnchor, string contentFilePath, string? anchor)
        {
            ContentFileUrl = contentFileUrlWithoutAnchor ?? throw new ArgumentNullException(nameof(contentFileUrlWithoutAnchor));
            ContentFilePath = contentFilePath ?? throw new ArgumentNullException(nameof(contentFilePath));
            if (!ContentPathUtils.IsLocalPath(contentFileUrlWithoutAnchor))
            {
                throw new ArgumentException($"\"{contentFileUrlWithoutAnchor}\" points to a remote resource.");
            }
            Anchor = anchor;
        }

        /// <summary>
        /// Gets the file path portion of the content file URL.
        /// For example, if the content file URL is '../content/chapter1.html#section1', then the value of this property will be '../content/chapter1.html'.
        /// </summary>
        public string ContentFileUrl { get; }

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
