namespace VersOne.Epub.Schema
{
    public class EpubGuideReference
    {
        public string Type { get; set; }
        public string Title { get; set; }
        public string Href { get; set; }

        public override string ToString()
        {
            return $"Type: {Type}, Href: {Href}";
        }
    }
}
