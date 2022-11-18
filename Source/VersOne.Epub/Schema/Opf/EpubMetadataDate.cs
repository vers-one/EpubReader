﻿using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>The date of the publication or some other event associated with the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcdate" />,
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
        /// <param name="event">
        /// The name of the event represented by this date (e.g., creation, publication, modification, etc.) or <c>null</c> if the event doesn't have a name.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="date"/> argument is <c>null</c>.</exception>
        public EpubMetadataDate(string date, string? @event)
        {
            Date = date ?? throw new ArgumentNullException(nameof(date));
            Event = @event;
        }

        /// <summary>
        /// <para>Gets the date of the event.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-opf-dcdate" />,
        /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" />,
        /// and <see href="http://purl.org/dc/elements/1.1/date" /> for more information.
        /// </para>
        /// </summary>
        public string Date { get; }

        /// <summary>
        /// <para>Gets the name of the event represented by this date (e.g., creation, publication, modification, etc.) or <c>null</c> if the event doesn't have a name.</para>
        /// <para>
        /// See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.7" /> for more information.
        /// </para>
        /// </summary>
        public string? Event { get; }
    }
}
