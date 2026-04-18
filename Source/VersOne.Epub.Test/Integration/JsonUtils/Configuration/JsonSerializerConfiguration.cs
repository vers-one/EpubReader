namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    internal class JsonSerializerConfiguration(IEnumerable<CustomType> customTypes)
    {
        private readonly Dictionary<Type, CustomType> customTypes = customTypes.ToDictionary(customType => customType.Type);

        public CustomType? GetCustomType(Type type)
        {
            customTypes.TryGetValue(type, out CustomType? result);
            return result;
        }
    }
}
