using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VersOne.Epub.Test.Integration.Runner;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseJsonSerializer : JsonSerializer
    {
        public TestCaseJsonSerializer(TestCaseExtensionDataHandler testCaseExtensionDataHandler)
        {
            ContractResolver = new TestCaseContractResolver(testCaseExtensionDataHandler);
            Converters.Add(new StringEnumConverter());
            SerializationBinder = new AmbiguousTypeBinder();
            TypeNameHandling = TypeNameHandling.Auto;
        }

        public void Serialize(StreamWriter streamWriter, TestCase testCase)
        {
            using TestCaseJsonTextWriter testCaseJsonTextWriter = new(streamWriter);
            Serialize(testCaseJsonTextWriter, testCase);
        }

        public TestCase Deserialize(StreamReader streamReader)
        {
            return Deserialize(streamReader, typeof(TestCase)) as TestCase;
        }
    }
}
