using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>A metadata link. Links are used to associate resources with the EPUB book, such as metadata records.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-elem" /> for more information.</para>
    /// </summary>
    public class EpubMetadataLink
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataLink" /> class.
        /// </summary>
        /// <param name="id">The unique ID of this link or <c>null</c> if the link doesn't have an ID.</param>
        /// <param name="href">The location of the linked resource.</param>
        /// <param name="mediaType">The media type of the linked resource or <c>null</c> if the link doesn't specify the media type.</param>
        /// <param name="properties">
        /// A list of the link properties used to establish the type of record a referenced resource represents or <c>null</c> if the link doesn't specify properties.
        /// </param>
        /// <param name="refines">A relative IRI that identifies the resource augmented by the link or <c>null</c> if the link doesn't specify any augmentation.</param>
        /// <param name="relationships">A list of properties that establish the relationship the resource has with the EPUB book.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="href" /> parameter is <c>null</c>.</exception>
        public EpubMetadataLink(string? id, string href, string? mediaType, List<EpubMetadataLinkProperty>? properties, string? refines,
            List<EpubMetadataLinkRelationship>? relationships)
        {
            Id = id;
            Href = href ?? throw new ArgumentNullException(nameof(href));
            MediaType = mediaType;
            Properties = properties;
            Refines = refines;
            Relationships = relationships ?? new List<EpubMetadataLinkRelationship>();
        }

        /// <summary>
        /// <para>Gets the unique ID of this link or <c>null</c> if the link doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the location of the linked resource.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-elem" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-href" /> for more information.
        /// </para>
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// <para>Gets the media type of the linked resource or <c>null</c> if the link doesn't specify the media type.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-link-media-type" /> for more information.
        /// </para>
        /// </summary>
        public string? MediaType { get; }

        /// <summary>
        /// <para>Gets a list of the link properties used to establish the type of record a referenced resource represents or <c>null</c> if the link doesn't specify properties.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-properties" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-properties" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataLinkProperty>? Properties { get; }

        /// <summary>
        /// <para>Gets a relative IRI that identifies the resource augmented by the link or <c>null</c> if the link doesn't specify any augmentation.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-refines" /> for more information.
        /// </para>
        /// </summary>
        public string? Refines { get; }

        /// <summary>
        /// <para>Gets a list of properties that establish the relationship the resource has with the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-link-rel" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-rel" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataLinkRelationship> Relationships { get; }
    }
}
