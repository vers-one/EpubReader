using System.Text.Encodings.Web;
using System.Text.Json;
using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseSerializer
    {
        private readonly Dictionary<Type, TypeSerializer> typeSerializers;
        private readonly TypeSerializer testCaseSerializer;

        public TestCaseSerializer(TestCasesSerializationContext testCasesSerializationContext)
        {
            typeSerializers = new Dictionary<Type, TypeSerializer>();
            Dictionary<Type, ICustomTypeHandler> customTypeHandlers = new ICustomTypeHandler[]
            {
                new EpubBookTypeHandler(testCasesSerializationContext),
                new EpubLocalByteContentFileTypeHandler(testCasesSerializationContext),
                new EpubLocalTextContentFileTypeHandler(testCasesSerializationContext),
                new EpubRemoteByteContentFileTypeHandler(testCasesSerializationContext),
                new EpubRemoteTextContentFileTypeHandler(testCasesSerializationContext)
            }.Concat(new TypesWithOptionalProperties(testCasesSerializationContext).Types).ToDictionary(customTypeHandler => customTypeHandler.Type);
            testCaseSerializer = new(typeof(TestCase), typeSerializers, customTypeHandlers);
    }

        public void Serialize(string testCasesFilePath, List<TestCase> testCases)
        {
            JsonArray testCasesJsonArray = new();
            foreach (TestCase testCase in testCases)
            {
                testCasesJsonArray.Add(testCaseSerializer.SerializeObject(testCase));
            }
            using FileStream fileStream = new(testCasesFilePath, FileMode.Create);
            JsonWriterOptions jsonWriterOptions = new()
            {
                Indented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            using Utf8JsonWriter utf8JsonWriter = new(fileStream, jsonWriterOptions);
            testCasesJsonArray.WriteTo(utf8JsonWriter);
        }

        public List<TestCase> Deserialize(string testCasesFilePath)
        {
            List<TestCase> result = new();
            using FileStream fileStream = new(testCasesFilePath, FileMode.Open);
            using JsonDocument jsonDocument = JsonDocument.Parse(fileStream);
            JsonElement rootElement = jsonDocument.RootElement;
            Assert.Equal(JsonValueKind.Array, rootElement.ValueKind);
            foreach (JsonElement serializedTestCase in rootElement.EnumerateArray())
            {
                TestCase? testCase = testCaseSerializer.DeserializeObject(serializedTestCase) as TestCase;
                Assert.NotNull(testCase);
                result.Add(testCase);
            }
            return result;
        }
    }
}
