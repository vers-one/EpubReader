﻿using System;
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
    internal static class PackageReader
    {
        public static async Task<EpubPackage> ReadPackageAsync(IZipFile epubFile, string rootFilePath, EpubReaderOptions epubReaderOptions)
        {
            IZipFileEntry rootFileEntry = epubFile.GetEntry(rootFilePath);
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
            EpubPackage result = new EpubPackage();
            string epubVersionValue = packageNode.Attribute("version").Value;
            EpubVersion epubVersion;
            switch (epubVersionValue)
            {
                case "2.0":
                    epubVersion = EpubVersion.EPUB_2;
                    break;
                case "3.0":
                    epubVersion = EpubVersion.EPUB_3;
                    break;
                case "3.1":
                    epubVersion = EpubVersion.EPUB_3_1;
                    break;
                default:
                    throw new EpubPackageException($"Unsupported EPUB version: {epubVersionValue}.");
            }
            result.EpubVersion = epubVersion;
            XElement metadataNode = packageNode.Element(opfNamespace + "metadata");
            if (metadataNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: metadata not found in the package.");
            }
            EpubMetadata metadata = ReadMetadata(metadataNode);
            result.Metadata = metadata;
            XElement manifestNode = packageNode.Element(opfNamespace + "manifest");
            if (manifestNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: manifest not found in the package.");
            }
            EpubManifest manifest = ReadManifest(manifestNode, epubReaderOptions.PackageReaderOptions);
            result.Manifest = manifest;
            XElement spineNode = packageNode.Element(opfNamespace + "spine");
            if (spineNode == null)
            {
                throw new EpubPackageException("EPUB parsing error: spine not found in the package.");
            }
            EpubSpine spine = ReadSpine(spineNode, epubVersion, epubReaderOptions.PackageReaderOptions);
            result.Spine = spine;
            XElement guideNode = packageNode.Element(opfNamespace + "guide");
            if (guideNode != null)
            {
                EpubGuide guide = ReadGuide(guideNode);
                result.Guide = guide;
            }
            return result;
        }

        private static EpubMetadata ReadMetadata(XElement metadataNode)
        {
            EpubMetadata result = new EpubMetadata
            {
                Titles = new List<string>(),
                Creators = new List<EpubMetadataCreator>(),
                Subjects = new List<string>(),
                Publishers = new List<string>(),
                Contributors = new List<EpubMetadataContributor>(),
                Dates = new List<EpubMetadataDate>(),
                Types = new List<string>(),
                Formats = new List<string>(),
                Identifiers = new List<EpubMetadataIdentifier>(),
                Sources = new List<string>(),
                Languages = new List<string>(),
                Relations = new List<string>(),
                Coverages = new List<string>(),
                Rights = new List<string>(),
                Links = new List<EpubMetadataLink>(),
                MetaItems = new List<EpubMetadataMeta>()
            };
            foreach (XElement metadataItemNode in metadataNode.Elements())
            {
                string innerText = metadataItemNode.Value;
                switch (metadataItemNode.GetLowerCaseLocalName())
                {
                    case "title":
                        result.Titles.Add(innerText);
                        break;
                    case "creator":
                        EpubMetadataCreator creator = ReadMetadataCreator(metadataItemNode);
                        result.Creators.Add(creator);
                        break;
                    case "subject":
                        result.Subjects.Add(innerText);
                        break;
                    case "description":
                        result.Description = innerText;
                        break;
                    case "publisher":
                        result.Publishers.Add(innerText);
                        break;
                    case "contributor":
                        EpubMetadataContributor contributor = ReadMetadataContributor(metadataItemNode);
                        result.Contributors.Add(contributor);
                        break;
                    case "date":
                        EpubMetadataDate date = ReadMetadataDate(metadataItemNode);
                        result.Dates.Add(date);
                        break;
                    case "type":
                        result.Types.Add(innerText);
                        break;
                    case "format":
                        result.Formats.Add(innerText);
                        break;
                    case "identifier":
                        EpubMetadataIdentifier identifier = ReadMetadataIdentifier(metadataItemNode);
                        result.Identifiers.Add(identifier);
                        break;
                    case "source":
                        result.Sources.Add(innerText);
                        break;
                    case "language":
                        result.Languages.Add(innerText);
                        break;
                    case "relation":
                        result.Relations.Add(innerText);
                        break;
                    case "coverage":
                        result.Coverages.Add(innerText);
                        break;
                    case "rights":
                        result.Rights.Add(innerText);
                        break;
                    case "link":
                        EpubMetadataLink link = ReadMetadataLink(metadataItemNode);
                        result.Links.Add(link);
                        break;
                    case "meta":
                        EpubMetadataMeta meta = ReadMetadataMeta(metadataItemNode);
                        result.MetaItems.Add(meta);
                        break;
                }
            }
            return result;
        }

        private static EpubMetadataCreator ReadMetadataCreator(XElement metadataCreatorNode)
        {
            EpubMetadataCreator result = new EpubMetadataCreator();
            foreach (XAttribute metadataCreatorNodeAttribute in metadataCreatorNode.Attributes())
            {
                string attributeValue = metadataCreatorNodeAttribute.Value;
                switch (metadataCreatorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "role":
                        result.Role = attributeValue;
                        break;
                    case "file-as":
                        result.FileAs = attributeValue;
                        break;
                }
            }
            result.Creator = metadataCreatorNode.Value;
            return result;
        }

        private static EpubMetadataContributor ReadMetadataContributor(XElement metadataContributorNode)
        {
            EpubMetadataContributor result = new EpubMetadataContributor();
            foreach (XAttribute metadataContributorNodeAttribute in metadataContributorNode.Attributes())
            {
                string attributeValue = metadataContributorNodeAttribute.Value;
                switch (metadataContributorNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "role":
                        result.Role = attributeValue;
                        break;
                    case "file-as":
                        result.FileAs = attributeValue;
                        break;
                }
            }
            result.Contributor = metadataContributorNode.Value;
            return result;
        }

        private static EpubMetadataDate ReadMetadataDate(XElement metadataDateNode)
        {
            EpubMetadataDate result = new EpubMetadataDate();
            XAttribute eventAttribute = metadataDateNode.Attribute(metadataDateNode.Parent.Name.Namespace + "event");
            if (eventAttribute != null)
            {
                result.Event = eventAttribute.Value;
            }
            result.Date = metadataDateNode.Value;
            return result;
        }

        private static EpubMetadataIdentifier ReadMetadataIdentifier(XElement metadataIdentifierNode)
        {
            EpubMetadataIdentifier result = new EpubMetadataIdentifier();
            foreach (XAttribute metadataIdentifierNodeAttribute in metadataIdentifierNode.Attributes())
            {
                string attributeValue = metadataIdentifierNodeAttribute.Value;
                switch (metadataIdentifierNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "scheme":
                        result.Scheme = attributeValue;
                        break;
                }
            }
            result.Identifier = metadataIdentifierNode.Value;
            return result;
        }

        private static EpubMetadataLink ReadMetadataLink(XElement metadataIdentifierNode)
        {
            EpubMetadataLink result = new EpubMetadataLink();
            foreach (XAttribute metadataIdentifierNodeAttribute in metadataIdentifierNode.Attributes())
            {
                string attributeValue = metadataIdentifierNodeAttribute.Value;
                switch (metadataIdentifierNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "href":
                        result.Href = attributeValue;
                        break;
                    case "media-type":
                        result.MediaType = attributeValue;
                        break;
                    case "refines":
                        result.Refines = attributeValue;
                        break;
                    case "properties":
                        result.Properties = EpubMetadataLinkPropertyParser.ParsePropertyList(attributeValue);
                        break;
                    case "rel":
                        result.Relationships = EpubMetadataLinkRelationshipParser.ParseRelationshipList(attributeValue);
                        break;
                }
            }
            return result;
        }

        private static EpubMetadataMeta ReadMetadataMeta(XElement metadataMetaNode)
        {
            EpubMetadataMeta result = new EpubMetadataMeta();
            foreach (XAttribute metadataMetaNodeAttribute in metadataMetaNode.Attributes())
            {
                string attributeValue = metadataMetaNodeAttribute.Value;
                switch (metadataMetaNodeAttribute.GetLowerCaseLocalName())
                {
                    case "name":
                        result.Name = attributeValue;
                        break;
                    case "content":
                        result.Content = attributeValue;
                        break;
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "refines":
                        result.Refines = attributeValue;
                        break;
                    case "property":
                        result.Property = attributeValue;
                        break;
                    case "scheme":
                        result.Scheme = attributeValue;
                        break;
                }
            }
            if (result.Content == null)
            {
                result.Content = metadataMetaNode.Value;
            }
            return result;
        }

        private static EpubManifest ReadManifest(XElement manifestNode, PackageReaderOptions packageReaderOptions)
        {
            EpubManifest result = new EpubManifest()
            {
                Items = new List<EpubManifestItem>()
            };
            foreach (XElement manifestItemNode in manifestNode.Elements())
            {
                if (manifestItemNode.CompareNameTo("item"))
                {
                    EpubManifestItem manifestItem = new EpubManifestItem();
                    foreach (XAttribute manifestItemNodeAttribute in manifestItemNode.Attributes())
                    {
                        string attributeValue = manifestItemNodeAttribute.Value;
                        switch (manifestItemNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "id":
                                manifestItem.Id = attributeValue;
                                break;
                            case "href":
                                manifestItem.Href = Uri.UnescapeDataString(attributeValue);
                                break;
                            case "media-type":
                                manifestItem.MediaType = attributeValue;
                                break;
                            case "media-overlay":
                                manifestItem.MediaOverlay = attributeValue;
                                break;
                            case "required-namespace":
                                manifestItem.RequiredNamespace = attributeValue;
                                break;
                            case "required-modules":
                                manifestItem.RequiredModules = attributeValue;
                                break;
                            case "fallback":
                                manifestItem.Fallback = attributeValue;
                                break;
                            case "fallback-style":
                                manifestItem.FallbackStyle = attributeValue;
                                break;
                            case "properties":
                                manifestItem.Properties = EpubManifestPropertyParser.ParsePropertyList(attributeValue);
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(manifestItem.Id))
                    {
                        if (packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item ID is missing");
                    }
                    if (String.IsNullOrWhiteSpace(manifestItem.Href))
                    {
                        if (packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item href is missing");
                    }
                    if (String.IsNullOrWhiteSpace(manifestItem.MediaType))
                    {
                        if (packageReaderOptions.SkipInvalidManifestItems)
                        {
                            continue;
                        }
                        throw new EpubPackageException("Incorrect EPUB manifest: item media type is missing");
                    }
                    result.Items.Add(manifestItem);
                }
            }
            return result;
        }

        private static EpubSpine ReadSpine(XElement spineNode, EpubVersion epubVersion, PackageReaderOptions packageReaderOptions)
        {
            EpubSpine result = new EpubSpine()
            {
                Items = new List<EpubSpineItemRef>()
            };
            foreach (XAttribute spineNodeAttribute in spineNode.Attributes())
            {
                string attributeValue = spineNodeAttribute.Value;
                switch (spineNodeAttribute.GetLowerCaseLocalName())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "page-progression-direction":
                        result.PageProgressionDirection = EpubPageProgressionDirectionParser.Parse(attributeValue);
                        break;
                    case "toc":
                        result.Toc = attributeValue;
                        break;
                }
            }
            if (epubVersion == EpubVersion.EPUB_2 && String.IsNullOrWhiteSpace(result.Toc) && !packageReaderOptions.IgnoreMissingToc)
            {
                throw new EpubPackageException("Incorrect EPUB spine: TOC is missing");
            }
            foreach (XElement spineItemNode in spineNode.Elements())
            {
                if (spineItemNode.CompareNameTo("itemref"))
                {
                    EpubSpineItemRef spineItemRef = new EpubSpineItemRef();
                    foreach (XAttribute spineItemNodeAttribute in spineItemNode.Attributes())
                    {
                        string attributeValue = spineItemNodeAttribute.Value;
                        switch (spineItemNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "id":
                                spineItemRef.Id = attributeValue;
                                break;
                            case "idref":
                                spineItemRef.IdRef = attributeValue;
                                break;
                            case "properties":
                                spineItemRef.Properties = EpubSpinePropertyParser.ParsePropertyList(attributeValue);
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(spineItemRef.IdRef))
                    {
                        throw new EpubPackageException("Incorrect EPUB spine: item ID ref is missing");
                    }
                    XAttribute linearAttribute = spineItemNode.Attribute("linear");
                    spineItemRef.IsLinear = linearAttribute == null || !linearAttribute.CompareValueTo("no");
                    result.Items.Add(spineItemRef);
                }
            }
            return result;
        }

        private static EpubGuide ReadGuide(XElement guideNode)
        {
            EpubGuide result = new EpubGuide()
            {
                Items = new List<EpubGuideReference>()
            };
            foreach (XElement guideReferenceNode in guideNode.Elements())
            {
                if (guideReferenceNode.CompareNameTo("reference"))
                {
                    EpubGuideReference guideReference = new EpubGuideReference();
                    foreach (XAttribute guideReferenceNodeAttribute in guideReferenceNode.Attributes())
                    {
                        string attributeValue = guideReferenceNodeAttribute.Value;
                        switch (guideReferenceNodeAttribute.GetLowerCaseLocalName())
                        {
                            case "type":
                                guideReference.Type = attributeValue;
                                break;
                            case "title":
                                guideReference.Title = attributeValue;
                                break;
                            case "href":
                                guideReference.Href = Uri.UnescapeDataString(attributeValue);
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(guideReference.Type))
                    {
                        throw new EpubPackageException("Incorrect EPUB guide: item type is missing");
                    }
                    if (String.IsNullOrWhiteSpace(guideReference.Href))
                    {
                        throw new EpubPackageException("Incorrect EPUB guide: item href is missing");
                    }
                    result.Items.Add(guideReference);
                }
            }
            return result;
        }
    }
}
