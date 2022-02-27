using System;

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

    internal class VersionStringAttribute : Attribute
    {
        public VersionStringAttribute(string version)
        {
            Version = version;
        }

        public string Version { get; }
    }
}
