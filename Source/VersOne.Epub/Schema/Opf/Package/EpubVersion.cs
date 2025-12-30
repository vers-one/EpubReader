using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Version of the EPUB specification to which the given <see cref="EpubPackage" /> conforms.</para>
    /// <para>
    /// See <see href="https://www.w3.org/TR/epub-33/#attrdef-package-version" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section1.3.2" /> for more information.
    /// </para>
    /// </summary>
    public enum EpubVersion
    {
        /// <summary>
        /// Version 2. Can be either EPUB 2.0 or EPUB 2.0.1.
        /// </summary>
        EPUB_2 = 2,

        /// <summary>
        /// Version 3. Can be either EPUB 3.0, EPUB 3.0.1, EPUB 3.2, or EPUB 3.3.
        /// </summary>
        EPUB_3,

        /// <summary>
        /// Version 3.1. Represents the deprecated EPUB 3.1 standard.
        /// </summary>
        EPUB_3_1
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and extension method need to be close to each other to avoid issues when the enum was changed without changing the extension method. The file needs to be named after enum.")]
    internal static class VersionUtils
    {
        public static string GetVersionString(this EpubVersion epubVersion) => epubVersion switch
        {
            EpubVersion.EPUB_2 => "2",
            EpubVersion.EPUB_3 => "3",
            EpubVersion.EPUB_3_1 => "3.1",
            _ => epubVersion.ToString(),
        };
    }
}
