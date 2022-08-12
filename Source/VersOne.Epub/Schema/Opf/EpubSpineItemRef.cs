using System.Collections.Generic;
using System.Text;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>
    /// An element of the <see cref="EpubSpine" /> list which references a <see cref="EpubManifestItem" /> declared in the <see cref="EpubManifest" />.
    /// The order of the <see cref="EpubSpineItemRef" /> items in the <see cref="EpubSpine" /> list defines the default reading order of the EPUB book.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-itemref-elem" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
    /// </para>
    /// </summary>
    public class EpubSpineItemRef
    {
        /// <summary>
        /// <para>Gets the unique ID of this EPUB spine element.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string Id { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets the value of the <see cref="EpubManifestItem.Id" /> property of an item declared in the <see cref="EpubManifest" /> this EPUB spine element is referencing to.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-itemref-idref" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public string IdRef { get; internal set; }

        /// <summary>
        /// <para>
        /// Gets a value indicating whether the referenced <see cref="EpubManifestItem" /> contains content that contributes to the primary reading order
        /// and has to be read sequentially (<c>true</c>) or auxiliary content that enhances or augments the primary content and can be accessed out of sequence (<c>false</c>).
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-itemref-linear" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public bool IsLinear { get; internal set; }

        /// <summary>
        /// <para>Gets a list of additional EPUB spine element properties.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-itemref-property-values" /> for more information.</para>
        /// </summary>
        public List<EpubSpineProperty> Properties { get; internal set; }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Id" /> and <see cref="IdRef" /> properties for debugging purposes.
        /// </summary>
        /// <returns>A string containing the values of the <see cref="Id" /> and <see cref="IdRef" /> properties.</returns>
        public override string ToString()
        {
            StringBuilder resultBuilder = new StringBuilder();
            if (Id != null)
            {
                resultBuilder.Append("Id: ");
                resultBuilder.Append(Id);
                resultBuilder.Append("; ");
            }
            resultBuilder.Append("IdRef: ");
            resultBuilder.Append(IdRef ?? "(null)");
            return resultBuilder.ToString();
        }
    }
}
