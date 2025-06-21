using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Represents a unified collection of EPUB metadata items. A collection allows resources to be assembled into logical groups
    /// for a variety of potential uses: enabling content that has been split across multiple EPUB Content Documents to be reassembled
    /// back into a meaningful unit (e.g., an index split across multiple documents), identifying resources for specialized purposes
    /// (e.g., preview content), or collecting together resources that present additional information about this EPUB book.
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-collection-elem" /> for more information.</para>
    /// </summary>
    public class EpubCollection
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubCollection"/> class.
        /// </summary>
        /// <param name="role">The role of this collection.</param>
        /// <param name="metadata">The EPUB meta information included into this collection or <c>null</c> if the collection doesn't have any meta information.</param>
        /// <param name="nestedCollections">A list of sub-collections included in this collection.</param>
        /// <param name="links">A list of metadata links.</param>
        /// <param name="id">The unique ID of this collection or <c>null</c> if the collection doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this collection or <c>null</c> if the collection doesn't specify a text direction.</param>
        /// <param name="language">The language of this collection or <c>null</c> if the collection doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="role" /> parameter is <c>null</c>.</exception>
        public EpubCollection(string role, EpubMetadata? metadata = null, List<EpubCollection>? nestedCollections = null, List<EpubMetadataLink>? links = null,
            string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Role = role ?? throw new ArgumentNullException(nameof(role));
            Metadata = metadata;
            NestedCollections = nestedCollections ?? new List<EpubCollection>();
            Links = links ?? new List<EpubMetadataLink>();
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the role of this collection.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-collection-role" /> for more information.</para>
        /// </summary>
        public string Role { get; }

        /// <summary>
        /// <para>Gets the EPUB meta information included into this collection or <c>null</c> if the collection doesn't have any meta information.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-metadata-elem" /> for more information.</para>
        /// </summary>
        public EpubMetadata? Metadata { get; }

        /// <summary>
        /// <para>Gets a list of sub-collections included in this collection.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#elemdef-collection" /> for more information.</para>
        /// </summary>
        public List<EpubCollection> NestedCollections { get; }

        /// <summary>
        /// <para>Gets a list of metadata links. Links are used to associate resources with this collection, such as metadata records.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-link-elem" /> for more information.</para>
        /// </summary>
        public List<EpubMetadataLink> Links { get; }

        /// <summary>
        /// <para>Gets the unique ID of this collection or <c>null</c> if the collection doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this collection or <c>null</c> if the collection doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this collection or <c>null</c> if the collection doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
