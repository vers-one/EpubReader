using System.Collections.Generic;

namespace VersOne.Epub
{
    /// <summary>
    /// Represents a single narration section (typically a chapter) in the book.
    /// </summary>
    public class EpubNarration
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubNarration" /> class.
        /// </summary>
        /// <param name="phrases">The list of the phrases that constitute this narration section.</param>
        public EpubNarration(List<EpubNarrationPhrase>? phrases = null)
        {
            Phrases = phrases ?? new List<EpubNarrationPhrase>();
        }

        /// <summary>
        /// Gets the list of the phrases that constitute this narration section.
        /// </summary>
        public List<EpubNarrationPhrase> Phrases { get; }
    }
}
