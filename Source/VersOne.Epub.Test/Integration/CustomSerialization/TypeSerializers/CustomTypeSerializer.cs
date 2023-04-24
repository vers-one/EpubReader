using System.Text.Json.Nodes;
using VersOne.Epub.Test.Integration.CustomSerialization.Context;

namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal abstract class CustomTypeSerializer
    {
        protected CustomTypeSerializer()
        {
            CustomPropertySerializers = new Dictionary<string, CustomPropertySerializer>();
        }

        public abstract Type Type { get; }
        public Dictionary<string, CustomPropertySerializer> CustomPropertySerializers { get; }
    }

    internal abstract class CustomTypeSerializer<T> : CustomTypeSerializer where T : class
    {
        public override Type Type => typeof(T);

        protected void AddCustomPropertySerializer(string typePropertyName, string jsonPropertyName,
            Func<T, TestEpubFile, JsonNode?> propertySerializer, Func<string?, TestEpubFile, object?> propertyDeserializer)
        {
            CustomPropertySerializers.Add(typePropertyName, new CustomPropertySerializer<T>(typePropertyName, jsonPropertyName, propertySerializer, propertyDeserializer));
        }
    }
}
