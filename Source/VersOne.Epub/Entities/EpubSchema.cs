using VersOne.Epub.Schema;

namespace VersOne.Epub
{
    /// <summary>
    /// <para>Parsed content of all EPUB schema files (OPF package and EPUB 2 NCX / EPUB 3 OPS navigation document) of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm" /> for more information.
    /// </para>
    /// </summary>
    public class EpubSchema
    {
        /// <summary>
        /// <para>Gets the parsed content of the OPF package of the EPUB book.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-doc" /> for more information.</para>
        /// </summary>
        public EpubPackage Package { get; internal set; }

        /// <summary>
        /// <para>Gets the parsed content of the EPUB 2 NCX document which is used for navigation within the EPUB 2 book or <c>null</c> if the NCX document is not present.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4.1" /> for more information.</para>
        /// </summary>
        public Epub2Ncx Epub2Ncx { get; internal set; }

        /// <summary>
        /// <para>Gets the parsed content of the EPUB 3 navigation document of the book or <c>null</c> if the navigation document is not present.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-package-nav" /> for more information.</para>
        /// </summary>
        public Epub3NavDocument Epub3NavDocument { get; internal set; }

        /// <summary>
        /// Gets the content directory path which acts as a root directory for all content files within the EPUB book.
        /// </summary>
        public string ContentDirectoryPath { get; internal set; }
    }
}
