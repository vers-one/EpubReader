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
    public class EpubManifest : List<EpubManifestItem>
    {
    }
}
