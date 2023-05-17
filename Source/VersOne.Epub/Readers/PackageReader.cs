using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using System.Xml.Linq;
using VersOne.Epub.Environment;
using VersOne.Epub.Options;
using VersOne.Epub.Schema;
using VersOne.Epub.Utils;

namespace VersOne.Epub.Internal
{
    internal class PackageReader
    {
        private readonly EpubReaderOptions epubReaderOptions;

        public PackageReader(EpubReaderOptions? epubReaderOptions = null)
        {
            this.epubReaderOptions = epubReaderOptions ?? new EpubReaderOptions();
        }

        public async Task<EpubPackage> ReadPackageAsync(IZipFile epubFile, string rootFilePath)
        {
            IZipFileEntry? rootFileEntry = epubFile.GetEntry(rootFilePath) ?? throw new EpubContainerException("EPUB parsing error: root file not found in the EPUB file.");
            XDocument containerDocument;
            using (Stream containerStream = rootFileEntry.Open())
            {
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace opfNamespace = "http://www.idpf.org/2007/opf";
            XElement packageNode = containerDocument.Element(opfNamespace + "package") ??
                throw new EpubPackageException("EPUB parsing error: package XML element not found in the package file.");
            string? uniqueIdentifier = null;
            string? epubVersionString = null;
            string? id = null;
            EpubTextDirection? textDirection = null;
            string? prefix = null;
            string? language = null;
            foreach (XAttribute packageNodeAttribute in packageNode.Attributes())
            {
                string attributeValue = packageNodeAttribute.Value;
                switch (packageNodeAttribute.GetLowerCaseLocalName())
                {
                    case "unique-identifier":
                        uniqueIdentifier = attributeValue;
                        break;
                    case "version":
                        epubVersionString = attributeValue;
                        break;
                    case "id":
                        id = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "prefix":
                        prefix = attributeValue;
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            if (epubVersionString == null)
            {
                throw new EpubPackageException("EPUB parsing error: EPUB version is not specified in the package.");
            }
            EpubVersion epubVersion = epubVersionString switch
            {
                "2.0" => EpubVersion.EPUB_2,
                "3.0" => EpubVersion.EPUB_3,
                "3.1" => EpubVersion.EPUB_3_1,
                _ => throw new EpubPackageException($"Unsupported EPUB version: \"{epubVersionString}\".")
            };
            XElement metadataNode = packageNode.Element(opfNamespace + "metadata") ?? throw new EpubPackageException("EPUB parsing error: metadata not found in the package.");
            EpubMetadata metadata = MetadataReader.ReadMetadata(metadataNode);
            XElement manifestNode = packageNode.Element(opfNamespace + "manifest") ?? throw new EpubPackageException("EPUB parsing error: manifest not found in the package.");
            EpubManifest manifest = ReadManifest(manifestNode, epubReaderOptions.PackageReaderOptions);
            XElement spineNode = packageNode.Element(opfNamespace + "spine") ?? throw new EpubPackageException("EPUB parsing error: spine not found in the package.");
            EpubSpine spine = ReadSpine(spineNode, epubVersion, epubReaderOptions.PackageReaderOptions);
            EpubGuide? guide = null;
            XElement guideNode = packageNode.Element(opfNamespace + "guide");
            if (guideNode != null)
            {
                guide = ReadGuide(guideNode);
            }
            List<EpubCollection> collections = ReadCollections(packageNode);
            return new(uniqueIdentifier, epubVersion, metadata, manifest, spine, guide, collections, id, textDirection, prefix, language);
        }

        private static EpubManifest ReadManifest(XElement manifestNode, PackageReaderOptions packageReaderOptions)
        {
            XAttribute? manifestIdAttribute = manifestNode.Attribute("id");
            string? manifestId = manifestIdAttribute?.Value;
            List<EpubManifestItem> items = new();
            HashSet<string> manifestItemIds = new();
            HashSet<string> manifestItemHrefs = new();
            foreach (XElement manifestItemNode in manifestNode.Elements())
            {
                if (manifestItemNode.CompareNameTo("item"))
                {
                    string? manifestItemId = null;
                    string? href = null;
                    string? mediaType = null;
                    string? mediaOverlay = null;
                    string? requiredNamespace = null;
                    string? requiredModules = null;
                    string? fallback = null;
                    string? fallbackStyle = null;
                    List<EpubManifestProperty>? properties = null;
                    foreach (XAttribute manifestItemNodeAttribute in manifestItemNode.Attributes())
                    {
                        string attributeValue = manifestItemNodeAttribute.Value;
                        switch (manifestItemNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "id":
                                manifestItemId = attributeValue;
                                break;
                            case "href":
                                href = Uri.UnescapeDataString(attributeValue);
                                break;
                            case "media-type":
                                mediaType = attributeValue;
                                break;
                            case "media-overlay":
                                mediaOverlay = attributeValue;
                                break;
                            case "required-namespace":
                                requiredNamespace = attributeValue;
                                break;
                            case "required-modules":
                                requiredModules = attributeValue;
                                break;
                            case "fallback":
                                fallback = attributeValue;
                                break;
                            case "fallback-style":
                                fallbackStyle = attributeValue;
                                break;
                            case "properties":
                                properties = EpubManifestPropertyParser.ParsePropertyList(attributeValue);
                                break;
                        }
                    }
                    if (manifestItemId == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item ID is missing.");
                    }
                    if (href == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item href is missing.");
                    }
                    if (mediaType == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item media type is missing.");
                    }
                    if (manifestItemIds.Contains(manifestItemId))
                    {
                        throw new EpubPackageException($"Incorrect EPUB manifest: item with ID = \"{manifestItemId}\" is not unique.");
                    }
                    manifestItemIds.Add(manifestItemId);
                    if (manifestItemHrefs.Contains(href))
                    {
                        throw new EpubPackageException($"Incorrect EPUB manifest: item with href = \"{href}\" is not unique.");
                    }
                    manifestItemHrefs.Add(href);
                    items.Add(new EpubManifestItem(manifestItemId, href, mediaType, mediaOverlay, requiredNamespace, requiredModules, fallback, fallbackStyle, properties));
                }
            }
            return new(manifestId, items);
        }

        private static EpubSpine ReadSpine(XElement spineNode, EpubVersion epubVersion, PackageReaderOptions packageReaderOptions)
        {
            string? spineId = null;
            EpubPageProgressionDirection? pageProgressionDirection = null;
            string? toc = null;
            List<EpubSpineItemRef> items = new();
            foreach (XAttribute spineNodeAttribute in spineNode.Attributes())
            {
                string attributeValue = spineNodeAttribute.Value;
                switch (spineNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        spineId = attributeValue;
                        break;
                    case "page-progression-direction":
                        pageProgressionDirection = EpubPageProgressionDirectionParser.Parse(attributeValue);
                        break;
                    case "toc":
                        toc = attributeValue;
                        break;
                }
            }
            if (epubVersion == EpubVersion.EPUB_2 && String.IsNullOrWhiteSpace(toc) && (packageReaderOptions == null || !packageReaderOptions.IgnoreMissingToc))
            {
                throw new EpubPackageException("Incorrect EPUB spine: TOC is missing.");
            }
            foreach (XElement spineItemNode in spineNode.Elements())
            {
                if (spineItemNode.CompareNameTo("itemref"))
                {
                    string? spineItemRefId = null;
                    string? idRef = null;
                    bool isLinear;
                    List<EpubSpineProperty>? properties = null;
                    foreach (XAttribute spineItemNodeAttribute in spineItemNode.Attributes())
                    {
                        string attributeValue = spineItemNodeAttribute.Value;
                        switch (spineItemNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "id":
                                spineItemRefId = attributeValue;
                                break;
                            case "idref":
                                idRef = attributeValue;
                                break;
                            case "properties":
                                properties = EpubSpinePropertyParser.ParsePropertyList(attributeValue);
                                break;
                        }
                    }
                    if (idRef == null)
                    {
                        throw new EpubPackageException("Incorrect EPUB spine: item ID ref is missing.");
                    }
                    XAttribute linearAttribute = spineItemNode.Attribute("linear");
                    isLinear = linearAttribute == null || !linearAttribute.CompareValueTo("no");
                    items.Add(new EpubSpineItemRef(spineItemRefId, idRef, isLinear, properties));
                }
            }
            return new(spineId, pageProgressionDirection, toc, items);
        }

        private static EpubGuide ReadGuide(XElement guideNode)
        {
            List<EpubGuideReference> items = new();
            foreach (XElement guideReferenceNode in guideNode.Elements())
            {
                if (guideReferenceNode.CompareNameTo("reference"))
                {
                    string? type = null;
                    string? title = null;
                    string? href = null;
                    foreach (XAttribute guideReferenceNodeAttribute in guideReferenceNode.Attributes())
                    {
                        string attributeValue = guideReferenceNodeAttribute.Value;
                        switch (guideReferenceNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "type":
                                type = attributeValue;
                                break;
                            case "title":
                                title = attributeValue;
                                break;
                            case "href":
                                href = Uri.UnescapeDataString(attributeValue);
                                break;
                        }
                    }
                    if (type == null)
                    {
                        throw new EpubPackageException("Incorrect EPUB guide: item type is missing.");
                    }
                    if (href == null)
                    {
                        throw new EpubPackageException("Incorrect EPUB guide: item href is missing.");
                    }
                    items.Add(new EpubGuideReference(type, title, href));
                }
            }
            return new(items);
        }

        private static List<EpubCollection> ReadCollections(XElement packageNode)
        {
            List<EpubCollection> result = new();
            foreach (XElement collectionNode in packageNode.Elements(packageNode.Name.Namespace + "collection"))
            {
                result.Add(ReadCollection(collectionNode));
            }
            return result;
        }

        private static EpubCollection ReadCollection(XElement collectionNode)
        {
            string? role = null;
            string? id = null;
            EpubTextDirection? textDirection = null;
            string? language = null;
            foreach (XAttribute collectionNodeAttribute in collectionNode.Attributes())
            {
                string attributeValue = collectionNodeAttribute.Value;
                switch (collectionNodeAttribute.GetLowerCaseLocalName())
                {
                    case "role":
                        role = attributeValue;
                        break;
                    case "id":
                        id = attributeValue;
                        break;
                    case "dir":
                        textDirection = EpubTextDirectionParser.Parse(attributeValue);
                        break;
                    case "lang":
                        language = attributeValue;
                        break;
                }
            }
            if (role == null)
            {
                throw new EpubPackageException("Incorrect EPUB collection: collection role is missing.");
            }
            EpubMetadata? metadata = null;
            List<EpubCollection> nestedCollections = new();
            List<EpubMetadataLink> links = new();
            foreach (XElement collectionChildNode in collectionNode.Elements())
            {
                switch (collectionChildNode.GetLowerCaseLocalName())
                {
                    case "metadata":
                        metadata = MetadataReader.ReadMetadata(collectionChildNode);
                        break;
                    case "collection":
                        EpubCollection nestedCollection = ReadCollection(collectionChildNode);
                        nestedCollections.Add(nestedCollection);
                        break;
                    case "link":
                        EpubMetadataLink link = MetadataReader.ReadLink(collectionChildNode);
                        links.Add(link);
                        break;
                }
            }
            return new(role, metadata, nestedCollections, links, id, textDirection, language);
        }
    }
}
