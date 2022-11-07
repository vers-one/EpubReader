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
                typeof(EpubLocalByteContentFile),
                typeof(EpubLocalTextContentFile)
            };
        }

        public Type BindToType(string assemblyName, string typeName)
        {
            return ambiguousTypes.SingleOrDefault(t => t.Name == typeName);
        }

        public void BindToName(Type serializedType, out string assemblyName, out string typeName)
        {
            assemblyName = null;
            typeName = serializedType.Name;
        }
    }
}
