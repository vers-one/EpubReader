using System;

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
        /// Initializes a new instance of the <see cref="EpubMetadataMeta" /> class with specified name and content.
        /// </summary>
        /// <param name="name">The name of the EPUB 2 generic metadata item or <c>null</c> for EPUB 3 generic metadata item.</param>
        /// <param name="content">The content (i.e. value) of the EPUB 2 generic metadata item or the metadata expression of the EPUB 3 generic metadata item.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> argument is <c>null</c>.</exception>
        public EpubMetadataMeta(string? name, string content)
            : this(name, content, null, null, null, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataMeta" /> class with specified name, content, ID, refining resource, property, and scheme.
        /// </summary>
        /// <param name="name">The name of the EPUB 2 generic metadata item or <c>null</c> for EPUB 3 generic metadata item.</param>
        /// <param name="content">The content (i.e. value) of the EPUB 2 generic metadata item or the metadata expression of the EPUB 3 generic metadata item.</param>
        /// <param name="id">The unique ID of this EPUB 3 generic metadata item or <c>null</c> if the generic metadata item doesn't have an ID.</param>
        /// <param name="refines">
        /// A relative IRI that identifies the resource augmented by the EPUB 3 generic metadata item
        /// or <c>null</c> if the generic metadata item doesn't specify any augmentation.
        /// </param>
        /// <param name="property">
        /// The property data type value that defines the statement being made in the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item
        /// or <c>null</c> for EPUB 2 generic metadata item.
        /// </param>
        /// <param name="scheme">
        /// The system or scheme that the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item is drawn from
        /// or <c>null</c> if the generic metadata item doesn't specify a scheme for the expression.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="content"/> argument is <c>null</c>.</exception>
        public EpubMetadataMeta(string? name, string content, string? id, string? refines, string? property, string? scheme)
        {
            Name = name;
            Content = content ?? throw new ArgumentNullException(nameof(content));
            Id = id;
            Refines = refines;
            Property = property;
            Scheme = scheme;
        }

        /// <summary>
        /// <para>Gets the name of the EPUB 2 generic metadata item or <c>null</c> for EPUB 3 generic metadata item.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" /> for more information.</para>
        /// </summary>
        public string? Name { get; }

        /// <summary>
        /// <para>Gets the content (i.e. value) of the EPUB 2 generic metadata item or the metadata expression of the EPUB 3 generic metadata item.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB 3 generic metadata item or <c>null</c> if the generic metadata item doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>
        /// Gets a relative IRI that identifies the resource augmented by the EPUB 3 generic metadata item
        /// or <c>null</c> if the generic metadata item doesn't specify any augmentation.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-refines" /> for more information.
        /// </para>
        /// </summary>
        public string? Refines { get; }

        /// <summary>
        /// <para>
        /// Gets the property data type value that defines the statement being made in the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item
        /// or <c>null</c> for EPUB 2 generic metadata item.
        /// </para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-meta-elem" /> for more information.
        /// </para>
        /// </summary>
        public string? Property { get; }

        /// <summary>
        /// <para>
        /// Gets the system or scheme that the expression (see <see cref="Content" />) of the EPUB 3 generic metadata item is drawn from
        /// or <c>null</c> if the generic metadata item doesn't specify a scheme for the expression.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-scheme" /> for more information.
        /// </para>
        /// </summary>
        public string? Scheme { get; }
    }
}
