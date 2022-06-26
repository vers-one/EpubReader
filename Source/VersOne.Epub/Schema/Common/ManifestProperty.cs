using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    public enum ManifestProperty
    {
        COVER_IMAGE = 1,
        MATHML,
        NAV,
        REMOTE_RESOURCES,
        SCRIPTED,
        SVG,
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class ManifestPropertyParser
    {
        public static ManifestProperty Parse(string stringValue)
        {
            switch (stringValue.ToLowerInvariant())
            {
                case "cover-image":
                    return ManifestProperty.COVER_IMAGE;
                case "mathml":
                    return ManifestProperty.MATHML;
                case "nav":
                    return ManifestProperty.NAV;
                case "remote-resources":
                    return ManifestProperty.REMOTE_RESOURCES;
                case "scripted":
                    return ManifestProperty.SCRIPTED;
                case "svg":
                    return ManifestProperty.SVG;
                default:
                    return ManifestProperty.UNKNOWN;
            }
        }
    }
}
