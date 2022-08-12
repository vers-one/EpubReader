using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>An item within the <see cref="EpubManifest" />. Represents a single content item of the EPUB book.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-item-elem" /> for more information.</para>
    /// </summary>
    public class EpubManifestItem
    {
        /// <summary>
        /// <para>Gets the unique ID of this EPUB manifest item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-item-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>Gets the URI to the content item represented by this EPUB manifest item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-href" />
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-item-elem" /> for more information.
        /// </para>
        /// </summary>
        public string Href { get; internal set; }

        /// <summary>
        /// <para>Gets the MIME media type of the content item represented by this EPUB manifest item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-item-media-type" /> for more information.</para>
        /// </summary>
        public string MediaType { get; internal set; }

        /// <summary>
        /// <para>Gets the ID of the media overlay document for the content item represented by this EPUB manifest item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-item-media-overlay" /> for more information.</para>
        /// </summary>
        public string MediaOverlay { get; internal set; }

        /// <summary>
        /// <para>Gets the namespace of the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string RequiredNamespace { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the a comma-separated list containing the names of the extended modules used in the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item.
        /// </para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string RequiredModules { get; internal set; }

        /// <summary>
        /// <para>Gets the ID of the fallback for the content item represented by this EPUB manifest item.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-item-fallback" /> for more information.</para>
        /// </summary>
        public string Fallback { get; internal set; }

        /// <summary>
        /// <para>Gets the CSS stylesheet to render the EPUB 2 Out-Of-Line XML Island represented by this EPUB manifest item..</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.3.1.2" /> for more information.</para>
        /// </summary>
        public string FallbackStyle { get; internal set; }

        /// <summary>
        /// <para>Gets the list of properties of this EPUB manifest item.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-properties" />,
        /// <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-item-elem" />,
        /// and <see href="https://www.w3.org/publishing/epub32/epub-packages.html#app-item-properties-vocab" /> for more information.
        /// </para>
        /// </summary>
        public List<EpubManifestProperty> Properties { get; internal set; }

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
