using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Property of EPUB manifest item.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#attrdef-properties" />,
    /// <see href="https://www.w3.org/TR/epub-33/#sec-item-elem" />,
    /// and <see href="https://www.w3.org/TR/epub-33/#app-item-properties-vocab" /> for more information.
    /// </para>
    /// </summary>
    public enum EpubManifestProperty
    {
        /// <summary>
        /// <para>The 'cover-image' property. This property identifies the manifest item as the cover image of the book.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-cover-image" /> for more information.</para>
        /// </summary>
        COVER_IMAGE = 1,

        /// <summary>
        /// <para>The 'mathml' property. This property indicates that the manifest item contains one or more instances of MathML markup.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-mathml" /> for more information.</para>
        /// </summary>
        MATHML,

        /// <summary>
        /// <para>The 'nav' property. This property indicates that the manifest item constitutes the EPUB 3 Navigation Document.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-nav-prop" /> for more information.</para>
        /// </summary>
        NAV,

        /// <summary>
        /// <para>
        /// The 'remote-resources' property. This property indicates that the manifest item contains one or more internal references to other publication resources
        /// that are located outside of this EPUB file.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-remote-resources" /> for more information.</para>
        /// </summary>
        REMOTE_RESOURCES,

        /// <summary>
        /// <para>
        /// The 'scripted' property. This property indicates that the manifest item is a Scripted Content Document (i.e. contains scripted content and/or HTML form elements).
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-scripted" /> for more information.</para>
        /// </summary>
        SCRIPTED,

        /// <summary>
        /// <para>The 'svg' property. This property indicates that the manifest item embeds one or more instances of SVG markup.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#sec-svg" /> for more information.</para>
        /// </summary>
        SVG,

        /// <summary>
        /// A EPUB manifest item property which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubManifestPropertyParser
    {
        public static List<EpubManifestProperty> ParsePropertyList(string stringValue)
        {
            return stringValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                Select(propertyString => ParseProperty(propertyString.Trim())).
                ToList();
        }

        private static EpubManifestProperty ParseProperty(string stringValue)
        {
            return stringValue.ToLowerInvariant() switch
            {
                "cover-image" => EpubManifestProperty.COVER_IMAGE,
                "mathml" => EpubManifestProperty.MATHML,
                "nav" => EpubManifestProperty.NAV,
                "remote-resources" => EpubManifestProperty.REMOTE_RESOURCES,
                "scripted" => EpubManifestProperty.SCRIPTED,
                "svg" => EpubManifestProperty.SVG,
                _ => EpubManifestProperty.UNKNOWN
            };
        }
    }
}
