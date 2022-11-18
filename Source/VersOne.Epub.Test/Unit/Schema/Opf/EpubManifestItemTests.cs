using VersOne.Epub.Schema;

namespace VersOne.Epub.Test.Unit.Schema.Opf
{
    public class EpubManifestItemTests
    {
        private const string ID = "id";
        private const string HREF = "chapter.html";
        private const string MEDIA_TYPE = "application/xhtml+xml";
        private const string MEDIA_OVERLAY = "overlay";
        private const string REQUIRED_NAMESPACE = "http://example.com/ns/example/";
        private const string REQUIRED_MODULES = "ruby, server-side-image-map";
        private const string FALLBACK = "fallback";
        private const string FALLBACK_STYLE = "fallback-style";

        private static List<EpubManifestProperty> Properties =>
            new()
            {
                EpubManifestProperty.SCRIPTED
            };

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with non-null parameters should succeed")]
        public void ConstructorWithNonNullParametersTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if id parameter is null")]
        public void ConstructorWithNullIdTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubManifestItem(null!, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if href parameter is null")]
        public void ConstructorWithNullHrefTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubManifestItem(ID, null!, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties));
        }

        [Fact(DisplayName = "Constructor should throw ArgumentNullException if mediaType parameter is null")]
        public void ConstructorWithNullMediaTypeTest()
        {
            Assert.Throws<ArgumentNullException>(() =>
                new EpubManifestItem(ID, HREF, null!, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties));
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null mediaOverlay parameter should succeed")]
        public void ConstructorWithNullMediaOverlayTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, null, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Null(epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null requiredNamespace parameter should succeed")]
        public void ConstructorWithNullRequiredNamespaceTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, null, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Null(epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null requiredModules parameter should succeed")]
        public void ConstructorWithNullRequiredModulesTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, null, FALLBACK, FALLBACK_STYLE, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Null(epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null fallback parameter should succeed")]
        public void ConstructorWithNullFallbackTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, null, FALLBACK_STYLE, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Null(epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null fallbackStyle parameter should succeed")]
        public void ConstructorWithNullFallbackStyleTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, null, Properties);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Null(epubManifestItem.FallbackStyle);
            Assert.Equal(Properties, epubManifestItem.Properties);
        }

        [Fact(DisplayName = "Constructing a EpubManifestItem instance with null properties parameter should succeed")]
        public void ConstructorWithNullPropertiesTest()
        {
            EpubManifestItem epubManifestItem = new(ID, HREF, MEDIA_TYPE, MEDIA_OVERLAY, REQUIRED_NAMESPACE, REQUIRED_MODULES, FALLBACK, FALLBACK_STYLE, null);
            Assert.Equal(ID, epubManifestItem.Id);
            Assert.Equal(HREF, epubManifestItem.Href);
            Assert.Equal(MEDIA_TYPE, epubManifestItem.MediaType);
            Assert.Equal(MEDIA_OVERLAY, epubManifestItem.MediaOverlay);
            Assert.Equal(REQUIRED_NAMESPACE, epubManifestItem.RequiredNamespace);
            Assert.Equal(REQUIRED_MODULES, epubManifestItem.RequiredModules);
            Assert.Equal(FALLBACK, epubManifestItem.Fallback);
            Assert.Equal(FALLBACK_STYLE, epubManifestItem.FallbackStyle);
            Assert.Null(epubManifestItem.Properties);
        }

        public class EpubGuideReferenceTests
        {
            [Fact(DisplayName = "ToString method should produce a string with the values of Id, Href, and MediaType properties")]
            public void ToStringTest()
            {
                EpubManifestItem epubManifestItem = new
                (
                    id: "item-1",
                    href: "chapter1.html",
                    mediaType: "application/xhtml+xml"
                );
                Assert.Equal("Id: item-1, Href = chapter1.html, MediaType = application/xhtml+xml", epubManifestItem.ToString());
            }
        }
    }
}
