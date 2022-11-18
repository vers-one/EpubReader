using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>EPUB manifest. Provides an exhaustive list of the content items that constitute the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-manifest-elem" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3" /> for more information.
    /// </para>
    /// </summary>
    public class EpubManifest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubManifest" /> class.
        /// </summary>
        /// <param name="items">A list of the content items that constitute the EPUB book.</param>
        public EpubManifest(List<EpubManifestItem>? items = null)
        {
            Items = items ?? new List<EpubManifestItem>();
        }

        /// <summary>
        /// <para>Gets a list of the content items that constitute the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-manifest-elem" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubManifestItem> Items { get; }
    }
}
