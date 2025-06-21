using System;
using System.Collections.Generic;
using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// <para>Parsed content of all EPUB schema files (OPF package and EPUB 2 NCX / EPUB 3 OPS navigation document) of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm" /> for more information.
    /// </para>
    /// </summary>
    public class EpubSchema
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSchema" /> class.
        /// </summary>
        /// <param name="package">The parsed content of the OPF package of the EPUB book.</param>
        /// <param name="epub2Ncx">
        /// The parsed content of the EPUB 2 NCX document which is used for navigation within the EPUB 2 book or <c>null</c> if the NCX document is not present.
        /// </param>
        /// <param name="epub3NavDocument">The parsed content of the EPUB 3 navigation document of the book or <c>null</c> if the navigation document is not present.</param>
        /// <param name="mediaOverlays">A list of all SMIL (EPUB media overlay) documents in the book.</param>
        /// <param name="contentDirectoryPath">The content directory path which acts as a root directory for all content files within the EPUB book.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="package" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="contentDirectoryPath" /> parameter is <c>null</c>.</exception>
        public EpubSchema(EpubPackage package, Epub2Ncx? epub2Ncx, Epub3NavDocument? epub3NavDocument, List<Smil>? mediaOverlays, string contentDirectoryPath)
        {
            Package = package ?? throw new ArgumentNullException(nameof(package));
            Epub2Ncx = epub2Ncx;
            Epub3NavDocument = epub3NavDocument;
            MediaOverlays = mediaOverlays ?? new List<Smil>();
            ContentDirectoryPath = contentDirectoryPath ?? throw new ArgumentNullException(nameof(contentDirectoryPath));
        }

        /// <summary>
        /// <para>Gets the parsed content of the OPF package of the EPUB book.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-package-doc" /> for more information.</para>
        /// </summary>
        public EpubPackage Package { get; }

        /// <summary>
        /// <para>Gets the parsed content of the EPUB 2 NCX document which is used for navigation within the EPUB 2 book or <c>null</c> if the NCX document is not present.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" /> for more information.</para>
        /// </summary>
        public Epub2Ncx? Epub2Ncx { get; }

        /// <summary>
        /// <para>Gets the parsed content of the EPUB 3 navigation document of the book or <c>null</c> if the navigation document is not present.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav" /> for more information.</para>
        /// </summary>
        public Epub3NavDocument? Epub3NavDocument { get; }

        /// <summary>
        /// <para>Gets a list of all SMIL (EPUB media overlay) documents in the book.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-media-overlays" /> for more information.</para>
        /// </summary>
        public List<Smil> MediaOverlays { get; }

        /// <summary>
        /// Gets the content directory path which acts as a root directory for all content files within the EPUB book.
        /// </summary>
        public string ContentDirectoryPath { get; }
    }
}
