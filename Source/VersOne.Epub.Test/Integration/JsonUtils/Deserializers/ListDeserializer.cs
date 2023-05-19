using System.Collections;
using System.Collections.ObjectModel;
using System.Text.Json;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class ListDeserializer : TypeDeserializer
    {
        private readonly Type listType;
        private readonly bool isReadOnlyCollection;
        private readonly Type listItemType;
        private readonly Lazy<TypeDeserializer> listItemTypeDeserializer;

        public ListDeserializer(Type listType, TypeDeserializerCollection typeDeserializerCollection)
        {
            if (!listType.IsGenericType)
            {
                throw new ArgumentException($"{listType.Name} is not a generic List<T> type.");
            }
            this.listType = listType;
            isReadOnlyCollection = listType.GetGenericTypeDefinition() == typeof(ReadOnlyCollection<>);
            listItemType = listType.GetGenericArguments().First();
            listItemTypeDeserializer = new Lazy<TypeDeserializer>(() => typeDeserializerCollection.GetDeserializer(listItemType));
        }

        public override object? Deserialize(JsonElement jsonElement, JsonSerializationContext jsonSerializationContext)
        {
            Assert.Equal(JsonValueKind.Array, jsonElement.ValueKind);
            IList? list = Activator.CreateInstance(isReadOnlyCollection ? typeof(List<>).MakeGenericType(listItemType) : listType,
                new object[] { jsonElement.GetArrayLength() }) as IList;
            Assert.NotNull(list);
            foreach (JsonElement serializedListItem in jsonElement.EnumerateArray())
            {
                list.Add(listItemTypeDeserializer.Value.Deserialize(serializedListItem, jsonSerializationContext));
            }
            object? result = isReadOnlyCollection ? Activator.CreateInstance(listType, new object[] { list }) : list;
            return result;
        }
    }
}
