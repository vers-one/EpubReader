using System.Reflection;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using VersOne.Epub.Test.Integration.CustomTypeHandlers;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Test.Integration.JsonUtils
{
    internal class TestCaseContractResolver : DefaultContractResolver
    {
        private readonly List<ICustomTypeHandler> customTypeHandlers;

        public TestCaseContractResolver(CustomPropertyDependencies customPropertyDependencies)
        {
            customTypeHandlers = new List<ICustomTypeHandler>()
            {
                new EpubBookTypeHandler(customPropertyDependencies),
                new EpubLocalByteContentFileTypeHandler(customPropertyDependencies),
                new EpubLocalTextContentFileTypeHandler(customPropertyDependencies),
                new EpubRemoteByteContentFileTypeHandler(customPropertyDependencies),
                new EpubRemoteTextContentFileTypeHandler(customPropertyDependencies)
            };
        }

        protected override IList<JsonProperty> CreateProperties(Type type, MemberSerialization memberSerialization)
        {
            List<JsonProperty> result = new();
            ParameterInfo[] constructorParameters = !type.IsAbstract ? GetConstructorWithMostParameters(type).GetParameters() : Array.Empty<ParameterInfo>();
            foreach (PropertyInfo propertyInfo in type.GetProperties(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
            {
                if (constructorParameters.Length > 0 && !constructorParameters.Any(parameter => parameter.Name.CompareOrdinalIgnoreCase(propertyInfo.Name)))
                {
                    continue;
                }
                JsonProperty jsonProperty = CreateProperty(propertyInfo, memberSerialization);
                result.Add(jsonProperty);
            }
            return result;
        }

        protected override JsonObjectContract CreateObjectContract(Type objectType)
        {
            JsonObjectContract result = base.CreateObjectContract(objectType);
            ICustomTypeHandler? customTypeHandler = customTypeHandlers.FirstOrDefault(customTypeHandler => customTypeHandler.Type == objectType);
            if (customTypeHandler != null)
            {
                foreach (ICustomPropertyHandler propertyHandler in customTypeHandler.CustomPropertyHandlers)
                {
                    result.Properties.Remove(propertyHandler.TypePropertyName);
                }
                result.ExtensionDataGetter = CreateExtensionDataGetter(customTypeHandler);
                ConstructorInfo constructorWithMostParameters = GetConstructorWithMostParameters(objectType);
                result.OverrideCreator = CreateOverrideCreator(constructorWithMostParameters, customTypeHandler);
                ReplaceCreatorParameters(result, GetCustomTypeConstructorParameters(CreateConstructorParameters(constructorWithMostParameters, result.Properties), customTypeHandler));
            }
            else if (!objectType.IsAbstract)
            {
                ConstructorInfo constructorWithMostParameters = GetConstructorWithMostParameters(objectType);
                result.OverrideCreator = constructorWithMostParameters.Invoke;
                ReplaceCreatorParameters(result, CreateConstructorParameters(constructorWithMostParameters, result.Properties));
            }
            return result;
        }

        private static ExtensionDataGetter CreateExtensionDataGetter(ICustomTypeHandler customTypeHandler)
        {
            return new((object serializingObject) =>
            {
                List<KeyValuePair<object, object>> result = new();
                foreach (ICustomPropertyHandler propertyHandler in customTypeHandler.CustomPropertyHandlers)
                {
                    object? serializedPropertyValue = propertyHandler.SerializePropertyValue(serializingObject);
                    if (serializedPropertyValue != null)
                    {
                        result.Add(new KeyValuePair<object, object>(propertyHandler.JsonPropertyName, serializedPropertyValue));
                    }
                }
                return result;
            });
        }

        private static ObjectConstructor<object> CreateOverrideCreator(ConstructorInfo typeConstructor, ICustomTypeHandler customTypeHandler)
        {
            List<Func<object?, object?>?> parameterConverters = new();
            foreach (ParameterInfo parameterInfo in typeConstructor.GetParameters())
            {
                ICustomPropertyHandler? customPropertyHandler = customTypeHandler.CustomPropertyHandlers.
                    FirstOrDefault(propertyHandler => propertyHandler.ConstructorParameterName == parameterInfo.Name);
                if (customPropertyHandler != null)
                {
                    parameterConverters.Add(customPropertyHandler.DeserializePropertyValue);
                }
                else
                {
                    parameterConverters.Add(null);
                }
            }
            return parameters =>
            {
                Assert.Equal(parameterConverters.Count, parameters.Length);
                for (int i = 0; i < parameters.Length; i++)
                {
                    if (parameters[i] != null)
                    {
                        Func<object?, object?>? parameterConverter = parameterConverters[i];
                        if (parameterConverter != null)
                        {
                            parameters[i] = parameterConverter(parameters[i]);
                        }
                    }
                }
                return typeConstructor.Invoke(parameters);
            };
        }

        private static List<JsonProperty> GetCustomTypeConstructorParameters(IList<JsonProperty> regularTypeConstructorParameters, ICustomTypeHandler customTypeHandler)
        {
            List<JsonProperty> result = new();
            foreach (JsonProperty parameterInfo in regularTypeConstructorParameters)
            {
                ICustomPropertyHandler? customPropertyHandler = customTypeHandler.CustomPropertyHandlers.
                    FirstOrDefault(propertyHandler => propertyHandler.ConstructorParameterName == parameterInfo.PropertyName);
                if (customPropertyHandler != null)
                {
                    parameterInfo.PropertyName = customPropertyHandler.JsonPropertyName;
                    parameterInfo.PropertyType = typeof(string);
                }
                result.Add(parameterInfo);
            }
            return result;
        }

        private static ConstructorInfo GetConstructorWithMostParameters(Type type)
        {
            ConstructorInfo? result = type.GetConstructors(BindingFlags.Instance | BindingFlags.Public).
                OrderByDescending(constructor => constructor.GetParameters().Length).
                FirstOrDefault();
            Assert.NotNull(result);
            return result;
        }

        private static void ReplaceCreatorParameters(JsonObjectContract jsonObjectContract, IList<JsonProperty> newCreatorParameters)
        {
            jsonObjectContract.CreatorParameters.Clear();
            foreach (JsonProperty creatorParameter in newCreatorParameters)
            {
                jsonObjectContract.CreatorParameters.Add(creatorParameter);
            }
        }
    }
}
