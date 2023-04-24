namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class ObjectPropertyDeserializerCollection
    {
        public ObjectPropertyDeserializerCollection()
        {
            ConstructorParameterDeserializers = new List<PropertyDeserializer>();
            StandalonePropertyDeserializers = new List<PropertyDeserializer>();
        }

        public List<PropertyDeserializer> ConstructorParameterDeserializers { get; }
        public List<PropertyDeserializer> StandalonePropertyDeserializers { get; }
    }
}
