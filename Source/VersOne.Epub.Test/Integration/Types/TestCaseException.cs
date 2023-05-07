namespace VersOne.Epub.Test.Integration.Types
{
    public class TestCaseException
    {
        public TestCaseException(string type, string? message = null)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Message = message;
        }

        public string Type { get; set; }
        public string? Message { get; set; }
    }
}
