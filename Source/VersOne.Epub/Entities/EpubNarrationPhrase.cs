using System;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a single phrase within a <see cref="EpubNarration" />.
    /// </summary>
    public class EpubNarrationPhrase
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNarrationPhrase" /> class.
        /// </summary>
        /// <param name="textContentFile">The text content file containing the phrase that corresponds to the narration in the audio clip.</param>
        /// <param name="textContentAnchor">
        /// An HTML anchor of the phrase that corresponds to the narration in the audio clip or <c>null</c> if the audio clip contains the narration for the whole text content file.
        /// </param>
        /// <param name="audioContentFile">
        /// An audio content file containing the narration for this phrase or <c>null</c> if the phrase is intended to be narrated via a Text-to-Speech (TTS) system.
        /// </param>
        /// <param name="audioContentBegin">
        /// The timestamp that represents the beginning of the audio clip in the <paramref name="audioContentFile" />
        /// or <c>null</c> if the audio content file needs to be played from the beginning of the file.</param>
        /// <param name="audioContentEnd">
        /// The timestamp that represents the end of the audio clip in the <paramref name="audioContentFile" />
        /// or <c>null</c> if the audio content file needs to be played until the end of the file.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="textContentFile" /> parameter is <c>null</c>.</exception>
        public EpubNarrationPhrase(EpubLocalTextContentFile textContentFile, string? textContentAnchor,
            EpubContentFile? audioContentFile, EpubNarrationTimestamp? audioContentBegin, EpubNarrationTimestamp? audioContentEnd)
        {
            TextContentFile = textContentFile ?? throw new ArgumentNullException(nameof(textContentFile));
            TextContentAnchor = textContentAnchor;
            AudioContentFile = audioContentFile;
            AudioContentBegin = audioContentBegin;
            AudioContentEnd = audioContentEnd;
        }

        /// <summary>
        /// Gets the text content file containing the phrase that corresponds to the narration in the audio clip.
        /// </summary>
        public EpubLocalTextContentFile TextContentFile { get; }

        /// <summary>
        /// Gets an HTML anchor of the phrase that corresponds to the narration in the audio clip or <c>null</c> if the audio clip contains the narration for the whole text content file.
        /// </summary>
        public string? TextContentAnchor { get; }

        /// <summary>
        /// Gets an audio content file containing the narration for this phrase or <c>null</c> if the phrase is intended to be narrated via a Text-to-Speech (TTS) system.
        /// </summary>
        public EpubContentFile? AudioContentFile { get; }

        /// <summary>
        /// Gets the timestamp that represents the beginning of the audio clip in the <see cref="AudioContentFile" />
        /// or <c>null</c> if the audio content file needs to be played from the beginning of the file.
        /// </summary>
        public EpubNarrationTimestamp? AudioContentBegin { get; }

        /// <summary>
        /// Gets the timestamp that represents the end of the audio clip in the <see cref="AudioContentFile" />
        /// or <c>null</c> if the audio content file needs to be played until the end of the file.
        /// </summary>
        public EpubNarrationTimestamp? AudioContentEnd { get; }
    }
}
