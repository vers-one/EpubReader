using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of the reference to a clip of audio media.
    /// This class corresponds to the &lt;audio&gt; element in a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-mediaoverlays.html#sec-smil-audio-elem" /> for more information.</para>
    /// </summary>
    public class SmilAudio
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilAudio" /> class.
        /// </summary>
        /// <param name="id">An optional identifier of this element.</param>
        /// <param name="src">The relative or the absolute IRI of an audio file.</param>
        /// <param name="clipBegin">
        /// An optional clock value (in the SMIL clock format) that specifies the offset into the physical media corresponding to the start point of an audio clip.
        /// </param>
        /// <param name="clipEnd">
        /// An optional clock value (in the SMIL clock format) that specifies the offset into the physical media corresponding to the end point of an audio clip.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="src" /> parameter is <c>null</c>.</exception>
        public SmilAudio(string? id, string src, string? clipBegin, string? clipEnd)
        {
            Id = id;
            Src = src ?? throw new ArgumentNullException(nameof(src));
            ClipBegin = clipBegin;
            ClipEnd = clipEnd;
        }

        /// <summary>
        /// Gets an optional identifier of this element. If present, this value is unique within the scope of the media overlay document.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets the relative or the absolute IRI of an audio file.
        /// </summary>
        public string Src { get; }

        /// <summary>
        /// Gets an optional clock value (in the SMIL clock format) that specifies the offset into the physical media corresponding to the start point of an audio clip.
        /// </summary>
        public string? ClipBegin { get; }

        /// <summary>
        /// Gets an optional clock value (in the SMIL clock format) that specifies the offset into the physical media corresponding to the end point of an audio clip.
        /// </summary>
        public string? ClipEnd { get; }
    }
}
