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
            IZipFileEntry? rootFileEntry = epubFile.GetEntry(rootFilePath);
            if (rootFileEntry == null)
            {
                throw new EpubContainerException("EPUB parsing error: root file not found in the EPUB file.");
            }
            XDocument containerDocument;
            using (Stream containerStream = rootFileEntry.Open())
            {
                containerDocument = await XmlUtils.LoadDocumentAsync(containerStream, epubReaderOptions.XmlReaderOptions).ConfigureAwait(false);
            }
            XNamespace opfNamespace = "http://www.idpf.org/2007/opf";
            XElement packageNode = containerDocument.Element(opfNamespace + "package");
            if (packageNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: package XML element not found in the package file.");
            }
            string epubVersionValue = packageNode.Attribute("version").Value;
            EpubVersion epubVersion = epubVersionValue switch
            {
                "2.0" => EpubVersion.EPUB_2,
                "3.0" => EpubVersion.EPUB_3,
                "3.1" => EpubVersion.EPUB_3_1,
                _ => throw new EpubPackageException($"Unsupported EPUB version: \"{epubVersionValue}\".")
            };
            XElement metadataNode = packageNode.Element(opfNamespace + "metadata");
            if (metadataNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: metadata not found in the package.");
            }
            EpubMetadata metadata = ReadMetadata(metadataNode);
            XElement manifestNode = packageNode.Element(opfNamespace + "manifest");
            if (manifestNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: manifest not found in the package.");
            }
            EpubManifest manifest = ReadManifest(manifestNode, epubReaderOptions.PackageReaderOptions);
            XElement spineNode = packageNode.Element(opfNamespace + "spine");
            if (spineNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: spine not found in the package.");
            }
            EpubSpine spine = ReadSpine(spineNode, epubVersion, epubReaderOptions.PackageReaderOptions);
            EpubGuide? guide = null;
            XElement guideNode = packageNode.Element(opfNamespace + "guide");
            if (guideNode != null)
            {
                guide = ReadGuide(guideNode);
            }
            return new(epubVersion, metadata, manifest, spine, guide);
        }

        private static EpubMetadata ReadMetadata(XElement metadataNode)
        {
            List<string> titles = new();
            List<EpubMetadataCreator> creators = new();
            List<string> subjects = new();
            string? description = null;
            List<string> publishers = new();
            List<EpubMetadataContributor> contributors = new();
            List<EpubMetadataDate> dates = new();
            List<string> types = new();
            List<string> formats = new();
            List<EpubMetadataIdentifier> identifiers = new();
            List<string> sources = new();
            List<string> languages = new();
            List<string> relations = new();
            List<string> coverages = new();
            List<string> rights = new();
            List<EpubMetadataLink> links = new();
            List<EpubMetadataMeta> metaItems = new();
            foreach (XElement metadataItemNode in metadataNode.Elements())
            {
                string innerText = metadataItemNode.Value;
                switch (metadataItemNode.GetLowerCaseLocalName())
                {
                    case "title":
                        titles.Add(innerText);
                        break;
                    case "creator":
                        EpubMetadataCreator creator = ReadMetadataCreator(metadataItemNode);
                        creators.Add(creator);
                        break;
                    case "subject":
                        subjects.Add(innerText);
                        break;
                    case "description":
                        description = innerText;
                        break;
                    case "publisher":
                        publishers.Add(innerText);
                        break;
                    case "contributor":
                        EpubMetadataContributor contributor = ReadMetadataContributor(metadataItemNode);
                        contributors.Add(contributor);
                        break;
                    case "date":
                        EpubMetadataDate date = ReadMetadataDate(metadataItemNode);
                        dates.Add(date);
                        break;
                    case "type":
                        types.Add(innerText);
                        break;
                    case "format":
                        formats.Add(innerText);
                        break;
                    case "identifier":
                        EpubMetadataIdentifier identifier = ReadMetadataIdentifier(metadataItemNode);
                        identifiers.Add(identifier);
                        break;
                    case "source":
                        sources.Add(innerText);
                        break;
                    case "language":
                        languages.Add(innerText);
                        break;
                    case "relation":
                        relations.Add(innerText);
                        break;
                    case "coverage":
                        coverages.Add(innerText);
                        break;
                    case "rights":
                        rights.Add(innerText);
                        break;
                    case "link":
                        EpubMetadataLink link = ReadMetadataLink(metadataItemNode);
                        links.Add(link);
                        break;
                    case "meta":
                        EpubMetadataMeta meta = ReadMetadataMeta(metadataItemNode);
                        metaItems.Add(meta);
                        break;
                }
            }
            return new(titles, creators, subjects, description, publishers, contributors, dates, types, formats, identifiers, sources,
                languages, relations, coverages, rights, links, metaItems);
        }

        private static EpubMetadataCreator ReadMetadataCreator(XElement metadataCreatorNode)
        {
            string? id = null;
            string creator;
            string? role = null;
            string? fileAs = null;
            foreach (XAttribute metadataCreatorNodeAttribute in metadataCreatorNode.Attributes())
            {
                string attributeValue = metadataCreatorNodeAttribute.Value;
                switch (metadataCreatorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "role":
                        role = attributeValue;
                        break;
                    case "file-as":
                        fileAs = attributeValue;
                        break;
                }
            }
            creator = metadataCreatorNode.Value;
            return new(id, creator, fileAs, role);
        }

        private static EpubMetadataContributor ReadMetadataContributor(XElement metadataContributorNode)
        {
            string? id = null;
            string contributor;
            string? role = null;
            string? fileAs = null;
            foreach (XAttribute metadataContributorNodeAttribute in metadataContributorNode.Attributes())
            {
                string attributeValue = metadataContributorNodeAttribute.Value;
                switch (metadataContributorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "role":
                        role = attributeValue;
                        break;
                    case "file-as":
                        fileAs = attributeValue;
                        break;
                }
            }
            contributor = metadataContributorNode.Value;
            return new(id, contributor, fileAs, role);
        }

        private static EpubMetadataDate ReadMetadataDate(XElement metadataDateNode)
        {
            string date;
            string? @event = null;
            XAttribute eventAttribute = metadataDateNode.Attribute(metadataDateNode.Parent.Name.Namespace + "event");
            if (eventAttribute != null)
            {
                @event = eventAttribute.Value;
            }
            date = metadataDateNode.Value;
            return new EpubMetadataDate(date, @event);
        }

        private static EpubMetadataIdentifier ReadMetadataIdentifier(XElement metadataIdentifierNode)
        {
            string? id = null;
            string? scheme = null;
            string identifier;
            foreach (XAttribute metadataIdentifierNodeAttribute in metadataIdentifierNode.Attributes())
            {
                string attributeValue = metadataIdentifierNodeAttribute.Value;
                switch (metadataIdentifierNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "scheme":
                        scheme = attributeValue;
                        break;
                }
            }
            identifier = metadataIdentifierNode.Value;
            return new(id, scheme, identifier);
        }

        private static EpubMetadataLink ReadMetadataLink(XElement metadataIdentifierNode)
        {
            string? id = null;
            string? href = null;
            string? mediaType = null;
            List<EpubMetadataLinkProperty>? properties = null;
            string? refines = null;
            List<EpubMetadataLinkRelationship>? relationships = null;
            foreach (XAttribute metadataIdentifierNodeAttribute in metadataIdentifierNode.Attributes())
            {
                string attributeValue = metadataIdentifierNodeAttribute.Value;
                switch (metadataIdentifierNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        id = attributeValue;
                        break;
                    case "href":
                        href = attributeValue;
                        break;
                    case "media-type":
                        mediaType = attributeValue;
                        break;
                    case "refines":
                        refines = attributeValue;
                        break;
                    case "properties":
                        properties = EpubMetadataLinkPropertyParser.ParsePropertyList(attributeValue);
                        break;
                    case "rel":
                        relationships = EpubMetadataLinkRelationshipParser.ParseRelationshipList(attributeValue);
                        break;
                }
            }
            if (href == null)
            {
                throw new EpubPackageException("Incorrect EPUB metadata link: href is missing");
            }
            if (relationships == null)
            {
                throw new EpubPackageException("Incorrect EPUB metadata link: rel is missing");
            }
            return new(id, href, mediaType, properties, refines, relationships);
        }

        private static EpubMetadataMeta ReadMetadataMeta(XElement metadataMetaNode)
        {
            string? name = null;
            string? content = null;
            string? id = null;
            string? refines = null;
            string? property = null;
            string? scheme = null;
            foreach (XAttribute metadataMetaNodeAttribute in metadataMetaNode.Attributes())
            {
                string attributeValue = metadataMetaNodeAttribute.Value;
                switch (metadataMetaNodeAttribute.GetLowerCaseLocalName())
                {
                    case "name":
                        name = attributeValue;
                        break;
                    case "content":
                        content = attributeValue;
                        break;
                    case "id":
                        id = attributeValue;
                        break;
                    case "refines":
                        refines = attributeValue;
                        break;
                    case "property":
                        property = attributeValue;
                        break;
                    case "scheme":
                        scheme = attributeValue;
                        break;
                }
            }
            content ??= metadataMetaNode.Value;
            return new(name, content, id, refines, property, scheme);
        }

        private static EpubManifest ReadManifest(XElement manifestNode, PackageReaderOptions packageReaderOptions)
        {
            List<EpubManifestItem> items = new();
            foreach (XElement manifestItemNode in manifestNode.Elements())
            {
                if (manifestItemNode.CompareNameTo("item"))
                {
                    string? id = null;
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
                                id = attributeValue;
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
                    if (id == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item ID is missing");
                    }
                    if (href == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item href is missing");
                    }
                    if (mediaType == null)
                    {
                        if (packageReaderOptions != null && packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item media type is missing");
                    }
                    items.Add(new EpubManifestItem(id, href, mediaType, mediaOverlay, requiredNamespace, requiredModules, fallback, fallbackStyle, properties));
                }
            }
            return new(items);
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
                throw new EpubPackageException("Incorrect EPUB spine: TOC is missing");
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
                        throw new EpubPackageException("Incorrect EPUB spine: item ID ref is missing");
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
                        throw new EpubPackageException("Incorrect EPUB guide: item type is missing");
                    }
                    if (href == null)
                    {
                        throw new EpubPackageException("Incorrect EPUB guide: item href is missing");
                    }
                    items.Add(new EpubGuideReference(type, title, href));
                }
            }
            return new(items);
        }
    }
}
