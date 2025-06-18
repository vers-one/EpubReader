﻿using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Creator of the book. Represents the name of a person, organization, etc. responsible for the creation of the content of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub/#sec-opf-dccreator" />,
    /// <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.2.2" />,
    /// and <see href="http://purl.org/dc/elements/1.1/creator" /> for more information.
    /// </para>
    /// </summary>
    public class EpubMetadataCreator
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubMetadataCreator" /> class.
        /// </summary>
        /// <param name="creator">The name of the creator as the author intends it to be displayed to a user.</param>
        /// <param name="id">The unique ID of this EPUB metadata creator item.</param>
        /// <param name="fileAs">The normalized form of the name of the creator for sorting.</param>
        /// <param name="role">The creator's role which indicates the function the creator played in the creation of the content of the EPUB book.</param>
        /// <param name="textDirection">The text direction for the name of this creator or <c>null</c> if the creator doesn't specify a text direction.</param>
        /// <param name="language">The language for the name of this creator or <c>null</c> if the creator doesn't specify the language.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="creator" /> parameter is <c>null</c>.</exception>
        public EpubMetadataCreator(string creator, string? id = null, string? fileAs = null, string? role = null, EpubTextDirection? textDirection = null, string? language = null)
        {
            Creator = creator ?? throw new ArgumentNullException(nameof(creator));
            Id = id;
            FileAs = fileAs;
            Role = role;
            TextDirection = textDirection;
            Language = language;
        }

        /// <summary>
        /// <para>Gets the name of the creator as the author intends it to be displayed to a user.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#sec-opf-dccreator" /> for more information.</para>
        /// </summary>
        public string Creator { get; }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB metadata creator item.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>Gets the normalized form of the name of the creator for sorting.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#file-as" />
        /// and <see href="https://www.w3.org/TR/epub/#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string? FileAs { get; }

        /// <summary>
        /// <para>Gets the creator's role which indicates the function the creator played in the creation of the content of the EPUB book.</para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub/#role" />
        /// and <see href="https://www.w3.org/TR/epub/#sec-opf-dccreator" /> for more information.
        /// </para>
        /// </summary>
        public string? Role { get; }

        /// <summary>
        /// <para>Gets the text direction for the name of this creator or <c>null</c> if the creator doesn't specify a text direction.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-dir" /> for more information.</para>
        /// </summary>
        public EpubTextDirection? TextDirection { get; }

        /// <summary>
        /// <para>Gets the language for the name of this creator or <c>null</c> if the creator doesn't specify the language.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub/#attrdef-xml-lang" /> for more information.</para>
        /// </summary>
        public string? Language { get; }
    }
}
