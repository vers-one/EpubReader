using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Xml;
using VersFx.Formats.Text.Epub.Schema.Opf;

namespace VersFx.Formats.Text.Epub.Readers
{
    internal static class PackageReader
    {
        public static EpubPackage ReadPackage(ZipArchive epubArchive, string rootFilePath)
        {
            ZipArchiveEntry rootFileEntry = epubArchive.GetEntry(rootFilePath);
            if (rootFileEntry == null)
                throw new Exception("EPUB parsing error: root file not found in archive.");
            using (Stream containerStream = rootFileEntry.Open())
            {
                XmlDocument containerDocument = new XmlDocument();
                containerDocument.Load(containerStream);
                XmlNamespaceManager xmlNamespaceManager = new XmlNamespaceManager(containerDocument.NameTable);
                xmlNamespaceManager.AddNamespace("opf", "http://www.idpf.org/2007/opf");
                XmlNode packageNode = containerDocument.DocumentElement.SelectSingleNode("/opf:package", xmlNamespaceManager);
                EpubPackage result = new EpubPackage();
                string epubVersionValue = packageNode.Attributes["version"].Value;
                if (epubVersionValue == "2.0")
                    result.EpubVersion = EpubVersion.EPUB_2;
                else
                    if (epubVersionValue == "3.0")
                        result.EpubVersion = EpubVersion.EPUB_3;
                    else
                        throw new Exception(String.Format("Unsupported EPUB version: {0}.", epubVersionValue));
                XmlNode metadataNode = packageNode.SelectSingleNode("opf:metadata", xmlNamespaceManager);
                if (metadataNode == null)
                    throw new Exception("EPUB parsing error: metadata not found in the package.");
                EpubMetadata metadata = ReadMetadata(metadataNode);
                result.Metadata = metadata;
                XmlNode manifestNode = packageNode.SelectSingleNode("opf:manifest", xmlNamespaceManager);
                if (manifestNode == null)
                    throw new Exception("EPUB parsing error: manifest not found in the package.");
                EpubManifest manifest = ReadManifest(manifestNode);
                result.Manifest = manifest;
                XmlNode spineNode = packageNode.SelectSingleNode("opf:spine", xmlNamespaceManager);
                if (spineNode == null)
                    throw new Exception("EPUB parsing error: spine not found in the package.");
                EpubSpine spine = ReadSpine(spineNode);
                result.Spine = spine;
                XmlNode guideNode = packageNode.SelectSingleNode("opf:guide", xmlNamespaceManager);
                if (guideNode != null)
                {
                    EpubGuide guide = ReadGuide(guideNode);
                    result.Guide = guide;
                }
                return result;
            }
        }

        private static EpubMetadata ReadMetadata(XmlNode metadataNode)
        {
            EpubMetadata result = new EpubMetadata();
            result.Titles = new List<string>();
            result.Creators = new List<EpubMetadataCreator>();
            result.Subjects = new List<string>();
            result.Publishers = new List<string>();
            result.Contributors = new List<EpubMetadataContributor>();
            result.Dates = new List<EpubMetadataDate>();
            result.Types = new List<string>();
            result.Formats = new List<string>();
            result.Identifiers = new List<EpubMetadataIdentifier>();
            result.Sources = new List<string>();
            result.Languages = new List<string>();
            result.Relations = new List<string>();
            result.Coverages = new List<string>();
            result.Rights = new List<string>();
            foreach (XmlNode metadataItemNode in metadataNode.ChildNodes)
            {
                string innerText = metadataItemNode.InnerText;
                switch (metadataItemNode.LocalName.ToLowerInvariant())
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
                }
            }
            return result;
        }

        private static EpubMetadataCreator ReadMetadataCreator(XmlNode metadataCreatorNode)
        {
            EpubMetadataCreator result = new EpubMetadataCreator();
            foreach (XmlAttribute metadataCreatorNodeAttribute in metadataCreatorNode.Attributes)
            {
                string attributeValue = metadataCreatorNodeAttribute.Value;
                switch (metadataCreatorNodeAttribute.Name.ToLowerInvariant())
                {
                    case "opf:role":
                        result.Role = attributeValue;
                        break;
                    case "opf:file-as":
                        result.FileAs = attributeValue;
                        break;
                }
            }
            result.Creator = metadataCreatorNode.InnerText;
            return result;
        }

        private static EpubMetadataContributor ReadMetadataContributor(XmlNode metadataContributorNode)
        {
            EpubMetadataContributor result = new EpubMetadataContributor();
            foreach (XmlAttribute metadataContributorNodeAttribute in metadataContributorNode.Attributes)
            {
                string attributeValue = metadataContributorNodeAttribute.Value;
                switch (metadataContributorNodeAttribute.Name.ToLowerInvariant())
                {
                    case "opf:role":
                        result.Role = attributeValue;
                        break;
                    case "opf:file-as":
                        result.FileAs = attributeValue;
                        break;
                }
            }
            result.Contributor = metadataContributorNode.InnerText;
            return result;
        }

