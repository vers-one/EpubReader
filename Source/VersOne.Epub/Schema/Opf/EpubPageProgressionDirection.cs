using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Page progression direction within the spine. Represents the global direction in which the content flows.</para>
    /// <para>See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-spine-page-progression-direction" /> for more information.</para>
    /// </summary>
    public enum EpubPageProgressionDirection
    {
        /// <summary>
        /// Default page progression direction. The actual direction should be chosen based on the book's language.
        /// </summary>
        DEFAULT = 1,

        /// <summary>
        /// Left-to-right page progression direction.
        /// </summary>
        LEFT_TO_RIGHT,

        /// <summary>
        /// Right-to-left page progression direction.
        /// </summary>
        RIGHT_TO_LEFT,

        /// <summary>
        /// A page progression direction which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubPageProgressionDirectionParser
    {
        public static EpubPageProgressionDirection Parse(string stringValue)
        {
            switch (stringValue.ToLowerInvariant())
            {
                case "default":
                    return EpubPageProgressionDirection.DEFAULT;
                case "ltr":
                    return EpubPageProgressionDirection.LEFT_TO_RIGHT;
                case "rtl":
                    return EpubPageProgressionDirection.RIGHT_TO_LEFT;
                default:
                    return EpubPageProgressionDirection.UNKNOWN;
            }
        }
    }
}
