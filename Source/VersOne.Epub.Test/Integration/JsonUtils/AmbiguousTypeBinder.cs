using Newtonsoft.Json.Serialization;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    public class AmbiguousTypeBinder : ISerializationBinder
    {
        private readonly List<Type> ambiguousTypes;

        public AmbiguousTypeBinder()
        {
            ambiguousTypes = new List<Type>
            {
                typeof(EpubLocalTextContentFile),
                typeof(EpubLocalByteContentFile),
                typeof(EpubRemoteTextContentFile),
                typeof(EpubRemoteByteContentFile)
            };
        }

        public Type BindToType(string? assemblyName, string typeName)
        {
            Type? result = ambiguousTypes.SingleOrDefault(t => t.Name == typeName);
            Assert.NotNull(result);
            return result;
        }

        public void BindToName(Type serializedType, out string? assemblyName, out string? typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}
