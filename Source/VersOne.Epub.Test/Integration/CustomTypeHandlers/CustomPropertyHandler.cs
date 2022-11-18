namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal interface ICustomPropertyHandler
    {
        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public string ConstructorParameterName { get; }
        public object? SerializePropertyValue(object serializingObject);
        public object? DeserializePropertyValue(object? serializedValue);
    }

    internal class CustomPropertyHandler<T> : ICustomPropertyHandler where T : class
    {
        private readonly Func<T, string?> propertySerializer;
        private readonly Func<string?, object?> propertyDeserializer;

        public CustomPropertyHandler(string typePropertyName, string jsonPropertyName, Func<T, string?> propertySerializer, Func<string?, object?> propertyDeserializer)
        {
            this.propertySerializer = propertySerializer;
            this.propertyDeserializer = propertyDeserializer;
            TypePropertyName = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
            JsonPropertyName = jsonPropertyName ?? throw new ArgumentNullException(nameof(jsonPropertyName));
            ConstructorParameterName = Char.ToLower(typePropertyName[0]) + typePropertyName[1..];
        }

        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public string ConstructorParameterName { get; }

        public object? SerializePropertyValue(object serializingObject)
        {
            T? typedSerializingObject = serializingObject as T;
            Assert.NotNull(typedSerializingObject);
            return propertySerializer(typedSerializingObject);
        }

        public object? DeserializePropertyValue(object? serializedValue)
        {
            string? stringValue = serializedValue?.ToString();
            return propertyDeserializer(stringValue);
        }
    }
}
