namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Reference element of the <see cref="EpubGuide" />.</para>
    /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
    /// </summary>
    public class EpubGuideReference
    {
        /// <summary>
        /// <para>Gets the type of the publication component referenced by the <see cref="Href" /> property.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string Type { get; internal set; }

        /// <summary>
        /// <para>Gets the title of the reference.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string Title { get; internal set; }

        /// <summary>
        /// <para>Gets the link to a content document included in the manifest, with an optional fragment identifier.</para>
        /// <para>See <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.6" /> for more information.</para>
        /// </summary>
        public string Href { get; internal set; }

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
