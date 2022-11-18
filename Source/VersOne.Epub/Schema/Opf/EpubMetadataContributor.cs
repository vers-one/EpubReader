using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Contributor of the book. Represents the name of a person, organization, etc. that played a secondary role in the creation of the content of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccontributor" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.6" />,
    /// and <see href="http://purl.org/dc/elements/1.1/contributor" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataContributor
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataContributor" /> class.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB metadata contributor item.</param>
        /// <param name="contributor">The name of the contributor as the author intends it to be displayed to a user.</param>
        /// <param name="fileAs">The normalized form of the name of the contributor for sorting.</param>
        /// <param name="role">The contributor's role which describes the nature of work performed by the contributor.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="contributor"/> argument is <c>null</c>.</exception>
        public EpubMetadataContributor(string? id, string contributor, string? fileAs, string? role)
        {
            Id = id;
            Contributor = contributor ?? throw new ArgumentNullException(nameof(contributor));
            FileAs = fileAs;
            Role = role;
        }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata contributor item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the name of the contributor as the author intends it to be displayed to a user.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccontributor" /> for more information.</para>
        /// </summary>
        public string Contributor { get; }

        /// <summary>
        /// <para>Gets the normalized form of the name of the contributor for sorting.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#file-as" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccontributor" /> for more information.
        /// </para>
        /// </summary>
        public string? FileAs { get; }

        /// <summary>
        /// <para>Gets the contributor's role which describes the nature of work performed by the contributor.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#role" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dccontributor" /> for more information.
        /// </para>
        /// </summary>
        public string? Role { get; }
    }
}
