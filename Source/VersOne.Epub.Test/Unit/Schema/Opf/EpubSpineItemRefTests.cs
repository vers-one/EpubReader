using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubSpineItemRefTests
    {
        [Fact(DisplayName = "ToString method should produce a string with the ID and ID ref if both of them are set")]
        public void ToStringWithIdAndIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new()
            {
                Id = "testId",
                IdRef = "testIdRef"
            };
            Assert.Equal("Id: testId; IdRef: testIdRef", epubSpineItemRef.ToString());
        }

        [Fact(DisplayName = "ToString method should produce a string with ID ref when it is set and Id is null")]
        public void ToStringWithNullIdAndNonNullIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new()
            {
                Id = null,
                IdRef = "testIdRef"
            };
            Assert.Equal("IdRef: testIdRef", epubSpineItemRef.ToString());
        }

        [Fact(DisplayName = "ToString method should produce a string with ID and a null indicator for ID ref when Id is set and IdRef is null")]
        public void ToStringWithNonNullIdAndNullIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new()
            {
                Id = "testId",
                IdRef = null
            };
            Assert.Equal("Id: testId; IdRef: (null)", epubSpineItemRef.ToString());
        }

        [Fact(DisplayName = "ToString method should produce a string with a null indicator for ID ref when both Id and IdRef are null")]
        public void ToStringWithNullIdAndNullIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new()
            {
                Id = null,
                IdRef = null
            };
            Assert.Equal("IdRef: (null)", epubSpineItemRef.ToString());
        }
    }
}
