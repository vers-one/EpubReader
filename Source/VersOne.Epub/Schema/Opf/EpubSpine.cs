using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>EPUB spine. Defines an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
    /// </para>
    /// </summary>
    public class EpubSpine
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSpine" /> class.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB spine or <c>null</c> if the spine doesn't have an ID.</param>
        /// <param name="pageProgressionDirection">
        /// The page progression direction which defines the global direction in which the content flows
        /// or <c>null</c> if the spine doesn't specify a page progression direction.
        /// </param>
        /// <param name="toc">
        /// The value of the <see cref="EpubManifestItem.Id" /> property of the EPUB 2 NCX file declared in the <see cref="EpubManifest" />
        /// or <c>null</c> if the spine doesn't specify a EPUB 2 NCX file.
        /// </param>
        /// <param name="items">An ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</param>
        public EpubSpine(string? id = null, EpubPageProgressionDirection? pageProgressionDirection = null, string? toc = null, List<EpubSpineItemRef>? items = null)
        {
            Id = id;
            PageProgressionDirection = pageProgressionDirection;
            Toc = toc;
            Items = items ?? new List<EpubSpineItemRef>();
        }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB spine or <c>null</c> if the spine doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>
        /// Gets the page progression direction which defines the global direction in which the content flows
        /// or <c>null</c> if the spine doesn't specify a page progression direction.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-spine-page-progression-direction" /> for more information.</para>
        /// </summary>
        public EpubPageProgressionDirection? PageProgressionDirection { get; }

        /// <summary>
        /// <para>
        /// Gets the value of the <see cref="EpubManifestItem.Id" /> property of the EPUB 2 NCX file declared in the <see cref="EpubManifest" />
        /// or <c>null</c> if the spine doesn't specify a EPUB 2 NCX file.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.</para>
        /// </summary>
        public string? Toc { get; }

        /// <summary>
        /// <para>Gets an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubSpineItemRef> Items { get; }
    }
}
