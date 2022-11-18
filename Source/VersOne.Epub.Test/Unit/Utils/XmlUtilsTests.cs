using System.Xml;
using System.Xml.Linq;
using VersOne.Epub.Internal;
using VersOne.Epub.Options;
using VersOne.Epub.Test.Unit.TestUtils;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class XmlUtilsTests
    {
        private const string XML_1_0_HEADER = "<?xml version='1.0' encoding='utf-8'?>";
        private const string XML_1_1_HEADER = "<?xml version='1.1' encoding='utf-8'?>";
        private const string XML_1_0_FILE = XML_1_0_HEADER + "\r\n" + XML_BODY;
        private const string XML_1_1_FILE = XML_1_1_HEADER + "\r\n" + XML_BODY;

        private const string XML_BODY = $"""
            <parent>
                <child />
            </parent>
            """;

        private static XDocument ExpectedXDocument =>
            new(
                new XElement("parent",
                    new XElement("child")
                )
            );

        [Fact(DisplayName = "Loading a regular XML 1.0 file should succeed")]
        public async void TestXml10File()
        {
            XDocument actualXDocument = await XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_0_FILE), new XmlReaderOptions());
            CompareXDocuments(ExpectedXDocument, actualXDocument);
        }

        [Fact(DisplayName = "Loading an XML 1.1 file with XmlReaderOptions.SkipXmlHeaders = false should throw XmlException")]
        public async void TestXml11FileThrowsException()
        {
            await Assert.ThrowsAsync<XmlException>(() => XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_1_FILE), new XmlReaderOptions()));
        }

        [Fact(DisplayName = "Loading an XML 1.1 file with null XmlReaderOptions should throw XmlException")]
        public async void TestXml11FileWithNullXmlReaderOptionsThrowsException()
        {
            await Assert.ThrowsAsync<XmlException>(() => XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_1_FILE), null));
        }

        [Theory(DisplayName = "Loading either an XML 1.1 file or an XML file without a header with XmlReaderOptions.SkipXmlHeaders = true should succeed")]
        [InlineData(XML_1_1_FILE)]
        [InlineData(XML_BODY)]
        public async void TestXml11FileWorkaround(string xmlFileContent)
        {
            XmlReaderOptions xmlReaderOptions = new()
            {
                SkipXmlHeaders = true
            };
            XDocument actualXDocument = await XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(xmlFileContent), xmlReaderOptions);
            CompareXDocuments(ExpectedXDocument, actualXDocument);
        }

        [Fact(DisplayName = "Loading a file with just XML 1.1 header and no other content with XmlReaderOptions.SkipXmlHeaders = true should throw XmlException")]
        public async void TestXml11EmptyFileThrowsException()
        {
            XmlReaderOptions xmlReaderOptions = new()
            {
                SkipXmlHeaders = true
            };
            await Assert.ThrowsAsync<XmlException>(() => XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_1_HEADER), xmlReaderOptions));
        }

        private void CompareXDocuments(XDocument expected, XDocument actual)
        {
            Assert.NotNull(actual);
            CompareXElements(expected.Root, actual.Root);
        }

        private void CompareXElements(XElement? expected, XElement? actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);
                Assert.Equal(expected.Name, actual.Name);
                foreach (XElement expectedDescendant in expected.Descendants())
                {
                    XElement? actualDescendant = actual.Element(expectedDescendant.Name);
                    Assert.NotNull(actualDescendant);
                    CompareXElements(expectedDescendant, actualDescendant);
                }
            }
        }
    }
}
