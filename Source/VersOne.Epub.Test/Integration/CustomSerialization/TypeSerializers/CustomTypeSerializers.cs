namespace VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers
{
    internal static class CustomTypeSerializers
    {
        static CustomTypeSerializers()
        {
            TypeSerializers = new CustomTypeSerializer[]
            {
                new EpubBookTypeSerializer(),
                new EpubLocalByteContentFileTypeSerializer(),
                new EpubLocalTextContentFileTypeSerializer(),
                new EpubRemoteByteContentFileTypeSerializer(),
                new EpubRemoteTextContentFileTypeSerializer()
            }.ToDictionary(customTypeSerializer => customTypeSerializer.Type);
        }

        public static Dictionary<Type, CustomTypeSerializer> TypeSerializers { get; }
    }
}
