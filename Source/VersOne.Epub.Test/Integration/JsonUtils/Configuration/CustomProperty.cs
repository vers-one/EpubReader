namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    internal class CustomProperty
    {
        public CustomProperty(string typePropertyName, string? jsonPropertyName = null)
        {
            TypePropertyName = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
            JsonPropertyName = jsonPropertyName ?? typePropertyName;
            UsesCustomSerialization = false;
            IsIgnored = false;
            OptionalPropertyValue = null;
        }

        public string TypePropertyName { get; }
        public string JsonPropertyName { get; }
        public bool UsesCustomSerialization { get; set; }
        public bool IsIgnored { get; set; }
        public PropertyDefaultValue? OptionalPropertyValue { get; set; }
    }
}
