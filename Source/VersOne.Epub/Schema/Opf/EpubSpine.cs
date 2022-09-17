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
        /// <para>Gets the unique ID of this EPUB spine.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets the page progression direction which defines the global direction in which the content flows.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-spine-page-progression-direction" /> for more information.</para>
        /// </summary>
        public EpubPageProgressionDirection? PageProgressionDirection { get; internal set; }

        /// <summary>
        /// <para>Gets the value of the <see cref="EpubManifestItem.Id" /> property of the EPUB 2 NCX file declared in the <see cref="EpubManifest" />.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.</para>
        /// </summary>
        public string Toc { get; internal set; }

        /// <summary>
        /// <para>Gets an ordered list of <see cref="EpubSpineItemRef" /> items that represent the default reading order of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-pkg-spine" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubSpineItemRef> Items { get; internal set; }
    }
}
