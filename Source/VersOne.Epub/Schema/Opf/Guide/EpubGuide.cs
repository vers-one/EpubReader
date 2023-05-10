using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>EPUB 2 guide. Provides machine-processable navigation to the key structural components of the EPUB book (e.g., cover page, table of contents, etc.).</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-guide" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.
    /// </para>
    /// </summary>
    public class EpubGuide
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubGuide" /> class.
        /// </summary>
        /// <param name="items">A list of EPUB 2 guide references to the key structural components of the EPUB book (e.g., cover page, table of contents, etc.).</param>
        public EpubGuide(List<EpubGuideReference>? items = null)
        {
            Items = items ?? new List<EpubGuideReference>();
        }

        /// <summary>
        /// <para>Gets a list of EPUB 2 guide references to the key structural components of the EPUB book (e.g., cover page, table of contents, etc.).</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-guide" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubGuideReference> Items { get; }
    }
}
