using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Test.Integration.Types;

namespace VersOne.Epub.Test.Integration.CustomTypeHandlers
{
    internal class TypesWithOptionalProperties
    {
        internal class TypeWithOptionalProperties<T> : CustomTypeHandler<T> where T : class
        {
            public TypeWithOptionalProperties(TestCasesSerializationContext testCasesSerializationContext, Dictionary<string, PropertyDefaultValue> optionalProperties)
                : base(testCasesSerializationContext, optionalProperties)
            {
            }

            public override bool PreserveReferences => false;
        }

        private readonly TestCasesSerializationContext testCasesSerializationContext;

        public TypesWithOptionalProperties(TestCasesSerializationContext testCasesSerializationContext)
        {
            this.testCasesSerializationContext = testCasesSerializationContext;
            Types = new List<ICustomTypeHandler>()
            {
                CreateType<TestCase>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(TestCase.ExpectedException), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubNavigationItem>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubNavigationItem.Link), PropertyDefaultValue.NULL },
                    { nameof(EpubNavigationItem.HtmlContentFile), PropertyDefaultValue.NULL },
                    { nameof(EpubNavigationItem.NestedItems), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubSchema>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubSchema.Epub2Ncx), PropertyDefaultValue.NULL },
                    { nameof(EpubSchema.Epub3NavDocument), PropertyDefaultValue.NULL },
                    { nameof(EpubSchema.MediaOverlays), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubPackage>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubPackage.Guide), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubMetadata>(new Dictionary<string, PropertyDefaultValue>()
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
                }),
                CreateType<EpubMetadataCreator>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataCreator.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataCreator.FileAs), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataCreator.Role), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubMetadataContributor>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataContributor.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataContributor.FileAs), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataContributor.Role), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubMetadataDate>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataDate.Event), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubMetadataIdentifier>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataIdentifier.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataIdentifier.Scheme), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubMetadataLink>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataLink.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.MediaType), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Properties), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Refines), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataLink.Relationships), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubMetadataMeta>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubMetadataMeta.Name), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Refines), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Property), PropertyDefaultValue.NULL },
                    { nameof(EpubMetadataMeta.Scheme), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubManifest>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubManifest.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubManifestItem>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubManifestItem.MediaOverlay), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.RequiredNamespace), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.RequiredModules), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.Fallback), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.FallbackStyle), PropertyDefaultValue.NULL },
                    { nameof(EpubManifestItem.Properties), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubSpine>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubSpine.Id), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.PageProgressionDirection), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.Toc), PropertyDefaultValue.NULL },
                    { nameof(EpubSpine.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubGuide>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubGuide.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<EpubGuideReference>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubGuideReference.Title), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub2Ncx>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2Ncx.DocTitle), PropertyDefaultValue.NULL },
                    { nameof(Epub2Ncx.DocAuthors), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2Ncx.PageList), PropertyDefaultValue.NULL },
                    { nameof(Epub2Ncx.NavLists), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxHead>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxHead.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxHeadMeta>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxHeadMeta.Scheme), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub2NcxNavigationMap>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxNavigationMap.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxNavigationPoint>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxNavigationPoint.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationPoint.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationPoint.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationPoint.ChildNavigationPoints), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxContent>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxContent.Id), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub2NcxPageList>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxPageList.Items), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxPageTarget>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxPageTarget.Id), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.Value), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxPageTarget.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxPageTarget.Content), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub2NcxNavigationList>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxNavigationList.Id), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationList.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationList.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationList.NavigationTargets), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub2NcxNavigationTarget>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxNavigationTarget.Class), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.Value), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.PlayOrder), PropertyDefaultValue.NULL },
                    { nameof(Epub2NcxNavigationTarget.NavigationLabels), PropertyDefaultValue.EMPTY_ARRAY },
                    { nameof(Epub2NcxNavigationTarget.Content), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub3NavDocument>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3NavDocument.Navs), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub3Nav>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3Nav.Type), PropertyDefaultValue.NULL },
                    { nameof(Epub3Nav.IsHidden), PropertyDefaultValue.FALSE },
                    { nameof(Epub3Nav.Head), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub3NavOl>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3NavOl.IsHidden), PropertyDefaultValue.FALSE },
                    { nameof(Epub3NavOl.Lis), PropertyDefaultValue.EMPTY_ARRAY }
                }),
                CreateType<Epub3NavLi>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3NavLi.Anchor), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavLi.Span), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavLi.ChildOl), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub3NavAnchor>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3NavAnchor.Href), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Title), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Alt), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavAnchor.Type), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub3NavSpan>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub3NavSpan.Title), PropertyDefaultValue.NULL },
                    { nameof(Epub3NavSpan.Alt), PropertyDefaultValue.NULL }
                }),
                CreateType<EpubReaderOptions>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(EpubReaderOptions.PackageReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.ContentReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.ContentDownloaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.Epub2NcxReaderOptions), PropertyDefaultValue.EMPTY_OBJECT },
                    { nameof(EpubReaderOptions.XmlReaderOptions), PropertyDefaultValue.EMPTY_OBJECT }
                }),
                CreateType<PackageReaderOptions>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(PackageReaderOptions.IgnoreMissingToc), PropertyDefaultValue.FALSE },
                    { nameof(PackageReaderOptions.SkipInvalidManifestItems), PropertyDefaultValue.FALSE }
                }),
                CreateType<ContentDownloaderOptions>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(ContentDownloaderOptions.DownloadContent), PropertyDefaultValue.FALSE },
                    { nameof(ContentDownloaderOptions.DownloaderUserAgent), PropertyDefaultValue.NULL },
                    { nameof(ContentDownloaderOptions.CustomContentDownloader), PropertyDefaultValue.NULL }
                }),
                CreateType<Epub2NcxReaderOptions>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(Epub2NcxReaderOptions.IgnoreMissingContentForNavigationPoints), PropertyDefaultValue.FALSE }
                }),
                CreateType<XmlReaderOptions>(new Dictionary<string, PropertyDefaultValue>()
                {
                    { nameof(XmlReaderOptions.SkipXmlHeaders), PropertyDefaultValue.FALSE }
                })
            };
        }

        public List<ICustomTypeHandler> Types { get; }

        private TypeWithOptionalProperties<T> CreateType<T>(Dictionary<string, PropertyDefaultValue> optionalProperties) where T : class
        {
            return new TypeWithOptionalProperties<T>(testCasesSerializationContext, optionalProperties);
        }
    }
}
