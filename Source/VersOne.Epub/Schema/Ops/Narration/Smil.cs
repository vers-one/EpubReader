using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of a EPUB 3 media overlay document.
    /// This class corresponds to the &lt;smil&gt; element which is the root element for a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub/#sec-smil-smil-elem" /> for more information.</para>
    /// </summary>
    public class Smil
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Smil" /> class.
        /// </summary>
        /// <param name="id">An optional identifier of this element.</param>
        /// <param name="version">The version number of the SMIL specification to which this media overlay adheres. Currently, it must always be <see cref="SmilVersion.SMIL_3" />.</param>
        /// <param name="epubPrefix">Additional metadata vocabulary prefixes or <c>null</c> if there are no prefixes specified.</param>
        /// <param name="head">An optional media overlay head which acts as a container for metadata elements or <c>null</c> if the document doesn't specify any metadata.</param>
        /// <param name="body">The media overlay body which is the starting point for the presentation contained in the media overlay document.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="body" /> parameter is <c>null</c>.</exception>
        public Smil(string? id, SmilVersion version, string? epubPrefix, SmilHead? head, SmilBody body)
        {
            Id = id;
            Version = version;
            EpubPrefix = epubPrefix;
            Head = head;
            Body = body ?? throw new ArgumentNullException(nameof(body));
        }

        /// <summary>
        /// Gets an optional identifier of this element. If present, this value is unique within the scope of the media overlay document.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets the version number of the SMIL specification to which this media overlay adheres. Currently, it is always <see cref="SmilVersion.SMIL_3" />.
        /// </summary>
        public SmilVersion Version { get; }

        /// <summary>
        /// Gets additional metadata vocabulary prefixes or <c>null</c> if there are no prefixes specified.
        /// </summary>
        public string? EpubPrefix { get; }

        /// <summary>
        /// Gets an optional media overlay head which acts as a container for metadata elements or <c>null</c> if the document doesn't specify any metadata.
        /// </summary>
        public SmilHead? Head { get; }

        /// <summary>
        /// Gets the media overlay body which is the starting point for the presentation contained in the media overlay document.
        /// </summary>
        public SmilBody Body { get; }
    }
}
