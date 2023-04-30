using System.Collections;
using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class DictionarySerializer : TypeSerializer
    {
        private readonly Lazy<TypeSerializer> declaredValueTypeSerializer;

        public DictionarySerializer(Type dictionaryType, TypeSerializerCollection typeSerializerCollection)
        {
            Type declaredValueType = dictionaryType.IsGenericType ?
                dictionaryType.GetGenericArguments()[1] : throw new ArgumentException($"{dictionaryType.Name} is not a generic Dictionary<K,V> type.");
            declaredValueTypeSerializer = new Lazy<TypeSerializer>(() => typeSerializerCollection.GetSerializer(declaredValueType));
        }

        public override JsonNode? Serialize(object? value, JsonSerializationContext jsonSerializationContext)
        {
            if (value is not IDictionary dictionary)
            {
                return null;
            }
            JsonObject dictionaryObject = new();
            foreach (DictionaryEntry dictionaryItem in dictionary)
            {
                if (dictionaryItem.Key is not string key)
                {
                    throw new ArgumentException($"Expected string dictionary key type but got {dictionaryItem.Key.GetType().Name}.");
                }
                JsonNode? serializedValue;
                if (dictionaryItem.Value == null)
                {
                    serializedValue = null;
                }
                else
                {
                    serializedValue = declaredValueTypeSerializer.Value.Serialize(dictionaryItem.Value, jsonSerializationContext);
                }
                dictionaryObject.Add(key, serializedValue);
            }
            return dictionaryObject;
        }
    }
}
