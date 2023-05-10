using System;
using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    /// <summary>
    /// <para>Version of the EPUB specification to which the given <see cref="EpubPackage" /> conforms.</para>
    /// <para>
    /// See <see href="https://www.w3.org/publishing/epub32/epub-packages.html#attrdef-package-version" />
    /// and <see href="https://idpf.org/epub/20/spec/OPF_2.0.1_draft.htm#Section1.3.2" /> for more information.
    /// </para>
    /// </summary>
    public enum EpubVersion
    {
        /// <summary>
        /// Version 2. Can be either EPUB 2.0 or EPUB 2.0.1.
        /// </summary>
        [VersionString("2")]
        EPUB_2 = 2,

        /// <summary>
        /// Version 3. Can be either EPUB 3.0, EPUB 3.0.1, or EPUB 3.2.
        /// </summary>
        [VersionString("3")]
        EPUB_3,

        /// <summary>
        /// Version 3.1. Represents the deprecated EPUB 3.1 standard.
        /// </summary>
        [VersionString("3.1")]
        EPUB_3_1
    }

    [SuppressMessage("StyleCop.CSharp.DocumentationRules", "SA1649:File name should match first type name",
        Justification = "Enum and attribute need to be close to each other to indicate that attribute applies only to this enum. The file needs to be named after enum.")]
    internal class VersionStringAttribute : Attribute
    {
        public VersionStringAttribute(string version)
        {
            Version = version;
        }

        public string Version { get; }
    }
}
