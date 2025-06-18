using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the reference to an element (typically, a textual element) in the EPUB content document.
    /// This class corresponds to the &lt;text&gt; element in a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub/#sec-smil-text-elem" /> for more information.</para>
    /// </summary>
    public class SmilText
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilText" /> class.
        /// </summary>
        /// <param name="id">An optional identifier of this element.</param>
        /// <param name="src">The relative IRI reference of the corresponding EPUB content document, including a fragment identifier that references the specific element.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="src" /> parameter is <c>null</c>.</exception>
        public SmilText(string? id, string src)
        {
            Id = id;
            Src = src ?? throw new ArgumentNullException(nameof(src));
        }

        /// <summary>
        /// Gets an optional identifier of this element. If present, this value is unique within the scope of the media overlay document.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets the relative IRI reference of the corresponding EPUB content document, including a fragment identifier that references the specific element.
        /// </summary>
        public string Src { get; }
    }
}
