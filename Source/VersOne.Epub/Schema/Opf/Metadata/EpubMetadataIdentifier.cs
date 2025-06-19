using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>An identifier associated with the EPUB book, such as a UUID, DOI, or ISBN.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcidentifier" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" />,
    /// and <see href="http://purl.org/dc/elements/1.1/identifier" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataIdentifier
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataIdentifier" /> class.
        /// </summary>
        /// <param name="identifier">The unambiguous identifier for the EPUB book.</param>
        /// <param name="id">The unique ID of this EPUB metadata identifier item or <c>null</c> if the metadata identifier doesn't have an ID.</param>
        /// <param name="scheme">
        /// The name of the system or authority that generated or assigned the identifier, for example 'ISBN' or 'DOI' or <c>null</c> if the identified doesn't have a scheme.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="identifier" /> parameter is <c>null</c>.</exception>
        public EpubMetadataIdentifier(string identifier, string? id = null, string? scheme = null)
        {
            Identifier = identifier ?? throw new ArgumentNullException(nameof(identifier));
            Id = id;
            Scheme = scheme;
        }

        /// <summary>
        /// <para>Gets the unambiguous identifier for the EPUB book.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcidentifier" /> for more information.</para>
        /// </summary>
        public string Identifier { get; }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata identifier item or <c>null</c> if the metadata identifier doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>
        /// Gets the name of the system or authority that generated or assigned the identifier, for example 'ISBN' or 'DOI' or <c>null</c> if the identified doesn't have a scheme.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.10" /> for more information.</para>
        /// </summary>
        public string? Scheme { get; }
    }
}
