using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Property of <see cref="EpubSpineItemRef" />.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-itemref-property-values" /> for more information.</para>
    /// </summary>
    public enum EpubSpineProperty
    {
        /// <summary>
        /// <para>The 'page-spread-left' property. Indicates that the first page of the associated 'item' element represents the left-hand side of a two-page spread.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-page-spread-left" /> for more information.</para>
        /// </summary>
        PAGE_SPREAD_LEFT = 1,

        /// <summary>
        /// <para>The 'page-spread-right' property. Indicates that the first page of the associated 'item' element represents the right-hand side of a two-page spread.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-page-spread-right" /> for more information.</para>
        /// </summary>
        PAGE_SPREAD_RIGHT,

        /// <summary>
        /// A spine property which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubSpinePropertyParser
    {
        public static List<EpubSpineProperty> ParsePropertyList(string stringValue)
        {
            return stringValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                Select(propertyString => ParseProperty(propertyString.Trim())).
                ToList();
        }

        public static EpubSpineProperty ParseProperty(string stringValue)
        {
            return stringValue.ToLowerInvariant() switch
            {
                "page-spread-left" => EpubSpineProperty.PAGE_SPREAD_LEFT,
                "page-spread-right" => EpubSpineProperty.PAGE_SPREAD_RIGHT,
                _ => EpubSpineProperty.UNKNOWN,
            };
        }
    }
}
