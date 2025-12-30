using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal class Epub2NcxReader
    {
        private readonly EpubReaderOptions epubReaderOptions;
        private readonly Epub2NcxReaderOptions epub2NcxReaderOptions;

        public Epub2NcxReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
            epub2NcxReaderOptions = this.epubReaderOptions.Epub2NcxReaderOptions ?? new Epub2NcxReaderOptions();
        }

        public async Task<Epub2Ncx?> ReadEpub2NcxAsync(IZipFile epubFile, string contentDirectoryPath, EpubPackage package)
        {
            string? tocId = package.Spine.Toc;
            if (String.IsNullOrEmpty(tocId))
            {
                return null;
            }
            EpubManifestItem? tocManifestItem = package.Manifest.Items.Find(item => item.Id.CompareOrdinalIgnoreCase(tocId));
            if (tocManifestItem == null)
            {
                if (epub2NcxReaderOptions.IgnoreMissingTocManifestItemError)
                {
                    return null;
                }
                throw new Epub2NcxException($"EPUB parsing error: TOC item {tocId} not found in EPUB manifest.");
            }
            string tocFileEntryPath = ContentPathUtils.Combine(contentDirectoryPath, tocManifestItem.Href);
            IZipFileEntry? tocFileEntry = epubFile.GetEntry(tocFileEntryPath);
            if (tocFileEntry == null)
            {
                if (epub2NcxReaderOptions.IgnoreMissingTocFileError)
                {
                    return null;
                }
                throw new Epub2NcxException($"EPUB parsing error: TOC file {tocFileEntryPath} not found in the EPUB file.");
            }
            if (tocFileEntry.Length > Int32.MaxValue)
            {
                if (epub2NcxReaderOptions.IgnoreTocFileIsTooLargeError)
                {
                    return null;
                }
                throw new Epub2NcxException($"EPUB parsing error: TOC file {tocFileEntryPath} is larger than 2 GB.");
            }
            XDocument containerDocument;
            try
            {
                using Stream containerStream = tocFileEntry.Open();
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            catch (XmlException xmlException)
            {
                if (epub2NcxReaderOptions.IgnoreTocFileIsNotValidXmlError)
                {
                    return null;
                }
                throw new Epub2NcxException("EPUB parsing error: TOC file is not a valid XML file.", xmlException);
            }
            XNamespace ncxNamespace = "http://www.daisy.org/z3986/2005/ncx/";
            XElement? ncxNode = containerDocument.Element(ncxNamespace + "ncx");
            if (ncxNode == null)
            {
                if (epub2NcxReaderOptions.IgnoreMissingNcxElementError)
                {
                    return null;
                }
                throw new Epub2NcxException("EPUB parsing error: TOC file does not contain ncx element.");
            }
            XElement? headNode = ncxNode.Element(ncxNamespace + "head");
            if (headNode == null && !epub2NcxReaderOptions.IgnoreMissingHeadElementError)
            {
                throw new Epub2NcxException("EPUB parsing error: TOC file does not contain head element.");
            }
            Epub2NcxHead navigationHead = ReadNavigationHead(headNode, epub2NcxReaderOptions);
            XElement? docTitleNode = ncxNode.Element(ncxNamespace + "docTitle");
            if (docTitleNode == null && !epub2NcxReaderOptions.IgnoreMissingDocTitleElementError)
            {
                throw new Epub2NcxException("EPUB parsing error: TOC file does not contain docTitle element.");
            }
            string? docTitle = ReadNavigationDocTitle(docTitleNode);
            List<string> docAuthors = new();
            foreach (XElement docAuthorNode in ncxNode.Elements(ncxNamespace + "docAuthor"))
            {
                string? navigationDocAuthor = ReadNavigationDocAuthor(docAuthorNode);
                if (navigationDocAuthor != null)
                {
                    docAuthors.Add(navigationDocAuthor);
                }
            }
            XElement? navMapNode = ncxNode.Element(ncxNamespace + "navMap");
            if (navMapNode == null && !epub2NcxReaderOptions.IgnoreMissingNavMapElementError)
            {
                throw new Epub2NcxException("EPUB parsing error: TOC file does not contain navMap element.");
            }
            Epub2NcxNavigationMap navMap = ReadNavigationMap(navMapNode, epub2NcxReaderOptions);
            XElement? pageListNode = ncxNode.Element(ncxNamespace + "pageList");
            Epub2NcxPageList? pageList = null;
            if (pageListNode != null)
            {
                pageList = ReadNavigationPageList(pageListNode, epub2NcxReaderOptions);
            }
            List<Epub2NcxNavigationList> navLists = new();
            foreach (XElement navigationListNode in ncxNode.Elements(ncxNamespace + "navList"))
            {
                Epub2NcxNavigationList navigationList = ReadNavigationList(navigationListNode, epub2NcxReaderOptions);
                navLists.Add(navigationList);
            }
            return new(tocFileEntryPath, navigationHead, docTitle, docAuthors, navMap, pageList, navLists);
        }

        private static Epub2NcxHead ReadNavigationHead(XElement? headNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            List<Epub2NcxHeadMeta> items = new();
            if (headNode != null)
            {
                foreach (XElement metaNode in headNode.Elements())
                {
                    if (metaNode.CompareNameTo("meta"))
                    {
                        string? name = null;
                        string? content = null;
                        string? scheme = null;
                        foreach (XAttribute metaNodeAttribute in metaNode.Attributes())
                        {
                            string attributeValue = metaNodeAttribute.Value;
                            switch (metaNodeAttribute.GetLowerCaseLocalName())
                            {
                                case "name":
                                    name = attributeValue;
                                    break;
                                case "content":
                                    content = attributeValue;
                                    break;
                                case "scheme":
                                    scheme = attributeValue;
                                    break;
                            }
                        }
                        if (name == null)
                        {
                            if (epub2NcxReaderOptions.SkipInvalidMetaElements)
                            {
                                continue;
                            }
                            throw new Epub2NcxException("Incorrect EPUB navigation meta: meta name is missing.");
                        }
                        if (content == null)
                        {
                            if (epub2NcxReaderOptions.SkipInvalidMetaElements)
                            {
                                continue;
                            }
                            throw new Epub2NcxException("Incorrect EPUB navigation meta: meta content is missing.");
                        }
                        items.Add(new Epub2NcxHeadMeta(name, content, scheme));
                    }
                }
            }
            return new(items);
        }

        private static string? ReadNavigationDocTitle(XElement? docTitleNode)
        {
            if (docTitleNode != null)
            {
                foreach (XElement textNode in docTitleNode.Elements())
                {
                    if (textNode.CompareNameTo("text"))
                    {
                        return textNode.Value;
                    }
                }
            }
            return null;
        }

        private static string? ReadNavigationDocAuthor(XElement docAuthorNode)
        {
            foreach (XElement textNode in docAuthorNode.Elements())
            {
                if (textNode.CompareNameTo("text"))
                {
                    return textNode.Value;
                }
            }
            return null;
        }

        private static Epub2NcxNavigationMap ReadNavigationMap(XElement? navigationMapNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            List<Epub2NcxNavigationPoint> items = new();
            if (navigationMapNode != null)
            {
                foreach (XElement navigationPointNode in navigationMapNode.Elements())
                {
                    if (navigationPointNode.CompareNameTo("navPoint"))
                    {
                        Epub2NcxNavigationPoint? navigationPoint = ReadNavigationPoint(navigationPointNode, epub2NcxReaderOptions);
                        if (navigationPoint != null)
                        {
                            items.Add(navigationPoint);
                        }
                    }
                }
            }
            return new(items);
        }

        private static Epub2NcxNavigationPoint? ReadNavigationPoint(XElement navigationPointNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? @class = null;
            string? playOrder = null;
            List<Epub2NcxNavigationLabel> navigationLabels = new();
            Epub2NcxContent? content = null;
            bool isContentNodePresent = false;
            List<Epub2NcxNavigationPoint> childNavigationPoints = new();
            foreach (XAttribute navigationPointNodeAttribute in navigationPointNode.Attributes())
            {
                string attributeValue = navigationPointNodeAttribute.Value;
                switch (navigationPointNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "class":
                        @class = attributeValue;
                        break;
                    case "playorder":
                        playOrder = attributeValue;
                        break;
                }
            }
            if (id == null)
            {
                if (epub2NcxReaderOptions.SkipNavigationPointsWithMissingIds)
                {
                    return null;
                }
                throw new Epub2NcxException("Incorrect EPUB navigation point: point ID is missing.");
            }
            foreach (XElement navigationPointChildNode in navigationPointNode.Elements())
            {
                switch (navigationPointChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel? navigationLabel = ReadNavigationLabel(navigationPointChildNode, epub2NcxReaderOptions);
                        if (navigationLabel != null)
                        {
                            navigationLabels.Add(navigationLabel);
                        }
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationPointChildNode, epub2NcxReaderOptions);
                        isContentNodePresent = true;
                        break;
                    case "navpoint":
                        Epub2NcxNavigationPoint? childNavigationPoint = ReadNavigationPoint(navigationPointChildNode, epub2NcxReaderOptions);
                        if (childNavigationPoint != null)
                        {
                            childNavigationPoints.Add(childNavigationPoint);
                        }
                        break;
                }
            }
            if (!navigationLabels.Any() && !epub2NcxReaderOptions.AllowNavigationPointsWithoutLabels)
            {
                throw new Epub2NcxException($"EPUB parsing error: navigation point \"{id}\" should contain at least one navigation label.");
            }
            if (content == null)
            {
                if (isContentNodePresent)
                {
                    return null;
                }
                if (epub2NcxReaderOptions.IgnoreMissingContentForNavigationPoints)
                {
                    return null;
                }
                throw new Epub2NcxException($"EPUB parsing error: navigation point \"{id}\" should contain content.");
            }
            return new(id, @class, playOrder, navigationLabels, content, childNavigationPoints);
        }

        private static Epub2NcxNavigationLabel? ReadNavigationLabel(XElement navigationLabelNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            XElement? navigationLabelTextNode = navigationLabelNode.Element(navigationLabelNode.Name.Namespace + "text");
            if (navigationLabelTextNode == null)
            {
                if (epub2NcxReaderOptions.SkipInvalidNavigationLabels)
                {
                    return null;
                }
                throw new Epub2NcxException("Incorrect EPUB navigation label: label text element is missing.");
            }
            string text = navigationLabelTextNode.Value;
            return new(text);
        }

        private static Epub2NcxContent? ReadNavigationContent(XElement navigationContentNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? source = null;
            foreach (XAttribute navigationContentNodeAttribute in navigationContentNode.Attributes())
            {
                string attributeValue = navigationContentNodeAttribute.Value;
                switch (navigationContentNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "src":
                        source = Uri.UnescapeDataString(attributeValue);
                        break;
                }
            }
            if (source == null)
            {
                if (epub2NcxReaderOptions.SkipInvalidNavigationContent)
                {
                    return null;
                }
                throw new Epub2NcxException("Incorrect EPUB navigation content: content source is missing.");
            }
            return new(id, source);
        }

        private static Epub2NcxPageList ReadNavigationPageList(XElement navigationPageListNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            List<Epub2NcxPageTarget> items = new();
            foreach (XElement pageTargetNode in navigationPageListNode.Elements())
            {
                if (pageTargetNode.CompareNameTo("pageTarget"))
                {
                    Epub2NcxPageTarget pageTarget = ReadNavigationPageTarget(pageTargetNode, epub2NcxReaderOptions);
                    items.Add(pageTarget);
                }
            }
            return new(items);
        }

        private static Epub2NcxPageTarget ReadNavigationPageTarget(XElement navigationPageTargetNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? value = null;
            Epub2NcxPageTargetType type = default;
            string? @class = null;
            string? playOrder = null;
            List<Epub2NcxNavigationLabel> navigationLabels = new();
            Epub2NcxContent? content = null;
            foreach (XAttribute navigationPageTargetNodeAttribute in navigationPageTargetNode.Attributes())
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "value":
                        value = attributeValue;
                        break;
                    case "type":
                        if (!Enum.TryParse(attributeValue, true, out type))
                        {
                            type = Epub2NcxPageTargetType.UNKNOWN;
                        }
                        break;
                    case "class":
                        @class = attributeValue;
                        break;
                    case "playorder":
                        playOrder = attributeValue;
                        break;
                }
            }
            if (type == default)
            {
                if (epub2NcxReaderOptions.ReplaceMissingPageTargetTypesWithUnknown)
                {
                    type = Epub2NcxPageTargetType.UNKNOWN;
                }
                else
                {
                    throw new Epub2NcxException("Incorrect EPUB navigation page target: page target type is missing.");
                }
            }
            foreach (XElement navigationPageTargetChildNode in navigationPageTargetNode.Elements())
            {
                switch (navigationPageTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel? navigationLabel = ReadNavigationLabel(navigationPageTargetChildNode, epub2NcxReaderOptions);
                        if (navigationLabel != null)
                        {
                            navigationLabels.Add(navigationLabel);
                        }
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationPageTargetChildNode, epub2NcxReaderOptions);
                        break;
                }
            }
            if (!navigationLabels.Any() && !epub2NcxReaderOptions.AllowNavigationPageTargetsWithoutLabels)
            {
                throw new Epub2NcxException("Incorrect EPUB navigation page target: at least one navLabel element is required.");
            }
            return new(id, value, type, @class, playOrder, navigationLabels, content);
        }

        private static Epub2NcxNavigationList ReadNavigationList(XElement navigationListNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? @class = null;
            List<Epub2NcxNavigationLabel> navigationLabels = new();
            List<Epub2NcxNavigationTarget> navigationTargets = new();
            foreach (XAttribute navigationListNodeAttribute in navigationListNode.Attributes())
            {
                string attributeValue = navigationListNodeAttribute.Value;
                switch (navigationListNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "class":
                        @class = attributeValue;
                        break;
                }
            }
            foreach (XElement navigationListChildNode in navigationListNode.Elements())
            {
                switch (navigationListChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel? navigationLabel = ReadNavigationLabel(navigationListChildNode, epub2NcxReaderOptions);
                        if (navigationLabel != null)
                        {
                            navigationLabels.Add(navigationLabel);
                        }
                        break;
                    case "navtarget":
                        Epub2NcxNavigationTarget? navigationTarget = ReadNavigationTarget(navigationListChildNode, epub2NcxReaderOptions);
                        if (navigationTarget != null)
                        {
                            navigationTargets.Add(navigationTarget);
                        }
                        break;
                }
            }
            if (!navigationLabels.Any() && !epub2NcxReaderOptions.AllowNavigationListsWithoutLabels)
            {
                throw new Epub2NcxException("Incorrect EPUB navigation list: at least one navLabel element is required.");
            }
            return new(id, @class, navigationLabels, navigationTargets);
        }

        private static Epub2NcxNavigationTarget? ReadNavigationTarget(XElement navigationTargetNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? @class = null;
            string? value = null;
            string? playOrder = null;
            List<Epub2NcxNavigationLabel> navigationLabels = new();
            Epub2NcxContent? content = null;
            foreach (XAttribute navigationPageTargetNodeAttribute in navigationTargetNode.Attributes())
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "value":
                        value = attributeValue;
                        break;
                    case "class":
                        @class = attributeValue;
                        break;
                    case "playorder":
                        playOrder = attributeValue;
                        break;
                }
            }
            if (id == null)
            {
                if (epub2NcxReaderOptions.SkipInvalidNavigationTargets)
                {
                    return null;
                }
                throw new Epub2NcxException("Incorrect EPUB navigation target: navigation target ID is missing.");
            }
            foreach (XElement navigationTargetChildNode in navigationTargetNode.Elements())
            {
                switch (navigationTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel? navigationLabel = ReadNavigationLabel(navigationTargetChildNode, epub2NcxReaderOptions);
                        if (navigationLabel != null)
                        {
                            navigationLabels.Add(navigationLabel);
                        }
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationTargetChildNode, epub2NcxReaderOptions);
                        break;
                }
            }
            if (!navigationLabels.Any() && !epub2NcxReaderOptions.AllowNavigationTargetsWithoutLabels)
            {
                throw new Epub2NcxException("Incorrect EPUB navigation target: at least one navLabel element is required.");
            }
            return new(id, @class, value, playOrder, navigationLabels, content);
        }
    }
}
