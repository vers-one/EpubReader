namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    internal class JsonSerializerConfiguration
    {
        private readonly Dictionary<Type, CustomType> customTypes;

        public JsonSerializerConfiguration(IEnumerable<CustomType> customTypes)
        {
            this.customTypes = customTypes.ToDictionary(customType => customType.Type);
        }

        public CustomType? GetCustomType(Type type)
        {
            customTypes.TryGetValue(type, out CustomType? result);
            return result;
        }
    }
}
