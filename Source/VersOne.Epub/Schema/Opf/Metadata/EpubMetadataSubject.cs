using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Represents a subject of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcsubject" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.3" />,
    /// and <see href="http://purl.org/dc/elements/1.1/subject" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataSubject
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataSubject" /> class.
        /// </summary>
        /// <param name="subject">The text content of this subject.</param>
        /// <param name="id">The unique ID of this subject or <c>null</c> if the subject doesn't have an ID.</param>
        /// <param name="textDirection">The text direction of this subject or <c>null</c> if the subject doesn't specify a text direction.</param>
        /// <param name="language">The language of this subject or <c>null</c> if the subject doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="subject" /> parameter is <c>null</c>.</exception>
        public EpubMetadataSubject(string subject, string? id = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Subject = subject ?? throw new ArgumentNullException(nameof(subject));
            Id = id;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the text content of this subject.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dcsubject" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.3" />,
        /// and <see href="http://purl.org/dc/elements/1.1/subject" /> for more information.
        /// </para>
        /// </summary>
        public string Subject { get; }

        /// <summary>
        /// <para>Gets the unique ID of this subject or <c>null</c> if the subject doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the text direction of this subject or <c>null</c> if the subject doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language of this subject or <c>null</c> if the subject doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
