using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;
using VersOne.Epub.Test.Integration.Runner;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseJsonSerializer : JsonSerializer
    {
        public TestCaseJsonSerializer(CustomPropertyDependencies customPropertyDependencies)
        {
            ContractResolver = new TestCaseContractResolver(customPropertyDependencies);
            Converters.Add(new StringEnumConverter());
            DefaultValueHandling = DefaultValueHandling.Ignore;
            SerializationBinder = new AmbiguousTypeBinder();
            TypeNameHandling = TypeNameHandling.Auto;
        }

        public void Serialize(StreamWriter streamWriter, List<TestCase> testCases)
        {
            using TestCaseJsonTextWriter testCaseJsonTextWriter = new(streamWriter);
            Serialize(testCaseJsonTextWriter, testCases);
        }

        public List<TestCase>? Deserialize(StreamReader streamReader)
        {
            return Deserialize(streamReader, typeof(List<TestCase>)) as List<TestCase>;
        }
    }
}
