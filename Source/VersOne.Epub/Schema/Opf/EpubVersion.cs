using System;
using System.Diagnostics.CodeAnalysis;

namespace VersOne.Epub.Schema
{
    public enum EpubVersion
    {
        [VersionString("2")]
        EPUB_2 = 2,

        [VersionString("3")]
        EPUB_3,

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
