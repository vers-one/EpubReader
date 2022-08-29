using System.Text;

namespace VersOne.Epub.Test.Unit.TestUtils
{
    internal static class StreamUtils
    {
        public static MemoryStream CreateMemoryStreamForString(string input)
        {
            return new MemoryStream(Encoding.UTF8.GetBytes(input));
        }
    }
}
