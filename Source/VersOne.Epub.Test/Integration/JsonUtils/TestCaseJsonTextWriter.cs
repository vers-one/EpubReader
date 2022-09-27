using Newtonsoft.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseJsonTextWriter : JsonTextWriter
    {
        private readonly TextWriter textWriter;

        public TestCaseJsonTextWriter(TextWriter textWriter)
            : base(textWriter)
        {
            this.textWriter = textWriter;
            Indentation = 4;
            Formatting = Formatting.Indented;
        }

        public override void WriteStartArray()
        {
            if (Top > 0)
            {
                WriteWhitespace(textWriter.NewLine);
                WriteWhitespace(new string(' ', Top * Indentation - 1));
            }
            base.WriteStartArray();
        }

        public override void WriteStartObject()
        {
            if (WriteState == WriteState.Property && Top > 0)
            {
                WriteWhitespace(textWriter.NewLine);
                WriteWhitespace(new string(' ', Top * Indentation - 1));
            }
            base.WriteStartObject();
        }
    }
}
