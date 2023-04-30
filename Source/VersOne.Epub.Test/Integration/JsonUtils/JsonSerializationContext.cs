using System.Text.Json;
using System.Text.Json.Nodes;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class JsonSerializationContext
    {
        private readonly Lazy<Dictionary<object, int>> serializationReferencedObjects;
        private readonly Lazy<Dictionary<int, object>> deserializationReferencedObjects;

        public JsonSerializationContext()
        {
            this.serializationReferencedObjects = new Lazy<Dictionary<object, int>>();
            this.deserializationReferencedObjects = new Lazy<Dictionary<int, object>>();
        }

        public (int referenceNumber, bool isDuplicateReference) GetReferenceNumber(object reference)
        {
            if (serializationReferencedObjects.Value.TryGetValue(reference, out int existingReferenceNumber))
            {
                return (existingReferenceNumber, true);
            }
            int newReferenceNumber = serializationReferencedObjects.Value.Count + 1;
            serializationReferencedObjects.Value.Add(reference, newReferenceNumber);
            return (newReferenceNumber, false);
        }

        public void AddReference(int referenceNumber, object reference)
        {
            if (deserializationReferencedObjects.Value.ContainsKey(referenceNumber))
            {
                throw new ArgumentException($"Reference ${referenceNumber} has already been added.");
            }
            deserializationReferencedObjects.Value[referenceNumber] = reference;
        }

        public object GetExistingReference(int existingReferenceNumber)
        {
            if (!deserializationReferencedObjects.Value.TryGetValue(existingReferenceNumber, out object? existingReference))
            {
                throw new ArgumentException($"Reference ${existingReferenceNumber} does not exist.");
            }
            return existingReference;
        }

        public virtual JsonNode? SerializePropertyValue(Type type, string propertyName, object serializingObject)
        {
            throw new NotImplementedException($"Custom type serializer is required to serialize an object of type {type.Name}");
        }

        public virtual object? DeserializePropertyValue(Type type, string propertyName, JsonElement serializedValue)
        {
            throw new NotImplementedException($"Custom type deserializer is required to deserialize an object of type {type.Name}");
        }
    }
}
