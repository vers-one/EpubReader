namespace VersOne.Epub.Test.Integration.Types
{
    public class TestCaseException(string type, string? message = null)
    {
        public string Type { get; set; } = type ?? throw new ArgumentNullException(nameof(type));
        public string? Message { get; set; } = message;
    }
}
