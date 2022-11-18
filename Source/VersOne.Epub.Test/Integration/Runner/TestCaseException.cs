namespace VersOne.Epub.Test.Integration.Runner
{
    public class TestCaseException
    {
        public TestCaseException(string type, string? message)
        {
            Type = type ?? throw new ArgumentNullException(nameof(type));
            Message = message;
        }

        public string Type { get; set; }
        public string? Message { get; set; }
    }
}
