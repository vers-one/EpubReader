using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.CustomSerialization
{
    internal class TestCaseSerializer : JsonUtils.JsonSerializer
    {
        public TestCaseSerializer()
            : base(new JsonSerializerConfiguration(CustomTypes.Types.Values))
        {
        }

        public void Serialize(string testCasesFilePath, string testEpubFilePath, List<TestCase> testCases)
        {
            using TestEpubFile testEpubFile = new(testEpubFilePath);
            TestCaseSerializationContext testCaseSerializationContext = new(testEpubFile);
            JsonNode? testCasesJson = Serialize(testCases, testCaseSerializationContext);
            Assert.NotNull(testCasesJson);
            using FileStream fileStream = new(testCasesFilePath, FileMode.Create);
            JsonWriterOptions jsonWriterOptions = new()
            {
                Indented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            using Utf8JsonWriter utf8JsonWriter = new(fileStream, jsonWriterOptions);
            testCasesJson.WriteTo(utf8JsonWriter);
        }

        public List<TestCase> Deserialize(string testCasesFilePath, string testEpubFilePath)
        {
            using TestEpubFile testEpubFile = new(testEpubFilePath);
            TestCaseSerializationContext testCaseSerializationContext = new(testEpubFile);
            using FileStream fileStream = new(testCasesFilePath, FileMode.Open);
            using JsonDocument jsonDocument = JsonDocument.Parse(fileStream);
            JsonElement rootElement = jsonDocument.RootElement;
            List<TestCase>? result = Deserialize<List<TestCase>>(rootElement, testCaseSerializationContext) as List<TestCase>;
            Assert.NotNull(result);
            return result;
        }
    }
}
