namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>An identifier associated with the EPUB book, such as a UUID, DOI, or ISBN.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcidentifier" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" />,
    /// and <see href="http://purl.org/dc/elements/1.1/identifier" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataIdentifier
    {
        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata identifier item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets the name of the system or authority that generated or assigned the identifier, for example 'ISBN' or 'DOI'.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" /> for more information.</para>
        /// </summary>
        public string Scheme { get; internal set; }

        /// <summary>
        /// <para>Gets the unambiguous identifier for the EPUB book.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcidentifier" /> for more information.</para>
        /// </summary>
        public string Identifier { get; internal set; }
    }
}
