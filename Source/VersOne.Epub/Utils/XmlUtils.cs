using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using VersOne.Epub.Options;

namespace VersOne.Epub.Internal
{
    internal static class XmlUtils
    {
        private const string XML_DECLARATION_FIRST_CHARS = "<?xml";

        public static async Task<XDocument> LoadDocumentAsync(Stream stream, XmlReaderOptions? xmlReaderOptions = null)
        {
            using MemoryStream memoryStream = new();
            await stream.CopyToAsync(memoryStream).ConfigureAwait(false);
            memoryStream.Position = 0;
            if (xmlReaderOptions != null && xmlReaderOptions.SkipXmlHeaders)
            {
                SkipXmlHeader(memoryStream);
            }
            XmlReaderSettings xmlReaderSettings = new()
            {
                DtdProcessing = DtdProcessing.Ignore,
                Async = true
            };
            using XmlReader xmlReader = XmlReader.Create(memoryStream, xmlReaderSettings);
            return await Task.Run(() => XDocument.Load(xmlReader)).ConfigureAwait(false);
        }

        // This is a workaround for an issue that XML 1.1 files are not supported in .NET.
        // See https://github.com/vers-one/EpubReader/issues/34 for more details.
        // This method checks if the stream content starts with an XML declaration (<?xml ...)
        // in which case it moves the stream position to the next '<' character
        // (i.e. to the first XML content node, effectively skipping the XML header).
        // Files with no XML declaration are treated as XML 1.0 files by .NET.
        // If the stream content doesn't contain XML declaration, this method does not change its position.
        private static void SkipXmlHeader(MemoryStream memoryStream)
        {
            bool IsMatch(IList<byte> buffer, string pattern, int startPosition)
            {
                for (int i = 0; i < pattern.Length; i++)
                {
                    if (buffer[startPosition + i] != pattern[i])
                    {
                        return false;
                    }
                }
                return true;
            }

            int IndexOf(IList<byte> buffer, string pattern, int startPosition)
            {
                for (int i = startPosition; i < buffer.Count - pattern.Length; i++)
                {
                    if (IsMatch(buffer, pattern, i))
                    {
                        return i;
                    }
                }
                return -1;
            }

            memoryStream.TryGetBuffer(out ArraySegment<byte> memoryStreamBuffer);
            int position = IndexOf(memoryStreamBuffer, XML_DECLARATION_FIRST_CHARS, 0);
            if (position == -1)
            {
                return;
            }
            position = IndexOf(memoryStreamBuffer, "<", position + XML_DECLARATION_FIRST_CHARS.Length);
            if (position == -1)
            {
                return;
            }
            memoryStream.Position = position;
        }
    }
}
