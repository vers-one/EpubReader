using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Linq;

namespace VersOne.Epub.Schema
{
    public enum SpineProperty
    {
        PAGE_SPREAD_LEFT = 1,
        PAGE_SPREAD_RIGHT,
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class SpinePropertyParser
    {
        public static List<SpineProperty> ParsePropertyList(string stringValue)
        {
            return stringValue.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).
                Select(propertyString => ParseProperty(propertyString.Trim())).
                ToList();
        }

        public static SpineProperty ParseProperty(string stringValue)
        {
            switch (stringValue.ToLowerInvariant())
            {
                case "page-spread-left":
                    return SpineProperty.PAGE_SPREAD_LEFT;
                case "page-spread-right":
                    return SpineProperty.PAGE_SPREAD_RIGHT;
                default:
                    return SpineProperty.UNKNOWN;
            }
        }
    }
}
