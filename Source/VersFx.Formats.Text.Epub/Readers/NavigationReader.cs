using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Xml;
using VersFx.Formats.Text.Epub.Schema.Navigation;
using VersFx.Formats.Text.Epub.Schema.Opf;
using VersFx.Formats.Text.Epub.Utils;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class NavigationReader
    {
        public static EpubNavigation ReadNavigation(ZipArchive epubArchive, string contentDirectoryPath, EpubPackage package)
        {
            EpubNavigation result = new EpubNavigation();
            string tocId = package.Spine.Toc;
            if (String.IsNullOrEmpty(tocId))
                throw new Exception("EPUB parsing error: TOC ID is empty.");
            EpubManifestItem tocManifestItem = package.Manifest.FirstOrDefault(item => String.Compare(item.Id, tocId, StringComparison.OrdinalIgnoreCase) == 0);
            if (tocManifestItem == null)
                throw new Exception(String.Format("EPUB parsing error: TOC item {0} not found in EPUB manifest.", tocId));
            string tocFileEntryPath = ZipPathUtils.Combine(contentDirectoryPath, tocManifestItem.Href);
            ZipArchiveEntry tocFileEntry = epubArchive.GetEntry(tocFileEntryPath);
            if (tocFileEntry == null)
                throw new Exception(String.Format("EPUB parsing error: TOC file {0} not found in archive.", tocFileEntryPath));
            if (tocFileEntry.Length > Int32.MaxValue)
                throw new Exception(String.Format("EPUB parsing error: TOC file {0} is bigger than 2 Gb.", tocFileEntryPath));
            XmlDocument containerDocument;
            using (Stream containerStream = tocFileEntry.Open())
                containerDocument = XmlUtils.LoadDocument(containerStream);
            XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(containerDocument.NameTable);
            xmlNamespaceManager.AddNamespace("ncx", "http://www.daisy.org/z3986/2005/ncx/");
            XmlNode headNode = containerDocument.DocumentElement.SelectSingleNode("ncx:head", xmlNamespaceManager);
            if (headNode == null)
                throw new Exception("EPUB parsing error: TOC file does not contain head element");
            EpubNavigationHead navigationHead = ReadNavigationHead(headNode);
            result.Head = navigationHead;
            XmlNode docTitleNode = containerDocument.DocumentElement.SelectSingleNode("ncx:docTitle", xmlNamespaceManager);
            if (docTitleNode == null)
                throw new Exception("EPUB parsing error: TOC file does not contain docTitle element");
            EpubNavigationDocTitle navigationDocTitle = ReadNavigationDocTitle(docTitleNode);
            result.DocTitle = navigationDocTitle;
            result.DocAuthors = new List<EpubNavigationDocAuthor>();
            foreach (XmlNode docAuthorNode in containerDocument.DocumentElement.SelectNodes("ncx:docAuthor", xmlNamespaceManager))
            {
                EpubNavigationDocAuthor navigationDocAuthor = ReadNavigationDocAuthor(docAuthorNode);
                result.DocAuthors.Add(navigationDocAuthor);
            }
            XmlNode navMapNode = containerDocument.DocumentElement.SelectSingleNode("ncx:navMap", xmlNamespaceManager);
            if (navMapNode == null)
                throw new Exception("EPUB parsing error: TOC file does not contain navMap element");
            EpubNavigationMap navMap = ReadNavigationMap(navMapNode);
            result.NavMap = navMap;
            XmlNode pageListNode = containerDocument.DocumentElement.SelectSingleNode("ncx:pageList", xmlNamespaceManager);
            if (pageListNode != null)
            {
                EpubNavigationPageList pageList = ReadNavigationPageList(pageListNode);
                result.PageList = pageList;
            }
            result.NavLists = new List<EpubNavigationList>();
            foreach (XmlNode navigationListNode in containerDocument.DocumentElement.SelectNodes("ncx:navList", xmlNamespaceManager))
            {
                EpubNavigationList navigationList = ReadNavigationList(navigationListNode);
                result.NavLists.Add(navigationList);
            }
            return result;
        }

        private static EpubNavigationHead ReadNavigationHead(XmlNode headNode)
        {
            EpubNavigationHead result = new EpubNavigationHead();
            foreach (XmlNode metaNode in headNode.ChildNodes)
                if (String.Compare(metaNode.LocalName, "meta", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubNavigationHeadMeta meta = new EpubNavigationHeadMeta();
                    foreach (XmlAttribute metaNodeAttribute in metaNode.Attributes)
                    {
                        string attributeValue = metaNodeAttribute.Value;
                        switch (metaNodeAttribute.Name.ToLowerInvariant())
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
                        throw new Exception("Incorrect EPUB navigation meta: meta name is missing");
                    if (meta.Content == null)
                        throw new Exception("Incorrect EPUB navigation meta: meta content is missing");
                    result.Add(meta);
                }
            return result;
        }

        private static EpubNavigationDocTitle ReadNavigationDocTitle(XmlNode docTitleNode)
        {
            EpubNavigationDocTitle result = new EpubNavigationDocTitle();
            foreach (XmlNode textNode in docTitleNode.ChildNodes)
                if (String.Compare(textNode.LocalName, "text", StringComparison.OrdinalIgnoreCase) == 0)
                    result.Add(textNode.InnerText);
            return result;
        }

        private static EpubNavigationDocAuthor ReadNavigationDocAuthor(XmlNode docAuthorNode)
        {
            EpubNavigationDocAuthor result = new EpubNavigationDocAuthor();
            foreach (XmlNode textNode in docAuthorNode.ChildNodes)
                if (String.Compare(textNode.LocalName, "text", StringComparison.OrdinalIgnoreCase) == 0)
                    result.Add(textNode.InnerText);
            return result;
        }

        private static EpubNavigationMap ReadNavigationMap(XmlNode navigationMapNode)
        {
            EpubNavigationMap result = new EpubNavigationMap();
            foreach (XmlNode navigationPointNode in navigationMapNode.ChildNodes)
                if (String.Compare(navigationPointNode.LocalName, "navPoint", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubNavigationPoint navigationPoint = ReadNavigationPoint(navigationPointNode);
                    result.Add(navigationPoint);
                }
            return result;
        }

        private static EpubNavigationPoint ReadNavigationPoint(XmlNode navigationPointNode)
        {
            EpubNavigationPoint result = new EpubNavigationPoint();
            foreach (XmlAttribute navigationPointNodeAttribute in navigationPointNode.Attributes)
            {
                string attributeValue = navigationPointNodeAttribute.Value;
                switch (navigationPointNodeAttribute.Name.ToLowerInvariant())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                    case "playOrder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (String.IsNullOrWhiteSpace(result.Id))
                throw new Exception("Incorrect EPUB navigation point: point ID is missing");
            result.NavigationLabels = new List<EpubNavigationLabel>();
            result.ChildNavigationPoints = new List<EpubNavigationPoint>();
            foreach (XmlNode navigationPointChildNode in navigationPointNode.ChildNodes)
                switch (navigationPointChildNode.LocalName.ToLowerInvariant())
                {
                    case "navlabel":
                        EpubNavigationLabel navigationLabel = ReadNavigationLabel(navigationPointChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        EpubNavigationContent content = ReadNavigationContent(navigationPointChildNode);
                        result.Content = content;
                        break;
                    case "navpoint":
                        EpubNavigationPoint childNavigationPoint = ReadNavigationPoint(navigationPointChildNode);
                        result.ChildNavigationPoints.Add(childNavigationPoint);
                        break;
                }
            return result;
        }

        private static EpubNavigationLabel ReadNavigationLabel(XmlNode navigationLabelNode)
        {
            EpubNavigationLabel result = new EpubNavigationLabel();
            XmlNode navigationLabelTextNode = navigationLabelNode.ChildNodes.OfType<XmlNode>().
                Where(node => String.Compare(node.LocalName, "text", StringComparison.OrdinalIgnoreCase) == 0).FirstOrDefault();
            if (navigationLabelTextNode == null)
                throw new Exception("Incorrect EPUB navigation label: label text element is missing");
            result.Text = navigationLabelTextNode.InnerText;
            return result;
        }

        private static EpubNavigationContent ReadNavigationContent(XmlNode navigationContentNode)
        {
            EpubNavigationContent result = new EpubNavigationContent();
            foreach (XmlAttribute navigationContentNodeAttribute in navigationContentNode.Attributes)
            {
                string attributeValue = navigationContentNodeAttribute.Value;
                switch (navigationContentNodeAttribute.Name.ToLowerInvariant())
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
                throw new Exception("Incorrect EPUB navigation content: content source is missing");
            return result;
        }

        private static EpubNavigationPageList ReadNavigationPageList(XmlNode navigationPageListNode)
        {
            EpubNavigationPageList result = new EpubNavigationPageList();
            foreach (XmlNode pageTargetNode in navigationPageListNode.ChildNodes)
                if (String.Compare(pageTargetNode.LocalName, "pageTarget", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubNavigationPageTarget pageTarget = ReadNavigationPageTarget(pageTargetNode);
                    result.Add(pageTarget);
                }
            return result;
        }

        private static EpubNavigationPageTarget ReadNavigationPageTarget(XmlNode navigationPageTargetNode)
        {
            EpubNavigationPageTarget result = new EpubNavigationPageTarget();
            foreach (XmlAttribute navigationPageTargetNodeAttribute in navigationPageTargetNode.Attributes)
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.Name.ToLowerInvariant())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "value":
                        result.Value = attributeValue;
                        break;
                    case "type":
                        EpubNavigationPageTargetType type;
                        if (!Enum.TryParse<EpubNavigationPageTargetType>(attributeValue, out type))
                            throw new Exception(String.Format("Incorrect EPUB navigation page target: {0} is incorrect value for page target type", attributeValue));
                        result.Type = type;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                    case "playOrder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (result.Type == default(EpubNavigationPageTargetType))
                throw new Exception("Incorrect EPUB navigation page target: page target type is missing");
            foreach (XmlNode navigationPageTargetChildNode in navigationPageTargetNode.ChildNodes)
                switch (navigationPageTargetChildNode.LocalName.ToLowerInvariant())
                {
                    case "navlabel":
                        EpubNavigationLabel navigationLabel = ReadNavigationLabel(navigationPageTargetChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        EpubNavigationContent content = ReadNavigationContent(navigationPageTargetChildNode);
                        result.Content = content;
                        break;
                }
            if (!result.NavigationLabels.Any())
                throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required");
            return result;
        }

        private static EpubNavigationList ReadNavigationList(XmlNode navigationListNode)
        {
            EpubNavigationList result = new EpubNavigationList();
            foreach (XmlAttribute navigationListNodeAttribute in navigationListNode.Attributes)
            {
                string attributeValue = navigationListNodeAttribute.Value;
                switch (navigationListNodeAttribute.Name.ToLowerInvariant())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "class":
                        result.Class = attributeValue;
                        break;
                }
            }
            foreach (XmlNode navigationListChildNode in navigationListNode.ChildNodes)
                switch (navigationListChildNode.LocalName.ToLowerInvariant())
                {
                    case "navlabel":
                        EpubNavigationLabel navigationLabel = ReadNavigationLabel(navigationListChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "navTarget":
                        EpubNavigationTarget navigationTarget = ReadNavigationTarget(navigationListChildNode);
                        result.NavigationTargets.Add(navigationTarget);
                        break;
                }
            if (!result.NavigationLabels.Any())
                throw new Exception("Incorrect EPUB navigation page target: at least one navLabel element is required");
            return result;
        }

        private static EpubNavigationTarget ReadNavigationTarget(XmlNode navigationTargetNode)
        {
            EpubNavigationTarget result = new EpubNavigationTarget();
            foreach (XmlAttribute navigationPageTargetNodeAttribute in navigationTargetNode.Attributes)
            {
                string attributeValue = navigationPageTargetNodeAttribute.Value;
                switch (navigationPageTargetNodeAttribute.Name.ToLowerInvariant())
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
                    case "playOrder":
                        result.PlayOrder = attributeValue;
                        break;
                }
            }
            if (String.IsNullOrWhiteSpace(result.Id))
                throw new Exception("Incorrect EPUB navigation target: navigation target ID is missing");
            foreach (XmlNode navigationTargetChildNode in navigationTargetNode.ChildNodes)
                switch (navigationTargetChildNode.LocalName.ToLowerInvariant())
                {
                    case "navlabel":
                        EpubNavigationLabel navigationLabel = ReadNavigationLabel(navigationTargetChildNode);
                        result.NavigationLabels.Add(navigationLabel);
                        break;
                    case "content":
                        EpubNavigationContent content = ReadNavigationContent(navigationTargetChildNode);
                        result.Content = content;
                        break;
                }
            if (!result.NavigationLabels.Any())
                throw new Exception("Incorrect EPUB navigation target: at least one navLabel element is required");
            return result;
        }
    }
}
