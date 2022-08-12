namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>A generic metadata item of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" />,
    /// <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf2-meta" />,
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataMeta
    {
        /// <summary>
        /// <para>Gets the name of the EPUB 2 generic metadata item.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.</para>
        /// </summary>
        public string Name { get; internal set; }

        /// <summary>
        /// <para>Gets the content (i.e. value) of the EPUB 2 generic metadata item or the metadata expression of the EPUB 3 generic metadata item.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Content { get; internal set; }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB 3 generic metadata item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets a relative IRI that identifies the resource augmented by the EPUB 3 generic metadata item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-refines" /> for more information.
        /// </para>
        /// </summary>
        public string Refines { get; internal set; }

        /// <summary>
        /// <para>Gets the property data type value that defines the statement being made in the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Property { get; internal set; }

        /// <summary>
        /// <para>Gets the system or scheme that the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item is drawn from.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-scheme" /> for more information.
        /// </para>
        /// </summary>
        public string Scheme { get; internal set; }
    }
}
