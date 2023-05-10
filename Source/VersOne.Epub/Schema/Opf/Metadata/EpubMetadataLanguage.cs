using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a language of the content of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dclanguage" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.12" />,
    /// and <see href="http://purl.org/dc/elements/1.1/language" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataLanguage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataLanguage" /> class.
        /// </summary>
        /// <param name="language">The language tag.</param>
        /// <param name="id">The unique ID of this language metadata element or <c>null</c> if the language metadata element doesn't have an ID.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="language" /> parameter is <c>null</c>.</exception>
        public EpubMetadataLanguage(string language, string? id = null)
        {
            Language = language ?? throw new ArgumentNullException(nameof(language));
            Id = id;
        }

        /// <summary>
        /// <para>Gets the language tag.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dclanguage" />
        /// and <see href="https://www.rfc-editor.org/info/bcp47" /> for more information.
        /// </para>
        /// </summary>
        public string Language { get; }

        /// <summary>
        /// <para>Gets the unique ID of this language metadata element or <c>null</c> if the language metadata element doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }
    }
}
