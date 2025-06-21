using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Property of <see cref="EpubMetadataLink" />.</para>
    /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-link-properties" /> for more information.</para>
    /// </summary>
    public enum EpubMetadataLinkProperty
    {
        /// <summary>
        /// <para>The 'onix' property. Indicates that the referenced resource is an ONIX record.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-onix" /> for more information.</para>
        /// </summary>
        ONIX = 1,

        /// <summary>
        /// A EPUB metadata link property which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubMetadataLinkPropertyParser
    {
        public static List<EpubMetadataLinkProperty> ParsePropertyList(string stringValue)
        {
            return stringValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                Select(propertyString => ParseProperty(propertyString.Trim())).
                ToList();
        }

        public static EpubMetadataLinkProperty ParseProperty(string stringValue)
        {
            return stringValue.ToLowerInvariant() switch
            {
                "onix" => EpubMetadataLinkProperty.ONIX,
                _ => EpubMetadataLinkProperty.UNKNOWN
            };
        }
    }
}
