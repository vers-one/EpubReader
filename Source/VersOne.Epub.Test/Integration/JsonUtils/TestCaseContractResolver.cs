using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseContractResolver : DefaultContractResolver
    {
        private readonly TestCaseExtensionDataHandler testCaseExtensionDataHandler;

        public TestCaseContractResolver(TestCaseExtensionDataHandler testCaseExtensionDataHandler)
        {
            this.testCaseExtensionDataHandler = testCaseExtensionDataHandler;
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            List<JsonProperty> result = new();
            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                JsonProperty jsonProperty = CreateProperty(propertyInfo, memberSerialization);
                jsonProperty.Writable = true;
                result.Add(jsonProperty);
            }
            return result;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract result = base.CreateObjectContract(objectType);
            if (objectType == typeof(EpubBook))
            {
                result.Properties.Remove(nameof(EpubBook.CoverImage));
                result.Properties.Remove(nameof(EpubBook.FilePath));
                result.ExtensionDataGetter = new ExtensionDataGetter(testCaseExtensionDataHandler.GetEpubBookExtensionData);
                result.ExtensionDataSetter = new ExtensionDataSetter(testCaseExtensionDataHandler.SetEpubBookExtensionData);
            }
            else if (objectType == typeof(EpubLocalByteContentFile))
            {
                result.Properties.Remove(nameof(EpubLocalByteContentFile.Content));
                result.ExtensionDataGetter = new ExtensionDataGetter(testCaseExtensionDataHandler.GetEpubLocalContentFileExtensionData);
                result.ExtensionDataSetter = new ExtensionDataSetter(testCaseExtensionDataHandler.SetEpubLocalByteContentFileExtensionData);
            }
            else if (objectType == typeof(EpubLocalTextContentFile))
            {
                result.Properties.Remove(nameof(EpubLocalTextContentFile.Content));
                result.ExtensionDataGetter = new ExtensionDataGetter(testCaseExtensionDataHandler.GetEpubLocalContentFileExtensionData);
                result.ExtensionDataSetter = new ExtensionDataSetter(testCaseExtensionDataHandler.SetEpubLocalTextContentFileExtensionData);
            }
            return result;
        }
    }
}
