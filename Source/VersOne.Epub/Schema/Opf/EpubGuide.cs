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
    public class EpubGuide : List<EpubGuideReference>
    {
    }
}
