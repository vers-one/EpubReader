using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>The date of the publication or some other event associated with the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcdate" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
    /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataDate
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataDate" /> class.
        /// </summary>
        /// <param name="date">The date of the event.</param>
        /// <param name="id">The unique ID of this EPUB metadata date item.</param>
        /// <param name="event">
        /// The name of the event represented by this date (e.g., creation, publication, modification, etc.) or <c>null</c> if the event doesn't have a name.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="date" /> parameter is <c>null</c>.</exception>
        public EpubMetadataDate(string date, string? id = null, string? @event = null)
        {
            Date = date ?? throw new ArgumentNullException(nameof(date));
            Id = id;
            Event = @event;
        }

        /// <summary>
        /// <para>Gets the date of the event.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#sec-opf-dcdate" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
        /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
        /// </para>
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata date item.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the name of the event represented by this date (e.g., creation, publication, modification, etc.) or <c>null</c> if the event doesn't have a name.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" /> for more information.
        /// </para>
        /// </summary>
        public string? Event { get; }
    }
}
