using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    public enum PageProgressionDirection
    {
        DEFAULT = 1,
        LEFT_TO_RIGHT,
        RIGHT_TO_LEFT,
        UNKNOWN
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and parser need to be close to each other to avoid issues when the enum was changed without changing the parser. The file needs to be named after enum.")]
    internal static class PageProgressionDirectionParser
    {
        public static PageProgressionDirection Parse(string stringValue)
        {
            switch (stringValue.ToLowerInvariant())
            {
                case "default":
                    return PageProgressionDirection.DEFAULT;
                case "ltr":
                    return PageProgressionDirection.LEFT_TO_RIGHT;
                case "rtl":
                    return PageProgressionDirection.RIGHT_TO_LEFT;
                default:
                    return PageProgressionDirection.UNKNOWN;
            }
        }
    }
}
