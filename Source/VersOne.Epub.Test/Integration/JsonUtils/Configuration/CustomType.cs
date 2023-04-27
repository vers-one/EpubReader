namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    internal class CustomType
    {
        public CustomType(Type type)
        {
            Type = type;
            PreserveReferences = false;
            CustomProperties = new Dictionary<string, CustomProperty>();
        }

        public Type Type { get; }
        public bool PreserveReferences { get; set; }
        public Dictionary<string, CustomProperty> CustomProperties { get; }

        public CustomProperty? GetCustomProperty(string propertyName)
        {
            CustomProperties.TryGetValue(propertyName, out CustomProperty? result);
            return result;
        }
        public void AddPropertyWithCustomSerialization(string typePropertyName, string jsonPropertyName)
        {
            AddCustomProperty(new(typePropertyName, jsonPropertyName)
            {
                UsesCustomSerialization = true
            });
        }

        public void AddOptionalProperty(string typePropertyName, PropertyDefaultValue propertyDefaultValue)
        {
            AddCustomProperty(new(typePropertyName)
            {
                OptionalPropertyValue = propertyDefaultValue
            });
        }

        public void AddIgnoredProperty(string typePropertyName)
        {
            AddCustomProperty(new(typePropertyName)
            {
                IsIgnored = true
            });
        }

        private void AddCustomProperty(CustomProperty customProperty)
        {
            CustomProperties.Add(customProperty.TypePropertyName, customProperty);
        }
    }
}
