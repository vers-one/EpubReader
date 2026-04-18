namespace VersOne.Epub.Test.Integration.JsonUtils.Configuration
{
    internal class CustomProperty(string typePropertyName, string? jsonPropertyName = null)
    {
        public string TypePropertyName { get; } = typePropertyName ?? throw new ArgumentNullException(nameof(typePropertyName));
        public string JsonPropertyName { get; } = jsonPropertyName ?? typePropertyName;
        public bool UsesCustomSerialization { get; set; } = false;
        public bool IsIgnored { get; set; } = false;
        public PropertyDefaultValue? OptionalPropertyValue { get; set; } = null;
    }
}
