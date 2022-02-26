namespace VersOne.Epub.Schema
{
    public class Epub2NcxContent
    {
        public string Id { get; set; }
        public string Source { get; set; }

        public override string ToString()
        {
            return "Source: " + Source;
        }
    }
}
