namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the media overlay head which acts as a container for metadata elements.
    /// This class corresponds to the &lt;head&gt; element in a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-mediaoverlays.html#sec-smil-head-elem" /> for more information.</para>
    /// </summary>
    public class SmilHead
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilHead" /> class.
        /// </summary>
        /// <param name="metadata">Optional metadata of the media overlay document.</param>
        public SmilHead(SmilMetadata? metadata)
        {
            Metadata = metadata;
        }

        /// <summary>
        /// Gets optional metadata of the media overlay document.
        /// </summary>
        public SmilMetadata? Metadata { get; }
    }
}
