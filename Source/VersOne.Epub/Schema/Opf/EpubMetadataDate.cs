namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Publication date of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcdate" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
    /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataDate
    {
        /// <summary>
        /// <para>Gets the publication date of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcdate" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
        /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
        /// </para>
        /// </summary>
        public string Date { get; internal set; }

        /// <summary>
        /// <para>Gets the name of the event represented by this date (e.g., creation, publication, modification, etc.).</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" /> for more information.
        /// </para>
        /// </summary>
        public string Event { get; internal set; }
    }
}
