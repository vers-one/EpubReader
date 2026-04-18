using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class EnumDeserializer(Type enumType) : TypeDeserializer
    {
        private readonly Type enumType = enumType;

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext _)
        {
            Assert.Equal(JsonValueKind.String, jsonElement.ValueKind);
            string? stringValue = jsonElement.GetString();
            Assert.NotNull(stringValue);
            if (!Enum.TryParse(enumType, stringValue, out object? result))
            {
                throw new ArgumentException($"{stringValue} is not defined in {enumType.Name}.");
            }
            return result;
        }
    }
}
