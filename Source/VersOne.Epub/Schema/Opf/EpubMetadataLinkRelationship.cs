using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Property that describes the relationship between the resource referenced by <see cref="EpubMetadataLink" /> and the EPUB book.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-link-rel" /> for more information.</para>
    /// </summary>
    public enum EpubMetadataLinkRelationship
    {
        /// <summary>
        /// <para>The 'acquire' property. Used in EPUB previews to identify where the full version of the EPUB book can be acquired.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-acquire" /> for more information.</para>
        /// </summary>
        ACQUIRE = 1,

        /// <summary>
        /// <para>The 'alternate' property. Identifies an alternate representation of the EPUB book or a collection.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-alternate" /> for more information.</para>
        /// </summary>
        ALTERNATE,

        /// <summary>
        /// <para>The 'marc21xml-record' property. Indicates that the referenced resource is a MARC21 record.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-marc21xml-record" />
        /// and <see href="https://idpf.org/epub/30/spec/epub30-publications.html#marc21xml-record" /> for more information.
        /// </para>
        /// </summary>
        MARC21XML_RECORD,

        /// <summary>
        /// <para>The 'mods-record' property. Indicates that the referenced resource is a MODS record.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-mods-record" />
        /// and <see href="https://idpf.org/epub/30/spec/epub30-publications.html#mods-record" /> for more information.
        /// </para>
        /// </summary>
        MODS_RECORD,

        /// <summary>
        /// <para>The 'onix-record' property. Indicates that the referenced resource is an ONIX record.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-onix-record" />
        /// and <see href="https://idpf.org/epub/30/spec/epub30-publications.html#onix-record" /> for more information.
        /// </para>
        /// </summary>
        ONIX_RECORD,

        /// <summary>
        /// <para>The 'record' property. Indicates that the referenced resource is a metadata record.</para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-record" /> for more information.</para>
        /// </summary>
        RECORD,

        /// <summary>
        /// <para>
        /// The 'voicing' property. Indicates that the referenced audio file provides an aural representation of the expression or resource (typically, the title or creator)
        /// specified by the <see cref="EpubMetadataLink.Refines" /> property.
        /// </para>
        /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-voicing" /> for more information.</para>
        /// </summary>
        VOICING,

        /// <summary>
        /// <para>The 'xml-signature' property. Indicates that the referenced resource contains an XML Signature for the EPUB book or associated property.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-xml-signature" />
        /// and <see href="https://idpf.org/epub/30/spec/epub30-publications.html#xml-signature" /> for more information.
        /// </para>
        /// </summary>
        XML_SIGNATURE,

        /// <summary>
        /// <para>The 'xmp-record' property. Indicates that the referenced resource is an XMP record.</para>
        /// <para>
        /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#sec-xmp-record" />
        /// and <see href="https://idpf.org/epub/30/spec/epub30-publications.html#xmp-record" /> for more information.
        /// </para>
        /// </summary>
        XMP_RECORD,

        /// <summary>
        /// A relationship which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubMetadataLinkRelationshipParser
    {
        public static List<EpubMetadataLinkRelationship> ParseRelationshipList(string stringValue)
        {
            return stringValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries).
                Select(relationshipString => ParseRelationship(relationshipString.Trim())).
                ToList();
        }

        public static EpubMetadataLinkRelationship ParseRelationship(string stringValue)
        {
            switch (stringValue.ToLowerInvariant())
            {
                case "acquire":
                    return EpubMetadataLinkRelationship.ACQUIRE;
                case "alternate":
                    return EpubMetadataLinkRelationship.ALTERNATE;
                case "marc21xml-record":
                    return EpubMetadataLinkRelationship.MARC21XML_RECORD;
                case "mods-record":
                    return EpubMetadataLinkRelationship.MODS_RECORD;
                case "onix-record":
                    return EpubMetadataLinkRelationship.ONIX_RECORD;
                case "record":
                    return EpubMetadataLinkRelationship.RECORD;
                case "voicing":
                    return EpubMetadataLinkRelationship.VOICING;
                case "xml-signature":
                    return EpubMetadataLinkRelationship.XML_SIGNATURE;
                case "xmp-record":
                    return EpubMetadataLinkRelationship.XMP_RECORD;
                default:
                    return EpubMetadataLinkRelationship.UNKNOWN;
            }
        }
    }
}
