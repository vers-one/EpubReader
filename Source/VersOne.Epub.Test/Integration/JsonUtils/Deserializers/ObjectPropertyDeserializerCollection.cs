namespace VersOne.Epub.Test.Integration.JsonUtils.Deserializers
{
    internal class ObjectPropertyDeserializerCollection
    {
        public ObjectPropertyDeserializerCollection()
        {
            ConstructorParameterDeserializers = [];
            StandalonePropertyDeserializers = [];
        }

        public List<PropertyDeserializer> ConstructorParameterDeserializers { get; }
        public List<PropertyDeserializer> StandalonePropertyDeserializers { get; }
    }
}
