using System;

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
        /// Initializes a new instance of the <see cref="EpubMetadataCreator" /> class with the specified creator's name.
        /// </summary>
        /// <param name="creator">The name of the creator as the author intends it to be displayed to a user.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="creator" /> parameter is <c>null</c>.</exception>
        public EpubMetadataCreator(string creator)
            : this(null, creator, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataCreator" /> class with specified ID, creator's name, creator's sorting name, and role.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB metadata creator item.</param>
        /// <param name="creator">The name of the creator as the author intends it to be displayed to a user.</param>
        /// <param name="fileAs">The normalized form of the name of the creator for sorting.</param>
        /// <param name="role">The creator's role which indicates the function the creator played in the creation of the content of the EPUB book.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="creator" /> parameter is <c>null</c>.</exception>
        public EpubMetadataCreator(string? id, string creator, string? fileAs, string? role)
        {
            Id = id;
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            FileAs = fileAs;
            Role = role;
        }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata creator item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the name of the creator as the author intends it to be displayed to a user.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.</para>
        /// </summary>
        public string Creator { get; }

        /// <summary>
        /// <para>Gets the normalized form of the name of the creator for sorting.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#file-as" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string? FileAs { get; }

        /// <summary>
        /// <para>Gets the creator's role which indicates the function the creator played in the creation of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#role" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string? Role { get; }
    }
}
