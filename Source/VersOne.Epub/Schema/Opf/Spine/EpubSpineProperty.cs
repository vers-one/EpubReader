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
        /// <para>The 'rendition:layout-pre-paginated' property. Specifies that the given spine item is pre-paginated.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#layout-pre-paginated" /> for more information.</para>
        /// </summary>
        LAYOUT_PRE_PAGINATED = 1,

        /// <summary>
        /// <para>The 'rendition:layout-reflowable' property. Specifies that the given spine item is reflowable.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#layout-reflowable" /> for more information.</para>
        /// </summary>
        LAYOUT_REFLOWABLE,

        /// <summary>
        /// <para>The 'rendition:orientation-auto' property. Specifies that the reading application determines the orientation to render the spine item in.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#orientation-auto" /> for more information.</para>
        /// </summary>
        ORIENTATION_AUTO,

        /// <summary>
        /// <para>The 'rendition:orientation-landscape' property. Specifies that the reading application should render the given spine item in landscape orientation.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#orientation-landscape" /> for more information.</para>
        /// </summary>
        ORIENTATION_LANDSCAPE,

        /// <summary>
        /// <para>The 'rendition:orientation-portrait' property. Specifies that the reading application should render the given spine item in portrait orientation.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#orientation-portrait" /> for more information.</para>
        /// </summary>
        ORIENTATION_PORTRAIT,

        /// <summary>
        /// <para>The 'rendition:spread-auto' property. Specifies that the reading application determines when to render a synthetic spread for the spine item.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#spread-auto" /> for more information.</para>
        /// </summary>
        SPREAD_AUTO,

        /// <summary>
        /// <para>
        /// The 'rendition:spread-both' property. Specifies that the reading application should render a synthetic spread for the spine item in both portrait and landscape orientations.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#spread-both" /> for more information.</para>
        /// </summary>
        SPREAD_BOTH,

        /// <summary>
        /// <para>
        /// The 'rendition:spread-landscape' property. Specifies that the reading application should render a synthetic spread for the spine item only when in landscape orientation.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#spread-landscape" /> for more information.</para>
        /// </summary>
        SPREAD_LANDSCAPE,

        /// <summary>
        /// <para>The 'rendition:spread-none' property. Specifies that the reading application should not render a synthetic spread for the spine item.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#spread-none" /> for more information.</para>
        /// </summary>
        SPREAD_NONE,

        /// <summary>
        /// <para>The 'rendition:page-spread-center' property. Indicates that the spine item should be centered.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#page-spread-center" /> for more information.</para>
        /// </summary>
        PAGE_SPREAD_CENTER,

        /// <summary>
        /// <para>
        /// The 'rendition:page-spread-left' or the 'page-spread-left' property.
        /// Indicates that the first page of the associated spine item represents the left-hand side of a two-page spread.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#fxl-page-spread-left" />
        /// or <see href="https://www.w3.org/TR/epub-33/#page-spread-left" /> for more information.
        /// </para>
        /// </summary>
        PAGE_SPREAD_LEFT,

        /// <summary>
        /// <para>
        /// The 'rendition:page-spread-right' or the 'page-spread-right' property.
        /// Indicates that the first page of the associated spine item represents the right-hand side of a two-page spread.
        /// </para>
        /// <para>
        /// See <see href="https://www.w3.org/TR/epub-33/#fxl-page-spread-right" />
        /// or <see href="https://www.w3.org/TR/epub-33/#page-spread-right" /> for more information.
        /// </para>
        /// </summary>
        PAGE_SPREAD_RIGHT,

        /// <summary>
        /// <para>The 'rendition:flow-paginated' property. Indicates that the EPUB creator preference is to dynamically paginate content overflow.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#flow-paginated" /> for more information.</para>
        /// </summary>
        FLOW_PAGINATED,

        /// <summary>
        /// <para>
        /// The 'rendition:flow-scrolled-continuous' property.
        /// Indicates that the EPUB creator preference is to provide a scrolled view for overflow content,
        /// and that consecutive spine items with this property are to be rendered as a continuous scroll.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#flow-scrolled-continuous" /> for more information.</para>
        /// </summary>
        FLOW_SCROLLED_CONTINUOUS,

        /// <summary>
        /// <para>
        /// The 'rendition:flow-scrolled-doc' property.
        /// Indicates that the EPUB creator preference is to provide a scrolled view for overflow content,
        /// and each spine item with this property is to be rendered as a separate scrollable document.
        /// </para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#flow-scrolled-doc" /> for more information.</para>
        /// </summary>
        FLOW_SCROLLED_DOC,

        /// <summary>
        /// <para>The 'rendition:flow-auto' property. Indicates that the EPUB creator has no preference for overflow content handling.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#flow-auto" /> for more information.</para>
        /// </summary>
        FLOW_AUTO,

        /// <summary>
        /// <para>The 'rendition:align-x-center' property. Specifies that the given spine item should be centered horizontally in the viewport or spread.</para>
        /// <para>See <see href="https://www.w3.org/TR/epub-33/#align-x-center" /> for more information.</para>
        /// </summary>
        ALIGN_X_CENTER,

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
                "rendition:layout-pre-paginated" => EpubSpineProperty.LAYOUT_PRE_PAGINATED,
                "rendition:layout-reflowable" => EpubSpineProperty.LAYOUT_REFLOWABLE,
                "rendition:orientation-auto" => EpubSpineProperty.ORIENTATION_AUTO,
                "rendition:orientation-landscape" => EpubSpineProperty.ORIENTATION_LANDSCAPE,
                "rendition:orientation-portrait" => EpubSpineProperty.ORIENTATION_PORTRAIT,
                "rendition:spread-auto" => EpubSpineProperty.SPREAD_AUTO,
                "rendition:spread-both" => EpubSpineProperty.SPREAD_BOTH,
                "rendition:spread-landscape" => EpubSpineProperty.SPREAD_LANDSCAPE,
                "rendition:spread-none" => EpubSpineProperty.SPREAD_NONE,
                "rendition:page-spread-center" => EpubSpineProperty.PAGE_SPREAD_CENTER,
                "rendition:page-spread-left" or "page-spread-left" => EpubSpineProperty.PAGE_SPREAD_LEFT,
                "rendition:page-spread-right" or "page-spread-right" => EpubSpineProperty.PAGE_SPREAD_RIGHT,
                "rendition:flow-paginated" => EpubSpineProperty.FLOW_PAGINATED,
                "rendition:flow-scrolled-continuous" => EpubSpineProperty.FLOW_SCROLLED_CONTINUOUS,
                "rendition:flow-scrolled-doc" => EpubSpineProperty.FLOW_SCROLLED_DOC,
                "rendition:flow-auto" => EpubSpineProperty.FLOW_AUTO,
                "rendition:align-x-center" => EpubSpineProperty.ALIGN_X_CENTER,
                _ => EpubSpineProperty.UNKNOWN
            };
        }
    }
}
