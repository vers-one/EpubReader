using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class NullableTypeDeserializer : TypeDeserializer
    {
        private readonly Lazy<TypeDeserializer> underlyingTypeDeserializer;

        public NullableTypeDeserializer(Type nullableType, TypeDeserializerCollection typeDeserializerCollection)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(nullableType);
            Assert.NotNull(underlyingType);
            underlyingTypeDeserializer = new Lazy<TypeDeserializer>(() => typeDeserializerCollection.GetDeserializer(underlyingType));
        }

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext)
        {
            return underlyingTypeDeserializer.Value.Deserialize(jsonElement, jsonSerializationContext);
        }
    }
}
