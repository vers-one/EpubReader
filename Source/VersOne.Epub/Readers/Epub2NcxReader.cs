using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
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

        public Epub2NcxReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<Epub2Ncx?> ReadEpub2NcxAsync(IZipFile epubFile, string contentDirectoryPath, EpubPackage package)
        {
            string? tocId = package.Spine.Toc;
            if (String.IsNullOrEmpty(tocId))
            {
                return null;
            }
            EpubManifestItem tocManifestItem = package.Manifest.Items.FirstOrDefault(item => item.Id.CompareOrdinalIgnoreCase(tocId)) ??
                throw new Epub2NcxException($"EPUB parsing error: TOC item {tocId} not found in EPUB manifest.");
            string tocFileEntryPath = ContentPathUtils.Combine(contentDirectoryPath, tocManifestItem.Href);
            IZipFileEntry? tocFileEntry = epubFile.GetEntry(tocFileEntryPath) ??
                throw new Epub2NcxException($"EPUB parsing error: TOC file {tocFileEntryPath} not found in the EPUB file.");
            if (tocFileEntry.Length > Int32.MaxValue)
            {
                throw new Epub2NcxException($"EPUB parsing error: TOC file {tocFileEntryPath} is larger than 2 GB.");
            }
            XDocument containerDocument;
            using (Stream containerStream = tocFileEntry.Open())
            {
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace ncxNamespace = "http://www.daisy.org/z3986/2005/ncx/";
            XElement ncxNode = containerDocument.Element(ncxNamespace + "ncx") ?? throw new Epub2NcxException("EPUB parsing error: TOC file does not contain ncx element.");
            XElement headNode = ncxNode.Element(ncxNamespace + "head") ?? throw new Epub2NcxException("EPUB parsing error: TOC file does not contain head element.");
            Epub2NcxHead navigationHead = ReadNavigationHead(headNode);
            XElement docTitleNode = ncxNode.Element(ncxNamespace + "docTitle") ?? throw new Epub2NcxException("EPUB parsing error: TOC file does not contain docTitle element.");
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
            XElement navMapNode = ncxNode.Element(ncxNamespace + "navMap") ?? throw new Epub2NcxException("EPUB parsing error: TOC file does not contain navMap element.");
            Epub2NcxNavigationMap navMap = ReadNavigationMap(navMapNode, epubReaderOptions.Epub2NcxReaderOptions);
            XElement pageListNode = ncxNode.Element(ncxNamespace + "pageList");
            Epub2NcxPageList? pageList = null;
            if (pageListNode != null)
            {
                pageList = ReadNavigationPageList(pageListNode);
            }
            List<Epub2NcxNavigationList> navLists = new();
            foreach (XElement navigationListNode in ncxNode.Elements(ncxNamespace + "navList"))
            {
                Epub2NcxNavigationList navigationList = ReadNavigationList(navigationListNode);
                navLists.Add(navigationList);
            }
            return new(tocFileEntryPath, navigationHead, docTitle, docAuthors, navMap, pageList, navLists);
        }

        private static Epub2NcxHead ReadNavigationHead(XElement headNode)
        {
            List<Epub2NcxHeadMeta> items = new();
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
                        throw new Epub2NcxException("Incorrect EPUB navigation meta: meta name is missing.");
                    }
                    if (content == null)
                    {
                        throw new Epub2NcxException("Incorrect EPUB navigation meta: meta content is missing.");
                    }
                    items.Add(new Epub2NcxHeadMeta(name, content, scheme));
                }
            }
            return new(items);
        }

        private static string? ReadNavigationDocTitle(XElement docTitleNode)
        {
            foreach (XElement textNode in docTitleNode.Elements())
            {
                if (textNode.CompareNameTo("text"))
                {
                    return textNode.Value;
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

        private static Epub2NcxNavigationMap ReadNavigationMap(XElement navigationMapNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            List<Epub2NcxNavigationPoint> items = new();
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
            return new(items);
        }

        private static Epub2NcxNavigationPoint? ReadNavigationPoint(XElement navigationPointNode, Epub2NcxReaderOptions epub2NcxReaderOptions)
        {
            string? id = null;
            string? @class = null;
            string? playOrder = null;
            List<Epub2NcxNavigationLabel> navigationLabels = new();
            Epub2NcxContent? content = null;
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
                throw new Epub2NcxException("Incorrect EPUB navigation point: point ID is missing.");
            }
            foreach (XElement navigationPointChildNode in navigationPointNode.Elements())
            {
                switch (navigationPointChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationPointChildNode);
                        navigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationPointChildNode);
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
            if (!navigationLabels.Any())
            {
                throw new Epub2NcxException($"EPUB parsing error: navigation point \"{id}\" should contain at least one navigation label.");
            }
            if (content == null)
            {
                if (epub2NcxReaderOptions != null && epub2NcxReaderOptions.IgnoreMissingContentForNavigationPoints)
                {
                    return null;
                }
                else
                {
                    throw new Epub2NcxException($"EPUB parsing error: navigation point \"{id}\" should contain content.");
                }
            }
            return new(id, @class, playOrder, navigationLabels, content, childNavigationPoints);
        }

        private static Epub2NcxNavigationLabel ReadNavigationLabel(XElement navigationLabelNode)
        {
            XElement navigationLabelTextNode = navigationLabelNode.Element(navigationLabelNode.Name.Namespace + "text") ??
                throw new Epub2NcxException("Incorrect EPUB navigation label: label text element is missing.");
            string text = navigationLabelTextNode.Value;
            return new(text);
        }

        private static Epub2NcxContent ReadNavigationContent(XElement navigationContentNode)
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
                throw new Epub2NcxException("Incorrect EPUB navigation content: content source is missing.");
            }
            return new(id, source);
        }

        private static Epub2NcxPageList ReadNavigationPageList(XElement navigationPageListNode)
        {
            List<Epub2NcxPageTarget> items = new();
            foreach (XElement pageTargetNode in navigationPageListNode.Elements())
            {
                if (pageTargetNode.CompareNameTo("pageTarget"))
                {
                    Epub2NcxPageTarget pageTarget = ReadNavigationPageTarget(pageTargetNode);
                    items.Add(pageTarget);
                }
            }
            return new(items);
        }

        private static Epub2NcxPageTarget ReadNavigationPageTarget(XElement navigationPageTargetNode)
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
                throw new Epub2NcxException("Incorrect EPUB navigation page target: page target type is missing.");
            }
            foreach (XElement navigationPageTargetChildNode in navigationPageTargetNode.Elements())
            {
                switch (navigationPageTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationPageTargetChildNode);
                        navigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationPageTargetChildNode);
                        break;
                }
            }
            if (!navigationLabels.Any())
            {
                throw new Epub2NcxException("Incorrect EPUB navigation page target: at least one navLabel element is required.");
            }
            return new(id, value, type, @class, playOrder, navigationLabels, content);
        }

        private static Epub2NcxNavigationList ReadNavigationList(XElement navigationListNode)
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
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationListChildNode);
                        navigationLabels.Add(navigationLabel);
                        break;
                    case "navtarget":
                        Epub2NcxNavigationTarget navigationTarget = ReadNavigationTarget(navigationListChildNode);
                        navigationTargets.Add(navigationTarget);
                        break;
                }
            }
            if (!navigationLabels.Any())
            {
                throw new Epub2NcxException("Incorrect EPUB navigation page target: at least one navLabel element is required.");
            }
            return new(id, @class, navigationLabels, navigationTargets);
        }

        private static Epub2NcxNavigationTarget ReadNavigationTarget(XElement navigationTargetNode)
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
                throw new Epub2NcxException("Incorrect EPUB navigation target: navigation target ID is missing.");
            }
            foreach (XElement navigationTargetChildNode in navigationTargetNode.Elements())
            {
                switch (navigationTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationTargetChildNode);
                        navigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        content = ReadNavigationContent(navigationTargetChildNode);
                        break;
                }
            }
            if (!navigationLabels.Any())
            {
                throw new Epub2NcxException("Incorrect EPUB navigation target: at least one navLabel element is required.");
            }
            return new(id, @class, value, playOrder, navigationLabels, content);
        }
    }
}
