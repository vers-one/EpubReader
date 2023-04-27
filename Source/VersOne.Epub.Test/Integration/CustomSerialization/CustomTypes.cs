using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Integration.CustomSerialization.TypeSerializers;
using VersOne.Epub.Test.Integration.JsonUtils.Configuration;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.CustomSerialization
{
    internal static class CustomTypes
    {
        static CustomTypes()
        {
            Types = CreateTypes().ToDictionary(type => type.Type);
            foreach (CustomTypeSerializer customTypeSerializer in CustomTypeSerializers.TypeSerializers.Values)
            {
                if (!Types.TryGetValue(customTypeSerializer.Type, out CustomType? customType))
                {
                    customType = new CustomType(customTypeSerializer.Type);
                    Types.Add(customTypeSerializer.Type, customType);
                }
                foreach (CustomPropertySerializer customPropertySerializer in customTypeSerializer.CustomPropertySerializers.Values)
                {
                    string propertyName = customPropertySerializer.TypePropertyName;
                    if (customType.CustomProperties.ContainsKey(customPropertySerializer.TypePropertyName))
                    {
                        throw new InvalidOperationException(
                            $"Custom property {customPropertySerializer.TypePropertyName} has already been added to the custom type {customType.Type.Name}.");
                    }
                    else
                    {
                        customType.AddPropertyWithCustomSerialization(customPropertySerializer.TypePropertyName, customPropertySerializer.JsonPropertyName);
                    }
                }
            }
        }

        public static Dictionary<Type, CustomType> Types { get; }

        private static IEnumerable<CustomType> CreateTypes()
        {
            yield return CreateType<TestCase>
            (
                optionalProperties: new()
                {
                    { nameof(TestCase.ExpectedException), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<TestCaseException>
            (
                optionalProperties: new()
                {
                    { nameof(TestCaseException.Message), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubBook>
            (
                optionalProperties: new()
                {
                    { nameof(EpubBook.AuthorList), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubBook.Description), PropertyDefaultValue.NULL },
                    { nameof(EpubBook.ReadingOrder), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubBook.Navigation), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubNavigationItem>
            (
                optionalProperties: new()
                {
                    { nameof(EpubNavigationItem.Link), PropertyDefaultValue.NULL },
                    { nameof(EpubNavigationItem.HtmlContentFile), PropertyDefaultValue.NULL },
                    { nameof(EpubNavigationItem.NestedItems), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubSchema>
            (
                optionalProperties: new()
                {
                    { nameof(EpubSchema.Epub2Ncx), PropertyDefaultValue.NULL },
                    { nameof(EpubSchema.Epub3NavDocument), PropertyDefaultValue.NULL },
                    { nameof(EpubSchema.MediaOverlays), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubPackage>
            (
                optionalProperties: new()
                {
                    { nameof(EpubPackage.Guide), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubMetadata>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadata.Titles), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Creators), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Subjects), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Description), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadata.Publishers), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Contributors), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Dates), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Types), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Formats), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Identifiers), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Sources), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Languages), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Relations), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Coverages), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Rights), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.Links), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(EpubMetadata.MetaItems), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubMetadataCreator>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataCreator.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataCreator.FileAs), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataCreator.Role), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubMetadataContributor>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataContributor.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataContributor.FileAs), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataContributor.Role), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubMetadataDate>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataDate.Event), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubMetadataIdentifier>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataIdentifier.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataIdentifier.Scheme), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubMetadataLink>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataLink.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.MediaType), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Properties), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Refines), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Relationships), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubMetadataMeta>
            (
                optionalProperties: new()
                {
                    { nameof(EpubMetadataMeta.Name), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Refines), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Property), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Scheme), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubManifest>
            (
                optionalProperties: new()
                {
                    { nameof(EpubManifest.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubManifestItem>
            (
                optionalProperties: new()
                {
                    { nameof(EpubManifestItem.MediaOverlay), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.RequiredNamespace), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.RequiredModules), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.Fallback), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.FallbackStyle), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.Properties), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubSpine>
            (
                optionalProperties: new()
                {
                    { nameof(EpubSpine.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.PageProgressionDirection), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.Toc), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubGuide>
            (
                optionalProperties: new()
                {
                    { nameof(EpubGuide.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<EpubGuideReference>
            (
                optionalProperties: new()
                {
                    { nameof(EpubGuideReference.Title), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub2Ncx>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2Ncx.DocTitle), PropertyDefaultValue.NULL },
                    { nameof(Epub2Ncx.DocAuthors), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2Ncx.PageList), PropertyDefaultValue.NULL },
                    { nameof(Epub2Ncx.NavLists), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxHead>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxHead.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxHeadMeta>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxHeadMeta.Scheme), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub2NcxNavigationMap>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxNavigationMap.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxNavigationPoint>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxNavigationPoint.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationPoint.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationPoint.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationPoint.ChildNavigationPoints), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxContent>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxContent.Id), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub2NcxPageList>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxPageList.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxPageTarget>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxPageTarget.Id), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.Value), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxPageTarget.Content), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub2NcxNavigationList>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxNavigationList.Id), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationList.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationList.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationList.NavigationTargets), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub2NcxNavigationTarget>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxNavigationTarget.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.Value), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationTarget.Content), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub3NavDocument>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3NavDocument.Navs), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub3Nav>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3Nav.Type), PropertyDefaultValue.NULL },
                    { nameof(Epub3Nav.IsHidden), PropertyDefaultValue.FALSE },
                    { nameof(Epub3Nav.Head), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub3NavOl>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3NavOl.IsHidden), PropertyDefaultValue.FALSE },
                    { nameof(Epub3NavOl.Lis), PropertyDefaultValue.EMPTY_ARRAY }
                }
            );
            yield return CreateType<Epub3NavLi>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3NavLi.Anchor), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavLi.Span), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavLi.ChildOl), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub3NavAnchor>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3NavAnchor.Href), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Title), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Alt), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Type), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub3NavSpan>
            (
                optionalProperties: new()
                {
                    { nameof(Epub3NavSpan.Title), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavSpan.Alt), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<EpubLocalByteContentFile>
            (
                preserveReferences: true,
                ignoredProperties: new()
                {
                    nameof(EpubLocalByteContentFile.ContentFileType),
                    nameof(EpubLocalByteContentFile.ContentLocation)
                }
            );
            yield return CreateType<EpubLocalTextContentFile>
            (
                preserveReferences: true,
                ignoredProperties: new()
                {
                    nameof(EpubLocalTextContentFile.ContentFileType),
                    nameof(EpubLocalTextContentFile.ContentLocation)
                }
            );
            yield return CreateType<EpubRemoteByteContentFile>
            (
                preserveReferences: true,
                ignoredProperties: new()
                {
                    nameof(EpubRemoteByteContentFile.ContentFileType),
                    nameof(EpubRemoteByteContentFile.ContentLocation)
                }
            );
            yield return CreateType<EpubRemoteTextContentFile>
            (
                preserveReferences: true,
                ignoredProperties: new()
                {
                    nameof(EpubRemoteTextContentFile.ContentFileType),
                    nameof(EpubRemoteTextContentFile.ContentLocation)
                }
            );
            yield return CreateType<EpubReaderOptions>
            (
                optionalProperties: new()
                {
                    { nameof(EpubReaderOptions.PackageReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.ContentReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.ContentDownloaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.Epub2NcxReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.XmlReaderOptions), PropertyDefaultValue.EMPTY_OBJECT }
                }
            );
            yield return CreateType<PackageReaderOptions>
            (
                optionalProperties: new()
                {
                    { nameof(PackageReaderOptions.IgnoreMissingToc), PropertyDefaultValue.FALSE },
                    { nameof(PackageReaderOptions.SkipInvalidManifestItems), PropertyDefaultValue.FALSE }
                }
            );
            yield return CreateType<ContentDownloaderOptions>
            (
                optionalProperties: new()
                {
                    { nameof(ContentDownloaderOptions.DownloadContent), PropertyDefaultValue.FALSE },
                    { nameof(ContentDownloaderOptions.DownloaderUserAgent), PropertyDefaultValue.NULL },
                    { nameof(ContentDownloaderOptions.CustomContentDownloader), PropertyDefaultValue.NULL }
                }
            );
            yield return CreateType<Epub2NcxReaderOptions>
            (
                optionalProperties: new()
                {
                    { nameof(Epub2NcxReaderOptions.IgnoreMissingContentForNavigationPoints), PropertyDefaultValue.FALSE }
                }
            );
            yield return CreateType<XmlReaderOptions>
            (
                optionalProperties: new()
                {
                    { nameof(XmlReaderOptions.SkipXmlHeaders), PropertyDefaultValue.FALSE }
                }
            );
        }

        private static CustomType CreateType<T>(bool preserveReferences = false, Dictionary<string, PropertyDefaultValue>? optionalProperties = null,
            List<string>? ignoredProperties = null)
            where T : class
        {
            CustomType result = new(typeof(T));
            result.PreserveReferences = preserveReferences;
            if (optionalProperties != null)
            {
                foreach (KeyValuePair<string, PropertyDefaultValue> optionalProperty in optionalProperties)
                {
                    result.AddOptionalProperty(optionalProperty.Key, optionalProperty.Value);
                }
            }
            if (ignoredProperties != null)
            {
                foreach (string ignoredProperty in ignoredProperties)
                {
                    result.AddIgnoredProperty(ignoredProperty);
                }
            }
            return result;
        }
    }
}