        private static EpubMetadataDate ReadMetadataDate(XmlNode metadataDateNode)
        {
            EpubMetadataDate result = new EpubMetadataDate();
            XmlAttribute eventAttribute = metadataDateNode.Attributes["opf:event"];
            if (eventAttribute != null)
                result.Event = eventAttribute.Value;
            result.Date = metadataDateNode.InnerText;
            return result;
        }

        private static EpubMetadataIdentifier ReadMetadataIdentifier(XmlNode metadataIdentifierNode)
        {
            EpubMetadataIdentifier result = new EpubMetadataIdentifier();
            foreach (XmlAttribute metadataIdentifierNodeAttribute in metadataIdentifierNode.Attributes)
            {
                string attributeValue = metadataIdentifierNodeAttribute.Value;
                switch (metadataIdentifierNodeAttribute.Name.ToLowerInvariant())
                {
                    case "id":
                        result.Id = attributeValue;
                        break;
                    case "opf:scheme":
                        result.Scheme = attributeValue;
                        break;
                }
            }
            result.Identifier = metadataIdentifierNode.InnerText;
            return result;
        }

        private static EpubManifest ReadManifest(XmlNode manifestNode)
        {
            EpubManifest result = new EpubManifest();
            foreach (XmlNode manifestItemNode in manifestNode.ChildNodes)
                if (String.Compare(manifestItemNode.LocalName, "item", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubManifestItem manifestItem = new EpubManifestItem();
                    foreach (XmlAttribute manifestItemNodeAttribute in manifestItemNode.Attributes)
                    {
                        string attributeValue = manifestItemNodeAttribute.Value;
                        switch (manifestItemNodeAttribute.Name.ToLowerInvariant())
                        {
                            case "id":
                                manifestItem.Id = attributeValue;
                                break;
                            case "href":
                                manifestItem.Href = attributeValue;
                                break;
                            case "media-type":
                                manifestItem.MediaType = attributeValue;
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
                        }
                    }
                    if (String.IsNullOrWhiteSpace(manifestItem.Id))
                        throw new Exception("Incorrect EPUB manifest: item ID is missing");
                    if (String.IsNullOrWhiteSpace(manifestItem.Href))
                        throw new Exception("Incorrect EPUB manifest: item href is missing");
                    if (String.IsNullOrWhiteSpace(manifestItem.MediaType))
                        throw new Exception("Incorrect EPUB manifest: item media type is missing");
                    result.Add(manifestItem);
                }
            return result;
        }

        private static EpubSpine ReadSpine(XmlNode spineNode)
        {
            EpubSpine result = new EpubSpine();
            XmlAttribute tocAttribute = spineNode.Attributes["toc"];
            if (tocAttribute == null || String.IsNullOrWhiteSpace(tocAttribute.Value))
                throw new Exception("Incorrect EPUB spine: TOC is missing");
            result.Toc = tocAttribute.Value;
            foreach (XmlNode spineItemNode in spineNode.ChildNodes)
                if (String.Compare(spineItemNode.LocalName, "itemref", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubSpineItemRef spineItemRef = new EpubSpineItemRef();
                    XmlAttribute idRefAttribute = spineItemNode.Attributes["idref"];
                    if (idRefAttribute == null || String.IsNullOrWhiteSpace(idRefAttribute.Value))
                        throw new Exception("Incorrect EPUB spine: item ID ref is missing");
                    spineItemRef.IdRef = idRefAttribute.Value;
                    XmlAttribute linearAttribute = spineItemNode.Attributes["linear"];
                    spineItemRef.IsLinear = linearAttribute == null || String.Compare(linearAttribute.Value, "no", StringComparison.OrdinalIgnoreCase) != 0;
                    result.Add(spineItemRef);
                }
            return result;
        }

        private static EpubGuide ReadGuide(XmlNode guideNode)
        {
            EpubGuide result = new EpubGuide();
            foreach (XmlNode guideReferenceNode in guideNode.ChildNodes)
                if (String.Compare(guideReferenceNode.LocalName, "reference", StringComparison.OrdinalIgnoreCase) == 0)
                {
                    EpubGuideReference guideReference = new EpubGuideReference();
                    foreach (XmlAttribute guideReferenceNodeAttribute in guideReferenceNode.Attributes)
                    {
                        string attributeValue = guideReferenceNodeAttribute.Value;
                        switch (guideReferenceNodeAttribute.Name.ToLowerInvariant())
                        {
                            case "type":
                                guideReference.Type = attributeValue;
                                break;
                            case "title":
                                guideReference.Title = attributeValue;
                                break;
                            case "href":
                                guideReference.Href = attributeValue;
                                break;
                        }
                    }
                    if (String.IsNullOrWhiteSpace(guideReference.Type))
                        throw new Exception("Incorrect EPUB guide: item type is missing");
                    if (String.IsNullOrWhiteSpace(guideReference.Href))
                        throw new Exception("Incorrect EPUB guide: item href is missing");
                    result.Add(guideReference);
                }
            return result;
        }
    }
}
