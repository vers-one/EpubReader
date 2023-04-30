using System.Collections;
using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class ListSerializer : TypeSerializer
    {
        private readonly Lazy<TypeSerializer> declaredListItemTypeSerializer;

        public ListSerializer(Type listType, TypeSerializerCollection typeSerializerCollection)
        {
            Type declaredListItemType = listType.IsGenericType ? listType.GetGenericArguments().First() : throw new ArgumentException($"{listType.Name} is not a generic List<T> type.");
            declaredListItemTypeSerializer = new Lazy<TypeSerializer>(() => typeSerializerCollection.GetSerializer(declaredListItemType));
        }

        public override JsonNode? Serialize(object? value, JsonSerializationContext jsonSerializationContext)
        {
            if (value is not IList list)
            {
                return null;
            }
            JsonArray array = new();
            foreach (object? listItem in list)
            {
                if (listItem == null)
                {
                    array.Add(null);
                }
                else
                {
                    array.Add(declaredListItemTypeSerializer.Value.Serialize(listItem, jsonSerializationContext));
                }
            }
            return array;
        }
    }
}
