using System;
using System.Collections.Generic;
using System.Xml.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// The metadata of the media overlay document. This is an extension point that allows the inclusion of metadata from any metainformation structuring language.
    /// This class corresponds to the &lt;metadata&gt; element in a media overlay document.
    /// The content of this XML element is not parsed because EPUB 3 SMIL standard doesn't put any restrictions on its structure.
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-mediaoverlays.html#sec-smil-metadata-elem" /> for more information.</para>
    /// </summary>
    public class SmilMetadata
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilMetadata" /> class.
        /// </summary>
        /// <param name="items">The list of XML elements within the &lt;metadata&gt; element in the media overlay document.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="items" /> parameter is <c>null</c>.</exception>
        public SmilMetadata(List<XElement> items)
        {
            Items = items ?? throw new ArgumentNullException(nameof(items));
        }

        /// <summary>
        /// Gets the list of XML elements within the &lt;metadata&gt; element in the media overlay document.
        /// The content of these XML elements is not parsed because EPUB 3 SMIL standard doesn't put any restrictions on their structure.
        /// </summary>
        public List<XElement> Items { get; }
    }
}
