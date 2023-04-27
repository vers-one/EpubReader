using System.Collections;
using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class ListDeserializer : TypeDeserializer
    {
        private readonly Type listType;
        private readonly Lazy<TypeDeserializer> listItemTypeDeserializer;

        public ListDeserializer(Type listType, TypeDeserializerCollection typeDeserializerCollection)
        {
            this.listType = listType;
            Type listItemType = listType.IsGenericType ? listType.GetGenericArguments().First() : throw new ArgumentException($"{listType.Name} is not a generic List<T> type.");
            listItemTypeDeserializer = new Lazy<TypeDeserializer>(() => typeDeserializerCollection.GetDeserializer(listItemType));
        }

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext? jsonSerializationContext)
        {
            Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);
            IList? result = Activator.CreateInstance(listType, new object[] { jsonElement.GetArrayLength() }) as IList;
            Assert.NotNull(result);
            foreach (JsonElement serializedListItem in jsonElement.EnumerateArray())
            {
                result.Add(listItemTypeDeserializer.Value.Deserialize(serializedListItem, jsonSerializationContext));
            }
            return result;
        }
    }
}
