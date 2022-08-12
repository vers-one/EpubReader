namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Creator of the book. Represents the name of a person, organization, etc. responsible for the creation of the content of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.2" />,
    /// and <see href="http://purl.org/dc/elements/1.1/creator" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataCreator
    {
        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata creator item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets the name of the creator as the author intends it to be displayed to a user.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.</para>
        /// </summary>
        public string Creator { get; internal set; }

        /// <summary>
        /// <para>Gets the normalized form of the name of the creator for sorting.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#file-as" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string FileAs { get; internal set; }

        /// <summary>
        /// <para>Gets the creator's role which indicates the function the creator played in the creation of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#role" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string Role { get; internal set; }
    }
}
