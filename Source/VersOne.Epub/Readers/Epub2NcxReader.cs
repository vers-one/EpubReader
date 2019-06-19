using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal static class Epub2NcxReader
    {
        public static async Task<Epub2Ncx> ReadEpub2NcxAsync(ZipArchive epubArchive, string contentDirectoryPath, EpubPackage package)
        {
            Epub2Ncx result = new Epub2Ncx();
            string tocId = package.Spine.Toc;
            if (String.IsNullOrEmpty(tocId))
            {
                return null;
            }
            EpubManifestItem tocManifestItem = package.Manifest.FirstOrDefault(item => item.Id.CompareOrdinalIgnoreCase(tocId));
            if (tocManifestItem == null)
            {
                throw new Exception($"EPUB parsing error: TOC item {tocId} not found in EPUB manifest.");
            }
            string tocFileEntryPath = ZipPathUtils.Combine(contentDirectoryPath, tocManifestItem.Href);
            ZipArchiveEntry tocFileEntry = epubArchive.GetEntry(tocFileEntryPath);
            if (tocFileEntry == null)
            {
                throw new Exception($"EPUB parsing error: TOC file {tocFileEntryPath} not found in archive.");
            }
            if (tocFileEntry.Length > Int32.MaxValue)
            {
                throw new Exception($"EPUB parsing error: TOC file {tocFileEntryPath} is larger than 2 Gb.");
            }
            XDocument containerDocument;
            using (Stream containerStream = tocFileEntry.Open())
            {
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream).ConfigureAwait(false);
            }
            XNamespace ncxNamespace = "http://www.daisy.org/z3986/2005/ncx/";
            XElement ncxNode = containerDocument.Element(ncxNamespace + "ncx");
            if (ncxNode == null)
            {
                throw new Exception("EPUB parsing error: TOC file does not contain ncx element.");
            }
            XElement headNode = ncxNode.Element(ncxNamespace + "head");
            if (headNode == null)
            {
                throw new Exception("EPUB parsing error: TOC file does not contain head element.");
            }
            Epub2NcxHead navigationHead = ReadNavigationHead(headNode);
            result.Head = navigationHead;
            XElement docTitleNode = ncxNode.Element(ncxNamespace + "docTitle");
            if (docTitleNode == null)
            {
                throw new Exception("EPUB parsing error: TOC file does not contain docTitle element.");
            }
            Epub2NcxDocTitle navigationDocTitle = ReadNavigationDocTitle(docTitleNode);
            result.DocTitle = navigationDocTitle;
            result.DocAuthors = new List<Epub2NcxDocAuthor>();
            foreach (XElement docAuthorNode in ncxNode.Elements(ncxNamespace + "docAuthor"))
            {
                Epub2NcxDocAuthor navigationDocAuthor = ReadNavigationDocAuthor(docAuthorNode);
                result.DocAuthors.Add(navigationDocAuthor);
            }
            XElement navMapNode = ncxNode.Element(ncxNamespace + "navMap");
            if (navMapNode == null)
            {
                throw new Exception("EPUB parsing error: TOC file does not contain navMap element.");
            }
            Epub2NcxNavigationMap navMap = ReadNavigationMap(navMapNode);
            result.NavMap = navMap;
            XElement pageListNode = ncxNode.Element(ncxNamespace + "pageList");
            if (pageListNode != null)
            {
                Epub2NcxPageList pageList = ReadNavigationPageList(pageListNode);
                result.PageList = pageList;
            }
            result.NavLists = new List<Epub2NcxNavigationList>();
            foreach (XElement navigationListNode in ncxNode.Elements(ncxNamespace + "navList"))
            {
                Epub2NcxNavigationList navigationList = ReadNavigationList(navigationListNode);
                result.NavLists.Add(navigationList);
            }
            return result;
        }

        private static Epub2NcxHead ReadNavigationHead(XElement headNode)
        {
            Epub2NcxHead result = new Epub2NcxHead();
            foreach (XElement metaNode in headNode.Elements())
            {
                if (metaNode.CompareNameTo("meta"))
                {
                    Epub2NcxHeadMeta meta = new Epub2NcxHeadMeta();
                    foreach (XAttribute metaNodeAttribute in metaNode.Attributes())
                    {
                        string attributeValue = metaNodeAttribute.Value;
                        switch (metaNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "name":
                                meta.Name = attributeValue;
                                break;
                            case "content":
                                meta.Content = attributeValue;
                                break;
                            case "scheme":
                                meta.Scheme = attributeValue;
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(meta.Name))
                    {
                        throw new Exception("Incorrect EPUB navigation meta: meta name is missing.");
                    }
                    if (meta.Content == null)
                    {
                        throw new Exception("Incorrect EPUB navigation meta: meta content is missing.");
                    }
                    result.Add(meta);
                }
            }
            return result;
        }

        private static Epub2NcxDocTitle ReadNavigationDocTitle(XElement docTitleNode)
        {
            Epub2NcxDocTitle result = new Epub2NcxDocTitle();
            foreach (XElement textNode in docTitleNode.Elements())
            {
                if (textNode.CompareNameTo("text"))
                {
                    result.Add(textNode.Value);
                }
            }
            return result;
        }

        private static Epub2NcxDocAuthor ReadNavigationDocAuthor(XElement docAuthorNode)
        {
            Epub2NcxDocAuthor result = new Epub2NcxDocAuthor();
            foreach (XElement textNode in docAuthorNode.Elements())
            {
                if (textNode.CompareNameTo("text"))
                {
                    result.Add(textNode.Value);
                }
            }
            return result;
        }

        private static Epub2NcxNavigationMap ReadNavigationMap(XElement navigationMapNode)
        {
            Epub2NcxNavigationMap result = new Epub2NcxNavigationMap();
            foreach (XElement navigationPointNode in navigationMapNode.Elements())
            {
                if (navigationPointNode.CompareNameTo("navPoint"))
                {
                    Epub2NcxNavigationPoint navigationPoint = ReadNavigationPoint(navigationPointNode);
                    result.Add(navigationPoint);
                }
            }
            return result;
        }

        private static Epub2NcxNavigationPoint ReadNavigationPoint(XElement navigationPointNode)
        {
            Epub2NcxNavigationPoint result = new Epub2NcxNavigationPoint();
            foreach (XAttribute navigationPointNodeAttribute in navigationPointNode.Attributes())
            {
                string attributeValue = navigationPointNodeAttribute.Value;
                switch (navigationPointNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                    case "playorder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (String.IsNullOrWhiteSpace(result.Id))
            {
                throw new Exception("Incorrect EPUB navigation point: point ID is missing.");
            }
            result.NavigationLabels = new List<Epub2NcxNavigationLabel>();
            result.ChildNavigationPoints = new List<Epub2NcxNavigationPoint>();
            foreach (XElement navigationPointChildNode in navigationPointNode.Elements())
            {
                switch (navigationPointChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationPointChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        Epub2NcxContent content = ReadNavigationContent(navigationPointChildNode);
                        result.Content = content;
                        break;
                    case "navpoint":
                        Epub2NcxNavigationPoint childNavigationPoint = ReadNavigationPoint(navigationPointChildNode);
                        result.ChildNavigationPoints.Add(childNavigationPoint);
                        break;
                }
            }
            if (!result.NavigationLabels.Any())
            {
                throw new Exception($"EPUB parsing error: navigation point {result.Id} should contain at least one navigation label.");
            }
            if (result.Content == null)
            {
                throw new Exception($"EPUB parsing error: navigation point {result.Id} should contain content.");
            }
            return result;
        }

        private static Epub2NcxNavigationLabel ReadNavigationLabel(XElement navigationLabelNode)
        {
            Epub2NcxNavigationLabel result = new Epub2NcxNavigationLabel();
            XElement navigationLabelTextNode = navigationLabelNode.Element(navigationLabelNode.Name.Namespace + "text");
            if (navigationLabelTextNode == null)
            {
                throw new Exception("Incorrect EPUB navigation label: label text element is missing.");
            }
            result.Text = navigationLabelTextNode.Value;
            return result;
        }

        private static Epub2NcxContent ReadNavigationContent(XElement navigationContentNode)
        {
            Epub2NcxContent result = new Epub2NcxContent();
            foreach (XAttribute navigationContentNodeAttribute in navigationContentNode.Attributes())
            {
                string attributeValue = navigationContentNodeAttribute.Value;
                switch (navigationContentNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "src":
                        result.Source = attributeValue;
                        break;
                }
            }
            if (String.IsNullOrWhiteSpace(result.Source))
            {
                throw new Exception("Incorrect EPUB navigation content: content source is missing.");
            }
            return result;
        }

        private static Epub2NcxPageList ReadNavigationPageList(XElement navigationPageListNode)
        {
            Epub2NcxPageList result = new Epub2NcxPageList();
            foreach (XElement pageTargetNode in navigationPageListNode.Elements())
            {
                if (pageTargetNode.CompareNameTo("pageTarget"))
                {
                    Epub2NcxPageTarget pageTarget = ReadNavigationPageTarget(pageTargetNode);
                    result.Add(pageTarget);
                }
            }
            return result;
        }

        private static Epub2NcxPageTarget ReadNavigationPageTarget(XElement navigationPageTargetNode)
        {
            Epub2NcxPageTarget result = new Epub2NcxPageTarget();
            foreach (XAttribute navigationPageTargetNodeAttribute in navigationPageTargetNode.Attributes())
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "value":
                        result.Value = attributeValue;
                        break;
                    case "type":
                        Epub2NcxPageTargetType type;
                        if (Enum.TryParse(attributeValue, true, out type))
                        {
                            result.Type = type;
                        }
                        else
                        {
                            result.Type = Epub2NcxPageTargetType.UNKNOWN;
                        }
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                    case "playorder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (result.Type == default(Epub2NcxPageTargetType))
            {
                throw new Exception("Incorrect EPUB navigation page target: page target type is missing.");
            }
            result.NavigationLabels = new List<Epub2NcxNavigationLabel>();
            foreach (XElement navigationPageTargetChildNode in navigationPageTargetNode.Elements())
                switch (navigationPageTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationPageTargetChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        Epub2NcxContent content = ReadNavigationContent(navigationPageTargetChildNode);
                        result.Content = content;
                        break;
                }
            if (!result.NavigationLabels.Any())
            {
                throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required.");
            }
            return result;
        }

        private static Epub2NcxNavigationList ReadNavigationList(XElement navigationListNode)
        {
            Epub2NcxNavigationList result = new Epub2NcxNavigationList();
            foreach (XAttribute navigationListNodeAttribute in navigationListNode.Attributes())
            {
                string attributeValue = navigationListNodeAttribute.Value;
                switch (navigationListNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                }
            }
            foreach (XElement navigationListChildNode in navigationListNode.Elements())
            {
                switch (navigationListChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationListChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "navTarget":
                        Epub2NcxNavigationTarget navigationTarget = ReadNavigationTarget(navigationListChildNode);
                        result.NavigationTargets.Add(navigationTarget);
                        break;
                }
            }
            if (!result.NavigationLabels.Any())
            {
                throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required.");
            }
            return result;
        }

        private static Epub2NcxNavigationTarget ReadNavigationTarget(XElement navigationTargetNode)
        {
            Epub2NcxNavigationTarget result = new Epub2NcxNavigationTarget();
            foreach (XAttribute navigationPageTargetNodeAttribute in navigationTargetNode.Attributes())
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "value":
                        result.Value = attributeValue;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                    case "playorder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (String.IsNullOrWhiteSpace(result.Id))
            {
                throw new Exception("Incorrect EPUB navigation target: navigation target ID is missing.");
            }
            foreach (XElement navigationTargetChildNode in navigationTargetNode.Elements())
            {
                switch (navigationTargetChildNode.GetLowerCaseLocalName())
                {
                    case "navlabel":
                        Epub2NcxNavigationLabel navigationLabel = ReadNavigationLabel(navigationTargetChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        Epub2NcxContent content = ReadNavigationContent(navigationTargetChildNode);
                        result.Content = content;
                        break;
                }
            }
            if (!result.NavigationLabels.Any())
            {
                throw new Exception("Incorrect EPUB navigation target: at least one navLabel element is required.");
            }
            return result;
        }
    }
}
