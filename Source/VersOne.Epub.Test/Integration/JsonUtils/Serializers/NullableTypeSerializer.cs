using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class NullableTypeSerializer : TypeSerializer
    {
        private readonly Lazy<TypeSerializer> underlyingTypeSerializer;

        public NullableTypeSerializer(Type nullableType, TypeSerializerCollection typeSerializerCollection)
        {
            Type? underlyingType = Nullable.GetUnderlyingType(nullableType);
            Assert.NotNull(underlyingType);
            underlyingTypeSerializer = new Lazy<TypeSerializer>(() => typeSerializerCollection.GetSerializer(underlyingType));
        }

        public override JsonNode? Serialize(object? value, JsonSerializationContext? testCasesSerializationContext)
        {
            return underlyingTypeSerializer.Value.Serialize(value, testCasesSerializationContext);
        }
    }
}
