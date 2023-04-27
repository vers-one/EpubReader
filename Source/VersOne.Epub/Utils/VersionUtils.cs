using System;
using System.Reflection;
using VersOne.Epub.Schema;

namespace VersOne.Epub.Internal
{
    internal static class VersionUtils
    {
        public static string GetVersionString(EpubVersion epubVersion)
        {
            Type epubVersionType = typeof(EpubVersion);
            FieldInfo fieldInfo = epubVersionType.GetRuntimeField(epubVersion.ToString());
            if (fieldInfo != null)
            {
                return fieldInfo.GetCustomAttribute<VersionStringAttribute>().Version;
            }
            else
            {
                return epubVersion.ToString();
            }
        }
    }
}
