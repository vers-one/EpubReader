using System;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Reference element of the <see cref="EpubGuide" />.</para>
    /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
    /// </summary>
    public class EpubGuideReference
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="EpubGuideReference" /> class.
        /// </summary>
        /// <param name="type">The type of the publication component referenced by the <see cref="Href" /> property.</param>
        /// <param name="title">The title of the reference or <c>null</c> if the reference doesn't have a title.</param>
        /// <param name="href">The link to a content document included in the manifest, with an optional fragment identifier.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="type"/> argument is <c>null</c>.</exception>
        /// <exception cref="ArgumentNullException">The <paramref name="href"/> argument is <c>null</c>.</exception>
        public EpubGuideReference(string type, string? title, string href)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Title = title;
            Href = href ?? throw new ArgumentNullException(nameof(href));
        }

        /// <summary>
        /// <para>Gets the type of the publication component referenced by the <see cref="Href" /> property.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string Type { get; }

        /// <summary>
        /// <para>Gets the title of the reference or <c>null</c> if the reference doesn't have a title.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string? Title { get; }

        /// <summary>
        /// <para>Gets the link to a content document included in the manifest, with an optional fragment identifier.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string Href { get; }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Type" /> and <see cref="Href" /> properties for debugging purposes.
        /// </summary>
        /// <returns>A string containing the values of the <see cref="Type" /> and <see cref="Href" /> properties.</returns>
        public override string ToString()
        {
            return $"Type: {Type}, Href: {Href}";
        }
    }
}
