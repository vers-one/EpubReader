namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal interface ICustomTypeHandler
    {
        public Type Type { get; }
        public List<ICustomPropertyHandler> CustomPropertyHandlers { get; }
    }

    internal abstract class CustomTypeHandler<T> : ICustomTypeHandler where T : class
    {
        public CustomTypeHandler(CustomPropertyDependencies customPropertyDependencies)
        {
            CustomPropertyHandlers = new List<ICustomPropertyHandler>();
            CustomPropertyDependencies = customPropertyDependencies;
        }

        public Type Type => typeof(T);

        public List<ICustomPropertyHandler> CustomPropertyHandlers { get; }
        public CustomPropertyDependencies CustomPropertyDependencies { get; }

        protected void AddCustomPropertyHandler(string typePropertyName, string jsonPropertyName, Func<T, string?> propertySerializer, Func<string?, object?> propertyDeserializer)
        {
            CustomPropertyHandlers.Add(new CustomPropertyHandler<T>(typePropertyName, jsonPropertyName, propertySerializer, propertyDeserializer));
        }
    }
}
