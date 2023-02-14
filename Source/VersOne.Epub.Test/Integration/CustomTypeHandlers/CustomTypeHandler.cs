using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal interface ICustomTypeHandler
    {
        Type Type { get; }
        Dictionary<string, ICustomPropertyHandler> CustomPropertyHandlers { get; }
        Dictionary<string, PropertyDefaultValue> OptionalProperties { get; }
        HashSet<string> IgnoredProperties { get; }
        bool PreserveReferences { get; }
    }

    internal abstract class CustomTypeHandler<T> : ICustomTypeHandler where T : class
    {
        protected CustomTypeHandler(TestCasesSerializationContext testCasesSerializationContext, Dictionary<string, PropertyDefaultValue>? optionalProperties = null,
            HashSet<string>? ignoredProperties = null)
        {
            CustomPropertyHandlers = new Dictionary<string, ICustomPropertyHandler>();
            TestCasesSerializationContext = testCasesSerializationContext;
            OptionalProperties = optionalProperties ?? new Dictionary<string, PropertyDefaultValue>();
            IgnoredProperties = ignoredProperties ?? new HashSet<string>();
        }

        public Type Type => typeof(T);

        public abstract bool PreserveReferences { get; }
        public Dictionary<string, ICustomPropertyHandler> CustomPropertyHandlers { get; }
        public Dictionary<string, PropertyDefaultValue> OptionalProperties { get; }
        public HashSet<string> IgnoredProperties { get; }
        public TestCasesSerializationContext TestCasesSerializationContext { get; }

        protected void AddCustomPropertyHandler(string typePropertyName, string jsonPropertyName, Func<T, JsonNode?> propertySerializer, Func<string?, object?> propertyDeserializer)
        {
            CustomPropertyHandlers.Add(typePropertyName, new CustomPropertyHandler<T>(typePropertyName, jsonPropertyName, propertySerializer, propertyDeserializer));
        }

        protected void AddOptionalProperty(string typePropertyName, PropertyDefaultValue propertyDefaultValue)
        {
            OptionalProperties.Add(typePropertyName, propertyDefaultValue);
        }

        protected void AddIgnoredProperty(string typePropertyName)
        {
            IgnoredProperties.Add(typePropertyName);
        }
    }
}
