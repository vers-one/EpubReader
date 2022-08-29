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

        private XDocument ExpectedXDocument =>
            new(
                new XElement("parent",
                    new XElement("child")
                )
            );

        [Fact]
        public async void TestXml10File()
        {
            XDocument actualXDocument = await XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_0_FILE), new XmlReaderOptions());
            CompareXDocuments(ExpectedXDocument, actualXDocument);
        }

        [Fact]
        public async void TestXml11FileThrowsException()
        {
            await Assert.ThrowsAsync<XmlException>(() => XmlUtils.LoadDocumentAsync(StreamUtils.CreateMemoryStreamForString(XML_1_1_FILE), new XmlReaderOptions()));
        }

        [Theory]
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

        private void CompareXDocuments(XDocument expected, XDocument actual)
        {
            if (expected == null)
            {
                Assert.Null(actual);
            }
            else
            {
                Assert.NotNull(actual);

            }
        }

        private void CompareXElements(XElement expected, XElement actual)
        {
            Assert.Equal(expected.Name, actual.Name);
            foreach (XElement expectedDescendant in expected.Descendants())
            {
                XElement actualDescendant = actual.Element(expectedDescendant.Name);
                Assert.NotNull(actualDescendant);
                CompareXElements(expectedDescendant, actualDescendant);
            }
        }
    }
}
