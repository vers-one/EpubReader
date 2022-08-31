using System.Xml.Linq;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class XmlExtensionMethodsTests
    {
        [Fact(DisplayName = "Extracting the local name of an element and converting it to lower case should succeed")]
        public void GetElementLowerCaseLocalNameTest()
        {
            XNamespace xNamespace = "http://www.idpf.org/2007/opf";
            XElement xElement = new(xNamespace + "Element");
            string lowerCaseLocalName = xElement.GetLowerCaseLocalName();
            Assert.Equal("element", lowerCaseLocalName);
        }

        [Fact(DisplayName = "Extracting the local name of an attribute and converting it to lower case should succeed")]
        public void GetAttributeLowerCaseLocalNameTest()
        {
            XNamespace xNamespace = "http://www.idpf.org/2007/opf";
            XAttribute xAttribute = new(xNamespace + "Attribute", "value");
            string lowerCaseLocalName = xAttribute.GetLowerCaseLocalName();
            Assert.Equal("attribute", lowerCaseLocalName);
        }

        [Fact(DisplayName = "Extracting the local name of an element and making a case-insensitive comparison to a string should succeed")]
        public void CompareElementNameTest()
        {
            XNamespace xNamespace = "http://www.idpf.org/2007/opf";
            XElement xElement = new(xNamespace + "Element");
            Assert.True(xElement.CompareNameTo("element"));
            Assert.False(xElement.CompareNameTo("something-else"));
        }

        [Fact(DisplayName = "Case-insensitive comparison of the value of an attribute to a string should succeed")]
        public void CompareAttributeValueTest()
        {
            XAttribute xAttribute = new("attribute", "Value");
            Assert.True(xAttribute.CompareValueTo("value"));
            Assert.False(xAttribute.CompareValueTo("something-else"));
        }
    }
}
