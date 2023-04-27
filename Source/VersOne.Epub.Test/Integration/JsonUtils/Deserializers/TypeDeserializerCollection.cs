using System.Collections;
using System.Collections.Concurrent;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;

namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class TypeDeserializerCollection
    {
        private readonly JsonSerializerConfiguration? jsonSerializerConfiguration;
        private readonly ConcurrentDictionary<Type, TypeDeserializer> deserializers;
        private readonly Lazy<TypeDeserializer> literalTypeDeserializer;

        public TypeDeserializerCollection(JsonSerializerConfiguration? jsonSerializerConfiguration)
        {
            this.jsonSerializerConfiguration = jsonSerializerConfiguration;
            deserializers = new ConcurrentDictionary<Type, TypeDeserializer>();
            literalTypeDeserializer = new Lazy<TypeDeserializer>(() => new LiteralTypeDeserializer());
        }

        public TypeDeserializer GetDeserializer(Type type)
        {
            return deserializers.GetOrAdd(type, CreateDeserializer(type));
        }

        private TypeDeserializer CreateDeserializer(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return new NullableTypeDeserializer(type, this);
            }
            else if (type.IsValueType || type == typeof(string))
            {
                if (type.IsEnum)
                {
                    return new EnumDeserializer(type);
                }
                else
                {
                    return literalTypeDeserializer.Value;
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (typeof(IDictionary).IsAssignableFrom(type))
                {
                    return new DictionaryDeserializer(type, this);
                }
                else
                {
                    return new ListDeserializer(type, this);
                }
            }
            else
            {
                return new ObjectDeserializer(type, jsonSerializerConfiguration, this);
            }
        }
    }
}
