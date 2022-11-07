namespace VersOne.Epub.Test.Unit.Content
{
    public class EpubContentFileTests
    {
        [Fact(DisplayName = "EpubLocalTextContentFile constructor should set correct property values")]
        public void LocalTextContentFileConstructorTest()
        {
            EpubLocalTextContentFile epubLocalTextContentFile = new();
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalTextContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubLocalTextContentFile.ContentFileType);
        }

        [Fact(DisplayName = "EpubLocalByteContentFile constructor should set correct property values")]
        public void LocalByteContentFileConstructorTest()
        {
            EpubLocalByteContentFile epubLocalByteContentFile = new();
            Assert.Equal(EpubContentLocation.LOCAL, epubLocalByteContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubLocalByteContentFile.ContentFileType);
        }

        [Fact(DisplayName = "EpubRemoteTextContentFile constructor should set correct property values")]
        public void RemoteTextContentFileConstructorTest()
        {
            EpubRemoteTextContentFile epubRemoteTextContentFile = new();
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteTextContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.TEXT, epubRemoteTextContentFile.ContentFileType);
        }

        [Fact(DisplayName = "EpubRemoteByteContentFile constructor should set correct property values")]
        public void RemoteByteContentFileConstructorTest()
        {
            EpubRemoteByteContentFile epubRemoteByteContentFile = new();
            Assert.Equal(EpubContentLocation.REMOTE, epubRemoteByteContentFile.ContentLocation);
            Assert.Equal(EpubContentFileType.BYTE_ARRAY, epubRemoteByteContentFile.ContentFileType);
        }
    }
}
