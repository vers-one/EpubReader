using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Ops.Narration
{
    public class SmilTextTests
    {
        private const string ID = "id";
        private const string SRC = "chapter.html";

        [Fact(DisplayName = "Constructing a SmilText instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            SmilText smilText = new(ID, SRC);
            Assert.Equal(ID, smilText.Id);
            Assert.Equal(SRC, smilText.Src);
        }

        [Fact(DisplayName = "Constructing a SmilText instance with null id parameter should succeed")]
        public void ConstructorWithNullIdTest()
        {
            SmilText smilText = new(null, SRC);
            Assert.Null(smilText.Id);
            Assert.Equal(SRC, smilText.Src);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if src parameter is null")]
        public void ConstructorWithNullSrcTest()
        {
            Assert.Throws<ArgumentNullException>(() => new SmilText(ID, null!));
        }
    }
}
