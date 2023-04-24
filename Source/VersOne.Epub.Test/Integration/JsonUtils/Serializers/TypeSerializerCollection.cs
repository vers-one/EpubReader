using System.Collections;
using System.Collections.Concurrent;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;

namespace VersOne.Epub.Test.Integration.JsonUtils.Serializers
{
    internal class TypeSerializerCollection
    {
        private readonly JsonSerializerConfiguration? jsonSerializerConfiguration;
        private readonly ConcurrentDictionary<Type, TypeSerializer> serializers;
        private readonly Lazy<TypeSerializer> literalTypeSerializer;
        private readonly Lazy<TypeSerializer> enumSerializer;

        public TypeSerializerCollection(JsonSerializerConfiguration? jsonSerializerConfiguration)
        {
            this.jsonSerializerConfiguration = jsonSerializerConfiguration;
            serializers = new ConcurrentDictionary<Type, TypeSerializer>();
            literalTypeSerializer = new Lazy<TypeSerializer>(() => new LiteralTypeSerializer());
            enumSerializer = new Lazy<TypeSerializer>(() => new EnumSerializer());
        }

        public TypeSerializer GetSerializer(Type type)
        {
            return serializers.GetOrAdd(type, CreateSerializer(type));
        }

        private TypeSerializer CreateSerializer(Type type)
        {
            if (Nullable.GetUnderlyingType(type) != null)
            {
                return new NullableTypeSerializer(type, this);
            }
            else if (type.IsValueType || type == typeof(string))
            {
                if (type.IsEnum)
                {
                    return enumSerializer.Value;
                }
                else
                {
                    return literalTypeSerializer.Value;
                }
            }
            else if (typeof(IEnumerable).IsAssignableFrom(type))
            {
                if (typeof(IDictionary).IsAssignableFrom(type))
                {
                    return new DictionarySerializer(type, this);
                }
                else
                {
                    return new ListSerializer(type, this);
                }
            }
            else
            {
                return new ObjectSerializer(type, jsonSerializerConfiguration, this);
            }
        }
    }
}
