using System;
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
        /// Initializes a new instance of the <see cref="EpubSpineItemRef" /> class with the specified ID ref.
        /// </summary>
        /// <param name="idRef">
        /// The value of the <see cref="EpubManifestItem.Id" /> property of an item declared in the <see cref="EpubManifest" /> this EPUB spine element is referencing to.
        /// </param>
        /// <exception cref="ArgumentNullException">The <paramref name="idRef" /> parameter is <c>null</c>.</exception>
        public EpubSpineItemRef(string idRef)
            : this(null, idRef, true, null)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="EpubSpineItemRef" /> class with specified ID, ID ref, linear indicator, and properties.
        /// </summary>
        /// <param name="id">The unique ID of this EPUB spine element or <c>null</c> if the spine doesn't have an ID.</param>
        /// <param name="idRef">
        /// The value of the <see cref="EpubManifestItem.Id" /> property of an item declared in the <see cref="EpubManifest" /> this EPUB spine element is referencing to.
        /// </param>
        /// <param name="isLinear">
        /// A value indicating whether the referenced <see cref="EpubManifestItem" /> contains content that contributes to the primary reading order
        /// and has to be read sequentially (<c>true</c>) or auxiliary content that enhances or augments the primary content and can be accessed out of sequence (<c>false</c>).
        /// </param>
        /// <param name="properties">A list of additional EPUB spine element properties or <c>null</c> if the spine doesn't specify properties.</param>
        /// <exception cref="ArgumentNullException">The <paramref name="idRef" /> parameter is <c>null</c>.</exception>
        public EpubSpineItemRef(string? id, string idRef, bool isLinear, List<EpubSpineProperty>? properties = null)
        {
            Id = id;
            IdRef = idRef ?? throw new ArgumentNullException(nameof(idRef));
            IsLinear = isLinear;
            Properties = properties;
        }

        /// <summary>
        /// <para>Gets the unique ID of this EPUB spine element or <c>null</c> if the spine doesn't have an ID.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-id" /> for more information.</para>
        /// </summary>
        public string? Id { get; }

        /// <summary>
        /// <para>
        /// Gets the value of the <see cref="EpubManifestItem.Id" /> property of an item declared in the <see cref="EpubManifest" /> this EPUB spine element is referencing to.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-itemref-idref" />
        /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section2.4" /> for more information.
        /// </para>
        /// </summary>
        public string IdRef { get; }

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
        public bool IsLinear { get; }

        /// <summary>
        /// <para>Gets a list of additional EPUB spine element properties or <c>null</c> if the spine doesn't specify properties.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-itemref-property-values" /> for more information.</para>
        /// </summary>
        public List<EpubSpineProperty>? Properties { get; }

        /// <summary>
        /// Returns a string containing the values of the <see cref="Id" /> and <see cref="IdRef" /> properties for debugging purposes.
        /// </summary>
        /// <returns>A string containing the values of the <see cref="Id" /> and <see cref="IdRef" /> properties.</returns>
        public override string ToString()
        {
            StringBuilder resultBuilder = new();
            if (Id != null)
            {
                resultBuilder.Append("Id: ");
                resultBuilder.Append(Id);
                resultBuilder.Append("; ");
            }
            resultBuilder.Append("IdRef: ");
            resultBuilder.Append(IdRef);
            return resultBuilder.ToString();
        }
    }
}
