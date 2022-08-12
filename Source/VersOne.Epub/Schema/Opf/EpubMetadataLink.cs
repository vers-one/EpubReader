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
        /// <para>Gets the unique ID of this EPUB metadata identifier item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets the location of the linked resource.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-elem" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-href" /> for more information.
        /// </para>
        /// </summary>
        public string Href { get; internal set; }

        /// <summary>
        /// <para>Gets the media type of the linked resource.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-link-media-type" /> for more information.
        /// </para>
        /// </summary>
        public string MediaType { get; internal set; }

        /// <summary>
        /// <para>Gets a list of the link properties used to establish the type of record a referenced resource represents.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-properties" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-properties" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataLinkProperty> Properties { get; internal set; }

        /// <summary>
        /// <para>Gets a relative IRI that identifies the resource augmented by the link.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-refines" /> for more information.
        /// </para>
        /// </summary>
        public string Refines { get; internal set; }

        /// <summary>
        /// <para>Gets a list of properties that establish the relationship the resource has with the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-link-rel" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-rel" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubMetadataLinkRelationship> Relationships { get; internal set; }
    }
}
