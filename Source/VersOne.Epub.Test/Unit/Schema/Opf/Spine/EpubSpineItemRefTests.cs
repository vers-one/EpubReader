using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf.Spine
{
    public class EpubSpineItemRefTests
    {
        private const string ID = "id";
        private const string ID_REF = "idref";
        private const bool IS_LINEAR = true;

        private static List<EpubSpineProperty> Properties =>
            new()
            {
                EpubSpineProperty.PAGE_SPREAD_LEFT
            };

        [Fact(DisplayName = "Constructing a EpubSpineItemRef instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubSpineItemRef epubSpineItemRef = new(ID, ID_REF, IS_LINEAR, Properties);
            Assert.Equal(ID, epubSpineItemRef.Id);
            Assert.Equal(ID_REF, epubSpineItemRef.IdRef);
            Assert.Equal(IS_LINEAR, epubSpineItemRef.IsLinear);
            Assert.Equal(Properties, epubSpineItemRef.Properties);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if idRef parameter is null")]
        public void ConstructorWithNullIdRefTest()
        {
            Assert.Throws<ArgumentNullException>(() => new EpubSpineItemRef(ID, null!, IS_LINEAR, Properties));
        }

        [Fact(DisplayName = "Constructing a EpubSpineItemRef instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            EpubSpineItemRef epubSpineItemRef = new(null, ID_REF, IS_LINEAR, Properties);
            Assert.Null(epubSpineItemRef.Id);
            Assert.Equal(ID_REF, epubSpineItemRef.IdRef);
            Assert.Equal(IS_LINEAR, epubSpineItemRef.IsLinear);
            Assert.Equal(Properties, epubSpineItemRef.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubSpineItemRef instance with null properties parameter should succeed")]
        public void ConstructorWithNullPropertiesTest()
        {
            EpubSpineItemRef epubSpineItemRef = new(ID, ID_REF, IS_LINEAR, null);
            Assert.Equal(ID, epubSpineItemRef.Id);
            Assert.Equal(ID_REF, epubSpineItemRef.IdRef);
            Assert.Equal(IS_LINEAR, epubSpineItemRef.IsLinear);
            Assert.Null(epubSpineItemRef.Properties);
        }

        [Fact(DisplayName = "ToString method should produce a string with the ID and ID ref if both of them are set")]
        public void ToStringWithIdAndIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new
            (
                id: "testId",
                idRef: "testIdRef",
                isLinear: false
            );
            Assert.Equal("Id: testId; IdRef: testIdRef", epubSpineItemRef.ToString());
        }

        [Fact(DisplayName = "ToString method should produce a string with ID ref when it is set and Id is null")]
        public void ToStringWithNullIdAndNonNullIdRefTest()
        {
            EpubSpineItemRef epubSpineItemRef = new
            (
                id: null,
                idRef: "testIdRef",
                isLinear: false
            );
            Assert.Equal("IdRef: testIdRef", epubSpineItemRef.ToString());
        }
    }
}
