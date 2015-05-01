using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace VersFx.Formats.Text.Epub.Utils
{
    internal static class XmlUtils
    {
        public static XmlDocument LoadDocument(Stream stream)
        {
            XmlDocument result = new XmlDocument();
            XmlReaderSettings xmlReaderSettings = new XmlReaderSettings
            {
                XmlResolver = null,
                DtdProcessing = DtdProcessing.Ignore
            };
            using (XmlReader xmlReader = XmlReader.Create(stream, xmlReaderSettings))
                result.Load(xmlReader);
            return result;
        }
    }
}
