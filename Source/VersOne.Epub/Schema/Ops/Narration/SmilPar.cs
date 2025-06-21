using System;
using System.Collections.Generic;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// Parsed content of a SMIL phrase which identifies a text and audio component to synchronize during playback.
    /// This class corresponds to the &lt;par&gt; element in a media overlay document.
    /// </para>
    /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-smil-par-elem" /> for more information.</para>
    /// </summary>
    public class SmilPar
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="SmilPar" /> class.
        /// </summary>
        /// <param name="id">An optional identifier of this element.</param>
        /// <param name="epubTypes">An optional list of the structural semantics of the corresponding element in the EPUB content document.</param>
        /// <param name="text">The reference to an element (typically, a textual element) in the EPUB content document.</param>
        /// <param name="audio">An optional reference to a clip of audio media.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="text" /> parameter is <c>null</c>.</exception>
        public SmilPar(string? id, List<Epub3StructuralSemanticsProperty>? epubTypes, SmilText text, SmilAudio? audio)
        {
            Id = id;
            EpubTypes = epubTypes;
            Text = text ?? throw new ArgumentNullException(nameof(text));
            Audio = audio;
        }

        /// <summary>
        /// Gets an optional identifier of this element. If present, this value is unique within the scope of the media overlay document.
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// Gets an optional list of the structural semantics of the corresponding element in the EPUB content document.
        /// </summary>
        public List<Epub3StructuralSemanticsProperty>? EpubTypes { get; }

        /// <summary>
        /// Gets the reference to an element (typically, a textual element) in the EPUB content document.
        /// </summary>
        public SmilText Text { get; }

        /// <summary>
        /// Gets an optional reference to a clip of audio media.
        /// </summary>
        public SmilAudio? Audio { get; }
    }
}
