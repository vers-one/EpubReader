using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>An item within the <see cref="EpubManifest" />. Represents a single content item of the EPUB book.</para>
    /// <para>See <see href="https://www.w3.org/TR/epub/#sec-item-elem" /> for more information.</para>
    /// </summary>
    public class EpubManifestItem
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubManifestItem" /> class with specified ID, href, media type, and properties.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB manifest item.</param>
        /// <param name="href">The URI to the content item represented by this EPUB manifest item.</param>
        /// <param name="mediaType">The MIME media type of the content item represented by this EPUB manifest item.</param>
        /// <param name="properties">The list of properties of this EPUB manifest item or <c>null</c> if the manifest item doesn't declare properties.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="href" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="mediaType" /> parameter is <c>null</c>.</exception>
        public EpubManifestItem(string id, string href, string mediaType, List<EpubManifestProperty>? properties = null)
            : this(id, href, mediaType, null, null, null, null, null, properties)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubManifestItem" /> class with specified ID, href, media type, media overlay, required namespace,
        /// required modules, fallback, fallback style, and properties.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB manifest item.</param>
        /// <param name="href">The URI to the content item represented by this EPUB manifest item.</param>
        /// <param name="mediaType">The MIME media type of the content item represented by this EPUB manifest item.</param>
        /// <param name="mediaOverlay">
        /// The ID of the media overlay document for the content item represented by this EPUB manifest item
        /// or <c>null</c> if the content item doesn't have a media overlay document.
        /// </param>
        /// <param name="requiredNamespace">
        /// The namespace of the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </param>
        /// <param name="requiredModules">
        /// A comma-separated list containing the names of the extended modules used in the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </param>
        /// <param name="fallback">
        /// The ID of the fallback for the content item represented by this EPUB manifest item
        /// or <c>null</c> if the content item does not have a fallback.
        /// </param>
        /// <param name="fallbackStyle">
        /// The CSS stylesheet to render the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </param>
        /// <param name="properties">The list of properties of this EPUB manifest item or <c>null</c> if the manifest item doesn't declare properties.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="id" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="href" /> parameter is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="mediaType" /> parameter is <c>null</c>.</exception>
        public EpubManifestItem(string id, string href, string mediaType, string? mediaOverlay, string? requiredNamespace, string? requiredModules,
            string? fallback, string? fallbackStyle, List<EpubManifestProperty>? properties)
        {
            Id = id ?? throw new ArgumentNullException(nameof(id));
            Href = href ?? throw new ArgumentNullException(nameof(href));
            MediaType = mediaType ?? throw new ArgumentNullException(nameof(mediaType));
            MediaOverlay = mediaOverlay;
            RequiredNamespace = requiredNamespace;
            RequiredModules = requiredModules;
            Fallback = fallback;
            FallbackStyle = fallbackStyle;
            Properties = properties;
        }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB manifest item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#attrdef-id" />
        /// and <see href="https://www.w3.org/TR/epub/#sec-item-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Id { get; }

        /// <summary>
        /// <para>Gets the URI to the content item represented by this EPUB manifest item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#attrdef-href" />
        /// and <see href="https://www.w3.org/TR/epub/#sec-item-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// <para>Gets the MIME media type of the content item represented by this EPUB manifest item.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-item-media-type" /> for more information.</para>
        /// </summary>
        public string MediaType { get; }

        /// <summary>
        /// <para>
        /// Gets the ID of the media overlay document for the content item represented by this EPUB manifest item
        /// or <c>null</c> if the content item doesn't have a media overlay document.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-item-media-overlay" /> for more information.</para>
        /// </summary>
        public string? MediaOverlay { get; }

        /// <summary>
        /// <para>
        /// Gets the namespace of the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string? RequiredNamespace { get; }

        /// <summary>
        /// <para>
        /// Gets a comma-separated list containing the names of the extended modules used in the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string? RequiredModules { get; }

        /// <summary>
        /// <para>
        /// Gets the ID of the fallback for the content item represented by this EPUB manifest item
        /// or <c>null</c> if the content item does not have a fallback.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-item-fallback" /> for more information.</para>
        /// </summary>
        public string? Fallback { get; }

        /// <summary>
        /// <para>
        /// Gets the CSS stylesheet to render the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item
        /// or <c>null</c> if the manifest item does not represent a EPUB 2 Out-Of-Line XML Island.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string? FallbackStyle { get; }

        /// <summary>
        /// <para>Gets the list of properties of this EPUB manifest item or <c>null</c> if the manifest item doesn't declare properties.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#attrdef-properties" />,
        /// <see href="https://www.w3.org/TR/epub/#sec-item-elem" />,
        /// and <see href="https://www.w3.org/TR/epub/#app-item-properties-vocab" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubManifestProperty>? Properties { get; }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Id" />, <see cref="Href" />, and <see cref="MediaType" /> properties for debugging purposes.
        /// </summary>
        /// <returns>A string containing the values of the <see cref="Id" />, <see cref="Href" />, and <see cref="MediaType" /> properties.</returns>
        public override string ToString()
        {
            return $"Id: {Id}, Href = {Href}, MediaType = {MediaType}";
        }
    }
}
