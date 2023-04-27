using VersOne.Epub.Internal;

namespace VersOne.Epub.Test.Unit.Utils
{
    public class ZipPathUtilsTests
    {
        [Theory(DisplayName = "Getting the directory path for a valid file path should succeed")]
        [InlineData("Directory/File.html", "Directory")]
        [InlineData("Directory/Subdirectory/File.html", "Directory/Subdirectory")]
        [InlineData("File.html", "")]
        [InlineData("Directory/", "Directory")]
        [InlineData("", "")]
        [InlineData("Directory/../File.html", "Directory/..")]
        [InlineData("../File.html", "..")]
        public void GetDirectoryPathTest(string filePath, string expectedDirectoryPath)
        {
            string actualDirectoryPath = ZipPathUtils.GetDirectoryPath(filePath);
            Assert.Equal(expectedDirectoryPath, actualDirectoryPath);
        }

        [Theory(DisplayName = "Combining directory path and file name should succeed")]
        [InlineData("Directory", "File.html", "Directory/File.html")]
        [InlineData("Directory/Subdirectory", "File.html", "Directory/Subdirectory/File.html")]
        [InlineData("", "File.html", "File.html")]
        [InlineData(null, "File.html", "File.html")]
        [InlineData("Directory/Subdirectory", "../File.html", "Directory/File.html")]
        [InlineData("Directory", "../File.html", "File.html")]
        [InlineData("Directory/Subdirectory/Subsubdirectory", "../../File.html", "Directory/File.html")]
        [InlineData("Directory/Subdirectory", "../../File.html", "File.html")]
        [InlineData("Directory", "../../File.html", "File.html")]
        [InlineData("", "../File.html", "../File.html")]
        [InlineData(null, "../File.html", "../File.html")]
        [InlineData("Directory", null, "Directory")]
        [InlineData(null, null, null)]
        public void CombineTest(string directory, string fileName, string expectedResult)
        {
            string actualResult = ZipPathUtils.Combine(directory, fileName);
            Assert.Equal(expectedResult, actualResult);
        }
    }
}
