using System;
using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Text direction of the content.</para>
    /// <para>See <see href="https://www.w3.org/TR/epub-33/#attrdef-dir" /> for more information.</para>
    /// </summary>
    public enum EpubTextDirection
    {
        /// <summary>
        /// Left-to-right text direction.
        /// </summary>
        LEFT_TO_RIGHT,

        /// <summary>
        /// Right-to-left text direction.
        /// </summary>
        RIGHT_TO_LEFT,

        /// <summary>
        /// Direction is determined using the Unicode Bidirectional Algorithm.
        /// </summary>
        AUTO,

        /// <summary>
        /// A text direction which is not present in this enumeration.
        /// </summary>
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class EpubTextDirectionParser
    {
        public static EpubTextDirection Parse(string stringValue)
        {
            if (String.IsNullOrEmpty(stringValue))
            {
                return EpubTextDirection.UNKNOWN;
            }
            return stringValue.ToLowerInvariant() switch
            {
                "ltr" => EpubTextDirection.LEFT_TO_RIGHT,
                "rtl" => EpubTextDirection.RIGHT_TO_LEFT,
                "auto" => EpubTextDirection.AUTO,
                _ => EpubTextDirection.UNKNOWN
            };
        }
    }
}
