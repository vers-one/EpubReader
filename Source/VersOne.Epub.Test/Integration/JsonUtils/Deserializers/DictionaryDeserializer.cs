using System.Collections;
using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class DictionaryDeserializer : TypeDeserializer
    {
        private readonly Type dictionaryType;
        private readonly Type dictionaryValueType;
        private readonly Lazy<TypeDeserializer> dictionaryValueTypeDeserializer;

        public DictionaryDeserializer(Type dictionaryType, TypeDeserializerCollection typeDeserializerCollection)
        {
            this.dictionaryType = dictionaryType;
            if (!dictionaryType.IsGenericType || dictionaryType.GenericTypeArguments.Length != 2)
            {
                throw new ArgumentException($"{dictionaryType.Name} is not a generic Dictionary<K,V> type.");
            }
            Type dictionaryKeyType = dictionaryType.GenericTypeArguments[0];
            if (dictionaryKeyType != typeof(string))
            {
                throw new ArgumentException($"Expected string dictionary key type but got {dictionaryKeyType.Name}.");
            }
            dictionaryValueType = dictionaryType.GenericTypeArguments[1];
            dictionaryValueTypeDeserializer = new Lazy<TypeDeserializer>(() => typeDeserializerCollection.GetDeserializer(dictionaryValueType));
        }

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext)
        {
            Assert.Equal(JsonValueKind.Object, jsonElement.ValueKind);
            IDictionary? result = Activator.CreateInstance(dictionaryType) as IDictionary;
            Assert.NotNull(result);
            foreach (JsonProperty serializedDictionaryItem in jsonElement.EnumerateObject())
            {
                result.Add(serializedDictionaryItem.Name, dictionaryValueTypeDeserializer.Value.Deserialize(serializedDictionaryItem.Value, jsonSerializationContext));
            }
            return result;
        }
    }
}
