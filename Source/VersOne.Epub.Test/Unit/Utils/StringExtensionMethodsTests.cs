using VersOne.Epub.Utils;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class StringExtensionMethodsTests
    {
        [Fact(DisplayName = "Case-insensitive comparison should succeed")]
        public void CompareOrdinalIgnoreCaseTest()
        {
            Assert.True("Test".CompareOrdinalIgnoreCase("test"));
            Assert.False("Test".CompareOrdinalIgnoreCase("something-else"));
        }
    }
}
